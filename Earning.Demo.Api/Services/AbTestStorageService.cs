using Earning.Demo.Shared.Entities;
using Earning.Demo.Shared.Services;
using System.Collections.Generic;
using System.Linq;

namespace Earning.Demo.Api.Services
{

    internal sealed class AbTestStorageService : StorageService
    {
        public AbTestStorageService(IConfigurationService configuration)
            :base(configuration)
        {
        }

        public override List<ApplicationDTO> GetAll()
        {
            var results = base.GetAll();
            results = results.Select(i =>
            {
                i.ApplicationType += "-AB-Testing";
                return i;
            }).ToList();
            return results;
        }
    }
}
