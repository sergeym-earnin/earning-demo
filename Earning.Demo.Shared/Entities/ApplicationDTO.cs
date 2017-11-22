using Earning.Demo.Shared.Services;
using System;

namespace Earning.Demo.Shared.Entities
{
    public class ApplicationDTO
    {
        public string ApplicationType { get; set; }
        public string NodeName { get; set; }
        public string ApplicationId { get; set; }
        public ConfigurationDTO Configuration { get; set; }
        public String Data { get; set; }
    }
}
