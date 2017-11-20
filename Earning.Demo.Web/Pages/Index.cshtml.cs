using Microsoft.AspNetCore.Mvc.RazorPages;
using Earning.Demo.Shared;

namespace Earning.Demo.Web.Pages
{
    public class IndexModel : PageModel
    {
        public IConfigurationService Configuration = new ConfigurationService();

        public void OnGet()
        {

        }
    }
}
