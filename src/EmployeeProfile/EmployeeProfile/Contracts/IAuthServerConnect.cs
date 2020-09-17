using System.Threading.Tasks;

namespace EmployeeProfile.Contracts
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
