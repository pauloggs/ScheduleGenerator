using System.Net.Http.Headers;

namespace ScheduleGenerator.Services
{
    public interface IGetData
    {
        string GetRecipeData();
    }

    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRecipeData()
        {
            var response = "Nothing";

            try
            {
                using var client = _httpClient;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = client.GetAsync("recipe").Result;


                if (responseMessage.IsSuccessStatusCode)
                {
                    response = await responseMessage.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }
    }
}
