using MyCookBook.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace WebApi.Test.V1
{
    public class ControllerBase : IClassFixture<MyCookBookWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ControllerBase(MyCookBookWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            ResourceErroMessages.Culture = CultureInfo.CurrentCulture;
        }

        protected async Task<HttpResponseMessage> PostRequest(string method, object body) 
        {
            var jsonString = JsonConvert.SerializeObject(body);
            
            return await _client.PostAsync(method, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
    }
}
