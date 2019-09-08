using Management.Ui.Configs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Management.Ui.Services
{
    public class TokenContainer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiManagementInfo _apiManagementInfo;

        public TokenContainer(IHttpContextAccessor httpContextAccessor, ApiManagementInfo apiManagementInfo)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiManagementInfo = apiManagementInfo;
        }

        protected async Task AddRequestHeaders(HttpClient httpClient)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",_apiManagementInfo.OcpApimSubscriptionKey);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
