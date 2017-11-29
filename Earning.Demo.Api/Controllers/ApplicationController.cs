using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Earning.Demo.Api.Services;
using Earning.Demo.Shared.Services;
using Earning.Demo.Shared.Entities;
using System.Linq;

namespace Earning.Demo.Api.Controllers
{
    [Route("api/application")]
    public class ApplicationController : Controller
    {
        IStorageService _storageService;
        IConfigurationService _configuration;

        public ApplicationController(IStorageService storageService, IConfigurationService configuration)
        {
            _storageService = storageService;
            _configuration = configuration;
        }

        [HttpGet]
        public IList<ApplicationDTO> Get()
        {
            return _storageService.GetAll();
        }

        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return _storageService.GetAll()
              .FirstOrDefault( i => string.IsNullOrEmpty(i.NodeName))?.Data;
        }

        [HttpPost]
        [Route("SetBusyFlag")]
        public void SetBusyFlag([FromBody] Busy data)
        {
            _storageService.SetBusyFlag(data.isBusy);
        }

        [HttpGet]
        [Route("GetBusyFlag")]
        public bool GetBusyFlag()
        {
            return _storageService.GetBusyFlag();
        }
    }

    public class Busy
    {
        public bool isBusy { get; set; }
    }
}
