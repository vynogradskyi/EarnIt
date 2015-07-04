using EarnIt.Ninja.Services.Domain.Enums;

namespace EarnIt.Ninja.Services.Domain.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}