using Management.Ui.Configs;
using Management.Ui.Services.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Management.Ui.Services
{
    public class UserManagementHttp : TokenContainer, IUserManagement
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserManagementHttp> _logger;

        public UserManagementHttp(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ApiManagementInfo apiManagementInfo,
            ILogger<UserManagementHttp> logger) : base(httpContextAccessor, apiManagementInfo)
        {

            _httpClient = httpClient;
            _logger = logger;
        }



        public async Task<IList<TenantViewDao>> GetTenantsAsync()
        {
            IList<TenantViewDao> result = null;
            _logger.LogInformation("Retrieve all tenant who user has access to");
            using (var response = await HttpGetAsync("tenants"))
            {

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var respContentAsString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<IList<TenantViewDao>>(respContentAsString);
                }
                else if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return null;
                }
                else
                {
                    await ThrowException(response);
                }

            }
            return result;
        }

        private async Task<HttpResponseMessage> HttpGetAsync(string requestUri)
        {
            _logger.LogInformation($"Calling rest api with following request: {requestUri}");

            if (_httpClient == null)
            {
                _logger.LogError("Http client is null");
            }

            await AddRequestHeaders(_httpClient);
            return await _httpClient.GetAsync(requestUri);

        }

        private async Task ThrowException(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to execute this operation");
            }
            var resAsString = await response.Content.ReadAsStringAsync();
            throw new Exception(resAsString);
        }


    }
}
