using MedsWebApp.Models;
using MedsWebApp.ViewModels;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Services
{
    public static class SendEmailService
    {
        public static async Task<bool> SendRegistrationApiUserEmail(this IEmailService mailService, ILogger logger, ViewRender viewRender, User user, string currentHost)
        {
            var (accessToken, expire) = AuthOptions.GenerateJWT(user);
            try
            {
                var email = user.Email;
                var header = "Реєстрація в мережі Аптека";
                var body = viewRender.Render("_RegistrationEmailTemplate", (user.Name, accessToken, expire, currentHost));
                await mailService.SendAsync(email, header, body, true);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Send registration mail error: user: email = {user.Email}, id = {user.Id}");
                return false;
            }
        }
        public static async Task<bool> SendRegistrationPharmacyUserEmail(this IEmailService mailService, ILogger logger, ViewRender viewRender, User user, Pharmacy pharmacy, string currentHost)
        {
            var (accessToken, expire) = AuthOptions.GenerateJWT(user);
            try
            {
                var email = user.Email;
                var header = $"Реєстрація користувача для {pharmacy.Name}";
                var body = viewRender.Render("_RegistrationEmailTemplate", (user.Name, accessToken, expire, currentHost));
                await mailService.SendAsync(email, header, body, true);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Send registration mail error: user: email = {user.Email}, id = {user.Id}");
                return false;
            }
        }
        public static async Task<bool> SendRegistrationAdminUserEmail(this IEmailService mailService, ILogger logger, ViewRender viewRender, User user, string currentHost)
        {
            var (accessToken, expire) = AuthOptions.GenerateJWT(user);
            try
            {
                var email = user.Email;
                var header = "Реєстрація адміністратора";
                var body = viewRender.Render("_RegistrationEmailTemplate", (user.Name, accessToken, expire, currentHost));
                await mailService.SendAsync(email, header, body, true);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Send registration mail error: user: email = {user.Email}, id = {user.Id}");
                return false;
            }
        }
        public static async Task<bool> SendOrderInfoEmail(this IEmailService mailService, ILogger logger, ViewRender viewRender, string userEmail, IEnumerable<Order> orders)
        {
            try
            {
                var header = "Інформація про замовлення в мережі Аптека";
                var body = viewRender.Render("_OrderEmailTemplate", orders.AsViewModel());
                await mailService.SendAsync(userEmail, header, body, true);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Send order info mail error: user: email = {userEmail}");
                return false;
            }
        }
        public static async Task<bool> SendResetPasswordEmail(this IEmailService mailService, ILogger logger, ViewRender viewRender, User user, string currentHost)
        {
            var (accessToken, expire) = AuthOptions.GenerateJWT(user);
            try
            {
                var email = user.Email;
                var header = "Відновлення паролю";
                var body = viewRender.Render("_ResetPasswordEmailTemplate", (user.Name, accessToken, expire, currentHost));
                await mailService.SendAsync(email, header, body, true);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Send registration mail error: user: email = {user.Email}, id = {user.Id}");
                return false;
            }
        }
    }
}
