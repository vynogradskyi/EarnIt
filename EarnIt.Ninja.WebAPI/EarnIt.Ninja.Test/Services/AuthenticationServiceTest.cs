using System;
using EarnIt.Ninja.Services.Contract.Repositories;
using EarnIt.Ninja.Services.Contract.Services;
using EarnIt.Ninja.Services.Domain.Enums;
using EarnIt.Ninja.Services.Domain.Models;
using EarnIt.Ninja.Services.Implementation.Services;
using Moq;
using NUnit.Framework;

namespace EarnIt.Ninja.Test.Services
{
    public class AuthenticationServiceTest
    {

        [Test]
        public void SignIn_WhenLoginCorrect_ThenTrue_Test()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(ur => ur.Get(It.Is<string>(v => v == "valek@gmail.com"))).Returns(new User
            {
                Email = "valek@gmail.com",
                Password = "qwerty"
            });
            UsingAuthenticationService(a =>
            {
                //Act
                var res = a.SignIn("valek@gmail.com", "qwerty");
                //Assert
                Assert.IsTrue(res);
            }, userRepositoryMock.Object);
        }

        [Test]
        public void SignIn_WhenLoginIncorrect_ThenFalse_Test()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(ur => ur.Get(It.Is<string>(v => v == "valek@gmail.com"))).Returns(new User
            {
                Email = "valek@gmail.com",
                Password = "qwerty"
            });
            UsingAuthenticationService(a =>
            {
                //Act
                var res = a.SignIn("valek", "qwerty");
                //Assert
                Assert.IsFalse(res);
            },userRepositoryMock.Object);
        } 

        [Test]
        public void SignUp_WhenUserInsertedOk_ThenNoExceptions_Test()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(ur => ur.Get(It.Is<string>(v => v == "valek@gmail.com"))).Returns(new User
            {
                Email = "valek@gmail.com",
                Password = "qwerty"
            });
            UsingAuthenticationService(a =>
            {
                //Assign
                var user = new User
                {
                    Email = "email",
                    FirstName = "firstName",
                    LastName = "lastName",
                    Password = "password",
                    UserType = UserType.Child
                };
                //Assert
                Assert.DoesNotThrow(() =>
                {
                    a.SignUp(user);
                });
            }, userRepositoryMock.Object);
        } 

        [Test]
        public void SignUp_WhenNull_ThenThrowsNullReference_Test()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(ur => ur.Get(It.Is<string>(v => v == "valek@gmail.com"))).Returns(new User
            {
                Email = "valek@gmail.com",
                Password = "qwerty"
            });
            UsingAuthenticationService(a =>
            {
                //Assert
                Assert.DoesNotThrow(() =>
                {
                    a.SignUp(null);
                });
            }, userRepositoryMock.Object);
        }

        private static void UsingAuthenticationService(Action<IAuthenticationService> a, IUserRepository userRepository)
        {
            var service = new AuthenticationService(userRepository);
            a(service);
        }
    }
}