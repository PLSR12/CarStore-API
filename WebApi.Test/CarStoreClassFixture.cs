using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test
{
    public class CarStoreClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public CarStoreClassFixture(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }


        protected async Task<HttpResponseMessage> DoDelete(string method, string token = "", string culture = "en")
        {
            AuthorizeRequest(token);

            return await _httpClient.DeleteAsync(method);
        }

        protected async Task<HttpResponseMessage> DoPost(string method, object request, string token = "")
        {
            AuthorizeRequest(token);
            return await _httpClient.PostAsJsonAsync(method, request);
        }

        protected async Task<HttpResponseMessage> DoPut(string method, object request, string token = "")
        {
            AuthorizeRequest(token);
            return await _httpClient.PutAsJsonAsync(method, request);
        }
        protected async Task<HttpResponseMessage> DoGet(string method, string token = "")
        {
            AuthorizeRequest(token);
            return await _httpClient.GetAsync(method);
        }

        private void AuthorizeRequest(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        }
    }
}
