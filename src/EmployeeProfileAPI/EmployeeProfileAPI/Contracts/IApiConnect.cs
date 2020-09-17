using System.Threading.Tasks;

namespace EmployeeProfile.Contracts
{
    public interface IApiConnect
    {
        Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, TRequest request);
        Task<TResponse> GetDataAsync<TResponse>(string endPoint);
    }
}
