using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class User : BaseModel
    {
        public const string AdminRole = "admin"; 
        public const string ApiUserRole = "api_user"; 
        public const string CustomerRole = "customer"; 
        public const string PharmacyUserRole = "pharmacy_user";
        
        public enum Roles : ushort
        {
            None,
            Admin,
            ApiUser,
            Customer,
            PharmacyUser
        }
        public static string GetRoleString(Roles role) => role switch
        {
            Roles.Admin => AdminRole,
            Roles.ApiUser => ApiUserRole,
            Roles.Customer => CustomerRole,
            Roles.PharmacyUser => PharmacyUserRole,
            _ => string.Empty
        };
        public static Roles GetRoleFromString(string role) => role switch
        {
            AdminRole => Roles.Admin,
            ApiUserRole => Roles.ApiUser,
            CustomerRole => Roles.Customer,
            PharmacyUserRole => Roles.PharmacyUser,
            _ => Roles.None
        };
        public string GetRoleString() => GetRoleString(Role);

        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime RefreshTokenExpire { get; set; }
        [Required]
        public Roles Role { get; set; }
        public List<Order> Orders { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public int? PharmacyId { get; set; }
    }
}
