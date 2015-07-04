using System;
using System.Collections.Generic;
using EarnIt.Ninja.Services.Contract.Entities;
using EarnIt.Ninja.Services.Contract.Repositories;
using EarnIt.Ninja.Services.Domain.Enums;
using EarnIt.Ninja.Services.Domain.Models;
using EarnIt.Ninja.Services.Implementation.Services;
using EarnIt.Ninja.WebAPI.CompositionRoot;
using EarnIt.Ninja.WebAPI.Models;
using Moq;
using Ninject;
using NUnit.Framework;

namespace EarnIt.Ninja.Test.Services
{
    [TestFixture]
    public class SynchronizationServiceTest
    {

        [Test]
        public void Synchronize_WhenGetRestricted_ThenReturnByEntity_Test()
        {
            UsingSynchronizationService(service =>
            {
                //Assign
                var syncRequest = new SynchronizationRequest()
                {
                    UserId = "12",
                    Types = new[] {typeof (User)},
                    Restricted = true,
                    RequestType = SynchronizationType.Get,
                    Entities = new List<IEntity>()
                };
                
                //Act
                var res = service.Synchronize(syncRequest);

                //Assert
                Assert.AreEqual(1, res.Count);
            });
        }


        [Test]
        public void Synchronize_WhenGetNotRestricted_ThenReturnByType_Test()
        {
            UsingSynchronizationService(service =>
            {
                //Assign
                var syncRequest = new SynchronizationRequest()
                {
                    UserId = "12",
                    Types = new[] {typeof (User)},
                    Restricted = false,
                    RequestType = SynchronizationType.Get,
                    Entities = new List<IEntity>()
                };
                
                //Act
                var res = service.Synchronize(syncRequest);

                //Assert
                Assert.IsEmpty(res);
            });
        }


        [Test]
        public void Synchronize_WhenPost_ThenNoExceptions_Test()
        {
            UsingSynchronizationService(service =>
            {
                //Assign
                var syncRequest = new SynchronizationRequest()
                {
                    UserId = "12",
                    Types = new[] {typeof (User)},
                    Restricted = true,
                    RequestType = SynchronizationType.Post,
                    Entities = new List<IEntity>()
                };
                
                //Act
                Assert.DoesNotThrow(() => service.Synchronize(syncRequest));
            });
        }

        private void UsingSynchronizationService(Action<SynchronizationService> action)
        {
            var kernel = new StandardKernel();
            var userRepositoryMock = new Mock<IRepository<User>>();
            userRepositoryMock.Setup(r => r.Get()).Returns(new List<IEntity>());
            userRepositoryMock.Setup(r => r.Get(It.IsAny<IEnumerable<int>>())).Returns(new List<IEntity>(){new SynchronizationEntity()});
            userRepositoryMock.Setup(r => r.SaveAll(It.IsAny<List<IEntity>>()));
            kernel.Bind<IRepository<User>>().ToConstant(userRepositoryMock.Object);
            var repositoryFactory = new GenericRepositoryFactory(kernel);
            var service = new SynchronizationService(repositoryFactory);
            action(service);
        }
    }
}