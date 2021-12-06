using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private static readonly SHA256 sha256 = SHA256.Create();
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }


        public Task<User> FindByEmail(string email, User.Roles role) => Get(model => model.Email == email && model.Role == role).FirstOrDefaultAsync();
        public Task<User> FindById(int id, User.Roles role) => Get(model => model.Id == id && model.Role == role).FirstOrDefaultAsync();
        public Task<User> FindByRefreshToken(Guid refreshToken, User.Roles role) => Get(model => model.RefreshToken == refreshToken && model.Role == role).FirstOrDefaultAsync();

        public Task<User> Login(string email, string password, User.Roles role)
        {
            var passwordHashBase64 = GetPasswordHash(password);
            if(role == User.Roles.PharmacyUser)
                return Get(model => model.Email == email && model.PasswordHash == passwordHashBase64 && (model.Role == role || model.Role == User.Roles.Admin)).FirstOrDefaultAsync();
            else
                return Get(model => model.Email == email && model.PasswordHash == passwordHashBase64 && model.Role == role).FirstOrDefaultAsync();
        }

        public Task<User> Register(string email, string password, User.Roles role, string name, int? pharmacyId)
        {
            var (refreshToken, refreshTokenExpire) = AuthOptions.GenerateRefreshToken();
            var newUser = new User
            {
                Name = string.IsNullOrWhiteSpace(name) ? "Користувач" : name,
                Email = email,
                PasswordHash = GetPasswordHash(password),
                RefreshToken = refreshToken,
                RefreshTokenExpire = refreshTokenExpire,
                Role = role,
                PharmacyId = pharmacyId
            };
            return Insert(newUser);
        }

        public static string GetPasswordHash(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHashBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(passwordHashBytes);
        }


    }
}
