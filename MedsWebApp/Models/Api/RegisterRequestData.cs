using System.ComponentModel.DataAnnotations;

namespace MedsWebApp.Models.Api
{
    public class RegisterRequestData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Name) && new EmailAddressAttribute().IsValid(Login);
        }
    }
}
