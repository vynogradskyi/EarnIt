using System;
using EarnIt.Ninja.Services.Contract.Services;
using EarnIt.Ninja.Services.Domain.Models;
using EarnIt.Ninja.WebAPI.Models;
using Nancy;
using Nancy.ModelBinding;

namespace EarnIt.Ninja.WebAPI.Modules
{
    public class AuthenticationModule : NancyModule
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationModule(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            Post["/signup"] = p => SignUp();
            Get["/signin"] = p => SignIn();
        }

        private EarnItResponse SignUp()
        {
            try
            {
                var user = this.Bind<User>();
                _authenticationService.SignUp(user);
                return new EarnItResponse
                {
                    HttpStatus = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new EarnItResponse
                {
                    HttpStatus = HttpStatusCode.InternalServerError,
#if DEBUG
                    Message = e.Message
#endif
                };
            }
        }

        private EarnItResponse SignIn()
        {
            try
            {
                var login = this.Request.Query["login"];
                var password = this.Request.Query["password"];
                var token = _authenticationService.SignIn(login, password);
                return new EarnItResponse
                {
                    HttpStatus = HttpStatusCode.OK,
                    Data = new { Token = token }
                };

            }
            catch (Exception e)
            {
                return new EarnItResponse
                {
                    HttpStatus = HttpStatusCode.InternalServerError,
#if DEBUG
                    Message = e.Message
#endif
                };
            }
        }


    }
}