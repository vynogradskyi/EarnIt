using System;
using EarnIt.Ninja.Services.Contract.Repositories;
using EarnIt.Ninja.Services.Contract.Services;
using EarnIt.Ninja.Services.Domain.Models;

namespace EarnIt.Ninja.Services.Implementation.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void SignUp(User user)
        {
            _userRepository.Add(user);
        }

        public bool SignIn(string login, string password)
        {
            var user = _userRepository.Get(login);
            return user != null && user.Password == password;
        }

        public string GenerateToken(string login)
        {
            var token = Guid.NewGuid().ToString();
            var user = _userRepository.Get(login);
            user.Token = token;
            _userRepository.Update(user);
            return token;
        }
    }
}