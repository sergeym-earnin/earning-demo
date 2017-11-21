using Earning.Demo.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Earning.Demo.Web.Services
{
    public class ApplicationProxy
    {
        IConfigurationProvider _configuration;

        public ApplicationProxy(IConfigurationProvider Configuration)
        {
            _configuration = Configuration;
        }

        public IList<ApplicationDTO> GetAllApplication()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.ApiUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("api/values").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<ApplicationDTO>>(stringData);

                return data;
            }
        }
    }

    public class ApplicationDTO
    {
        public string ApplicationType { get; set; }
        public string NodeName { get; set; }
        public string ApplicationId { get; set; }
        public ConfigurationProvider Configuration { get; set; }
        public String Data { get; set; }
    }
}
