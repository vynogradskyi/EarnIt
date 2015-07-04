using EarnIt.Ninja.Services.Domain.Models;

namespace EarnIt.Ninja.Services.Contract.Services
{
    public interface IAuthenticationService
    {
        void SignUp(User user);
        bool SignIn(string login, string password);
        string GenerateToken(string login);
    }
}