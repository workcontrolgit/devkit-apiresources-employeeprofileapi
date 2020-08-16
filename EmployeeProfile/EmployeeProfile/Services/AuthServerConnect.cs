using EmployeeProfile.Contracts;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeProfile.Services
{
    public class AuthServerConnect : IAuthServerConnect
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscoveryCache _discoveryCache;
        private readonly ILogger<AuthServerConnect> _logger;
        private readonly IConfiguration _config;

        public AuthServerConnect(HttpClient httpClient, IConfiguration config, IDiscoveryCache discoveryCache, ILogger<AuthServerConnect> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _discoveryCache = discoveryCache;
            _logger = logger;
        }
        public async Task<string> RequestClientCredentialsTokenAsync()
        {

            var endPointDiscovery = await _discoveryCache.GetAsync();
            if (endPointDiscovery.IsError)
            {
                _logger.Log(LogLevel.Error, $"ErrorType: {endPointDiscovery.ErrorType} Error: {endPointDiscovery.Error}");
                throw new HttpRequestException("Something went wrong while connecting to the AuthServer Token Endpoint.");
            }

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = endPointDiscovery.TokenEndpoint,
                ClientId = _config["Client:Id"],
                ClientSecret = _config["Client:Secret"],
                Scope = _config["Client:Scope"]
            });

            if (tokenResponse.IsError)
            {
                _logger.Log(LogLevel.Error, $"ErrorType: {tokenResponse.ErrorType} Error: {tokenResponse.Error}");
                throw new HttpRequestException("Something went wrong while requesting Token to the AuthServer.");
            }

            return tokenResponse.AccessToken;
        }
    }
}
