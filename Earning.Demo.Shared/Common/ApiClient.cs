using Earning.Demo.Shared.Entities;
using Earning.Demo.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Earning.Demo.Shared.Common
{
    // TODO: implement fall back behavior
    internal sealed class ApiClient: IApiClient
    {
        IConfigurationService _configuration;
        //HttpClient _httpClient;

        public ApiClient(IConfigurationService Configuration)
        {
            _configuration = Configuration;
        }

        public IList<ApplicationDTO> GetAllApplications(bool isAbTesting)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(isAbTesting ? _configuration.AbApiUrl : _configuration.ApiUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = _httpClient.GetAsync("api/application").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<ApplicationDTO>>(stringData);

                return data;
            }
        }

        public bool isWorkersBusy()
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(_configuration.ApiUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = _httpClient.GetAsync("api/application/GetBusyFlag").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<bool>(stringData);

                return data;
            }
        }

        public void SetBusyFlag(bool isWorkersBusy)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(_configuration.ApiUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
                var contentString = JsonConvert.SerializeObject(new { isBusy = isWorkersBusy });
                HttpResponseMessage response = _httpClient.PostAsync("api/application/SetBusyFlag",
                    new StringContent(contentString, Encoding.UTF8, "application/json")).Result;
            }
        }

        public void Dispose()
        {
            //_httpClient.Dispose();
        }
    }
}
