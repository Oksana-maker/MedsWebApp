using MedsWebApp.Models;
using MedsWebApp.Repositories;
using MedsWebApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".MedsWebApp.Session";
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = AuthOptions.GetTokenValidationParameters();
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = async context =>
                            {
                                var path = context.HttpContext.Request.Path;
                                if (!path.StartsWithSegments("/pharmacy")) return;
                                var accessToken = context.Request.Cookies["access_token"];
                                var refreshToken = context.Request.Cookies["refresh_token"];
                                if (!string.IsNullOrEmpty(accessToken) && AuthOptions.ValidateToken(accessToken) != null)
                                {
                                    context.Token = accessToken;
                                    return;
                                }
                                if (!Guid.TryParse(refreshToken, out Guid refresh_token) || (path.StartsWithSegments("/pharmacy/orders") &&
                                    !path.StartsWithSegments("/pharmacy/orders/live")))
                                {
                                    context.Fail(new Exception("refresh token not found"));
                                    return;
                                }
                                else
                                {
                                    var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationContext>();
                                    var userRepository = new UserRepository(dbContext);
                                    var user = await userRepository.FindByRefreshToken(refresh_token, Models.User.Roles.PharmacyUser);
                                    if (user == null)
                                    {
                                        context.Fail(new Exception("refresh token not found"));
                                        return;
                                    };
                                    var (newRefreshToken, expire) = AuthOptions.GenerateRefreshToken();
                                    user.RefreshToken = newRefreshToken;
                                    user.RefreshTokenExpire = expire;
                                    user = await userRepository.Update(user);
                                    var (access_token, expireAT) = AuthOptions.GenerateJWT(user);
                                    context.Response.Cookies.Append("access_token", access_token, new CookieOptions { HttpOnly = true, Expires = expireAT.AddHours(1) });
                                    context.Response.Cookies.Append("refresh_token", user.RefreshToken.ToString(), new CookieOptions { HttpOnly = true, Expires = expire });
                                    context.Token = access_token;
                                }
                            },
                            OnChallenge = context =>
                            {
                                if (context.AuthenticateFailure == null) return Task.CompletedTask;
                                var path = context.HttpContext.Request.Path;
                                if (!path.StartsWithSegments("/pharmacy") || path.StartsWithSegments("/pharmacy/orders/live")) return Task.CompletedTask;
                                context.HandleResponse();
                                var refreshToken = context.Request.Cookies["refresh_token"];
                                if (string.IsNullOrEmpty(refreshToken))
                                {
                                    context.Response.Redirect(context.Request.Scheme + "://" + context.Request.Host + "/Pharmacy");
                                    return Task.CompletedTask;
                                }
                                context.Response.Redirect(context.Request.Scheme + "://" + context.Request.Host + "/Pharmacy/RefreshToken?refresh_token=" + refreshToken + "&redirect=" + $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{path}");
                                return Task.CompletedTask;
                            },
                        };
                    });
            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    Server = Configuration["MailService:Server"],
                    Port = Convert.ToInt32(Configuration["MailService:Port"]),
                    SenderName = Configuration["MailService:SenderName"],
                    SenderEmail = Configuration["MailService:SenderEmail"],
                    Account = Configuration["MailService:Account"],
                    Password = Configuration["MailService:Password"],
                    Security = true
                });
            });
            services.AddScoped<ViewRender, ViewRender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{action=Index}/{id?}",
                    defaults: new { controller = "Home" });
                endpoints.MapHub<OrdersInfoHub>("/pharmacy/orders/live");
            });
        }
    }
}
