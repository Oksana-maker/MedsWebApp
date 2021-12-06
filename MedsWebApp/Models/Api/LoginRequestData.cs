namespace MedsWebApp.Models.Api
{
    public class LoginRequestData
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
