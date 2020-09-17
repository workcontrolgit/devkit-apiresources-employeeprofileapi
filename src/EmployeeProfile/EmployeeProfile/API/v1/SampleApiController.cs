using EmployeeProfile.Contracts;
using EmployeeProfile.DTO.Request;
using EmployeeProfile.DTO.Response;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace EmployeeProfile.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SampleApiController : ControllerBase
    {
        private readonly ILogger<SampleApiController> _logger;
        private readonly IApiConnect _sampleApiConnect;

        public SampleApiController(IApiConnect sampleApiConnect, ILogger<SampleApiController> logger)
        {
            _sampleApiConnect = sampleApiConnect;
            _logger = logger;
        }

        [Route("{id:long}")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<ApiResponse> Get(long id)
        {
            return new ApiResponse(await _sampleApiConnect.GetDataAsync<SampleQueryResponse>($"/api/v1/sample/{id}"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] SampleRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            return new ApiResponse(await _sampleApiConnect.PostDataAsync<SampleQueryResponse, SampleRequest>("/api/v1/sample", createRequest));
        }
    }
}
