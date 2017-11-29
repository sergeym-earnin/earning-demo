using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Earning.Demo.Shared.Services;
using Earning.Demo.Shared.Entities;
using System.Linq;
using Earning.Demo.Shared.Common;

namespace Earning.Demo.Api.Controllers
{
    [Route("api/application")]
    public class ApplicationController : Controller
    {
        IApiClient _apiClient;

        public ApplicationController(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return _apiClient.GetData();
        }
    }
}
