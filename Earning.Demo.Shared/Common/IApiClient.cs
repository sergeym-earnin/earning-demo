using Earning.Demo.Shared.Entities;
using System;
using System.Collections.Generic;

namespace Earning.Demo.Shared.Common
{
    public interface IApiClient: IDisposable
    {
        IList<ApplicationDTO> GetAllApplications(bool isAbTesting);
        bool isWorkersBusy();
        void SetBusyFlag(bool isWorkersBusy);
    }
}