using Earning.Demo.Shared.Entities;
using System;
using System.Collections.Generic;

namespace Earning.Demo.Api.Services
{
    public interface IStorageService: IDisposable
    {
        void Increment(string key, int incrementValue);
        string Get(string key);
        List<ApplicationDTO> GetAll();
        void SetBusyFlag(bool isBusy);
        bool GetBusyFlag();
    }
}
