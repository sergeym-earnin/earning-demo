namespace Earning.Demo.Shared.Services
{
    public interface IEnviromentService
    {
        void StartTracking(string applicationKey);
        string GetKey(string applicationKey);
    }
}