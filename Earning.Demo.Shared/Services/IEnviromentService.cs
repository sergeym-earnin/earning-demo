namespace Earning.Demo.Shared.Services
{
    public interface IEnviromentService
    {
        void StartTracking(string applicationKey);
        string GetDataKey(string applicationKey);
    }
}