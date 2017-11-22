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
        IConfigurationService _configurationService;

        public IEnumerable<IGrouping<string, ApplicationDTO>> Applications;

        [BindProperty]
        public bool isAbTesting { get; set; }
        [BindProperty]
        public bool isWorkersBusy { get; set; }

        public IndexModel(IApiClient apiClient, IConfigurationService configurationService)
        {
            _apiClient = apiClient;
            _configurationService = configurationService;
        }

        public void OnGet()
        {
            isAbTesting = _configurationService.IsAbTesting;
            isWorkersBusy = _apiClient.isWorkersBusy();
            Applications = _apiClient.GetAllApplications(isAbTesting).GroupBy(i => i.ApplicationType);
        }

        public IActionResult OnPost()
        {
            // Re-factor to storage;
            _configurationService.IsAbTesting = isAbTesting;
            _apiClient.SetBusyFlag(isWorkersBusy);
            return RedirectToPage("./Index");
        }
    }
}
