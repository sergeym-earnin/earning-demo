using Microsoft.AspNetCore.Mvc.RazorPages;
using Earning.Demo.Shared;
using Earning.Demo.Web.Services;
using System.Collections.Generic;
using System.Linq;

namespace Earning.Demo.Web.Pages
{
    public class IndexModel : PageModel
    {
        public IList<IGrouping<string, ApplicationDTO>> Applications;

        public IConfigurationProvider Configuration;

        public void OnGet()
        {
            Configuration = new ConfigurationProvider();
            var proxy = new ApplicationProxy(Configuration);
            Applications = proxy.GetAllApplication().GroupBy( i => i.ApplicationType).ToList();
        }
    }
}
