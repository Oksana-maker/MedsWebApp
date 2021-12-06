using MedsWebApp.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MedsWebApp
{
    public static class AuthOptions
    {
        private const string ISSUER = "MedsWebAppServer"; // издатель токена
        private const string AUDIENCE = "MedsWebAppAPIClient"; // потребитель токена
        private const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        private const int LIFETIME = 10; // время жизни токена - 1 минута
        private const int REFRESH_TOKEN_LIFETIME = 30; // время жизни токена обновления - 30 дней
        private readonly static JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
        public static TokenValidationParameters GetTokenValidationParameters() => new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = ISSUER,
            ValidateAudience = true,
            ValidAudience = AUDIENCE,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            IssuerSigningKey = GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
        public static (string accessToken, DateTime expire) GenerateJWT(User user)
        {
            var now = DateTime.Now;
            var expire = now.AddMinutes(LIFETIME);
            var jwt = new JwtSecurityToken(
                issuer: ISSUER,
                audience: AUDIENCE,
                claims: new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.GetRoleString())
                    },
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType).Claims,
                notBefore: now,
                expires: expire,
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            return (_jwtSecurityTokenHandler.WriteToken(jwt), expire);
        }
        public static (Guid refreshToken, DateTime expire) GenerateRefreshToken()
        {
            return (Guid.NewGuid(), DateTime.Now.AddDays(REFRESH_TOKEN_LIFETIME));
        }

        public static ClaimsPrincipal ValidateToken(string accessToken)
        {
            try
            {
                var claims = _jwtSecurityTokenHandler.ValidateToken(accessToken, GetTokenValidationParameters(), out var _);
                return claims;
            }
            catch
            {
                return null;
            }
        }
    }
}
