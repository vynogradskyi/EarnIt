using System;
using EarnIt.Ninja.Services.Contract.Entities;
using EarnIt.Ninja.Services.Contract.Services;
using EarnIt.Ninja.WebAPI.Models;
using Nancy;
using Nancy.ModelBinding;

namespace EarnIt.Ninja.WebAPI.Modules
{
    public class SynchronizationModule : NancyModule
    {
        private readonly ISynchronizationService _synchronizationService;

        public SynchronizationModule(ISynchronizationService synchronizationService)
        {
            _synchronizationService = synchronizationService;
            Post["/synchronize"] = p =>
            {
                var syncRequest = this.Bind<SynchronizationRequest>();
                return Synchronize(syncRequest);
            };
        }

        private SynchronizationResponse Synchronize(ISynchronizationRequest syncRequest)
        {
            var response = new SynchronizationResponse
            {
                Types = syncRequest.Types
            };

            try
            {
                response.Entities = _synchronizationService.Synchronize(syncRequest);
                response.Status = EarnItStatusCode.Ok;
                return response;
            }
            catch (Exception e)
            {
                response.Status = EarnItStatusCode.InternalServerError;
                response.Message = e.Message;
                throw;
            }
        }
    }
}