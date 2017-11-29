using Earning.Demo.Shared.Common;
using Earning.Demo.Shared.Entities;
using Earning.Demo.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Earning.Demo.Web.Pages
{
    public class IndexModel : PageModel
    {
        IApiClient _apiClient;

        public IEnumerable<IGrouping<string, ApplicationDTO>> Applications;
        public string Counter;

        [BindProperty]
        public bool isAbTesting { get; set; }
        [BindProperty]
        public bool isWorkersBusy { get; set; }

        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public void OnGet(bool isAbTesting)
        {
            isWorkersBusy = _apiClient.isWorkersBusy();
            var applications = _apiClient.GetAllApplications();
            Applications = applications
                .Where(i => !string.IsNullOrEmpty(i.NodeName))
                .GroupBy(i => i.NodeName);
            Counter = applications.FirstOrDefault(i => string.IsNullOrEmpty(i.NodeName) &&
                                                  i.Data != "BLA")?.Data;
        }

        public IActionResult OnPost()
        {
            _apiClient.SetBusyFlag(isWorkersBusy);
            return RedirectToPage("/Index", "OnGet", new { isAbTesting = isAbTesting });
        }
    }
}
