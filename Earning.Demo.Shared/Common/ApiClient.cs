using Earning.Demo.Shared.Entities;
using Earning.Demo.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Polly.Wrap;
using Polly;
using System.Threading.Tasks;
using System.IO;

namespace Earning.Demo.Shared.Common
{
    // TODO: Use Polly instead of custom fallback implementation
    sealed class ApiClient: IApiClient
    {
        IHttpContextAccessor _contextAccessor;
        IConfigurationService _configuration;
        PolicyWrap _policyWrapper;
        HttpClient _httpClient;

        public ApiClient(Policy[] policies, IConfigurationService Configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = Configuration;
            _contextAccessor = contextAccessor;
            _policyWrapper = Policy.WrapAsync(policies);

            SetBaseSettings();
        }

        void SetBaseSettings(bool ignoreAbTesting = false)
        {
            _httpClient = new HttpClient();
            string uri = _configuration.ApiUrl;
            if (!ignoreAbTesting &&_contextAccessor.HttpContext.Request.Query.TryGetValue("isAbTesting", out StringValues value) &&
               value.Count == 1 && bool.TryParse(value[0], out bool result) && result)
            {
                uri = _configuration.AbApiUrl;
            }
            _httpClient.BaseAddress = new Uri(uri);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        async Task<T> HttpInvoker<T>(string url, Func<Task<T>> action)
        {
            //return _policyWrapper.ExecuteAsync(() => action());
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                var uri = Path.Combine(_httpClient.BaseAddress.OriginalString, url);
                Console.WriteLine($"[FALLBACK] ({uri}) {e.Message}");
                _httpClient.Dispose();
                SetBaseSettings(true);
                return await action();
            }
        }

        async Task HttpInvoker(string url, Func<Task> action)
        {
            //return _policyWrapper.ExecuteAsync(() => action());
            try
            {
                await action();
            }
            catch (Exception e)
            {
                var uri = Path.Combine(_httpClient.BaseAddress.OriginalString, url);
                Console.WriteLine($"[FALLBACK] ({uri}) {e.Message}");
                _httpClient.Dispose();
                SetBaseSettings(true);
                await action();
            }
        }

        Task<T> GetAsync<T>(string url)
        {
            return HttpInvoker(url, async () =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _httpClient.SendAsync(requestMessage);
                string stringData = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(stringData);
            });
        }

        Task PostAsync<P> (string url, P data)
        {
            return HttpInvoker(url, async () =>
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                string stringData = await response.Content.ReadAsStringAsync();
            });
        }

        public IList<ApplicationDTO> GetAllApplications()
        {
            return GetAsync<IList<ApplicationDTO>>("api/application").Result;
        }

        public bool isWorkersBusy()
        {
            return GetAsync<bool>("api/application/GetBusyFlag").Result;
        }

        public void SetBusyFlag(bool isWorkersBusy)
        {
            PostAsync("api/application/SetBusyFlag", new { isBusy = isWorkersBusy }).Wait();
        }

        public string GetData()
        {
            return GetAsync<string>("api/application/GetData").Result;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
