namespace ScheduleGenerator.Services
{
    using System.Net.Http.Headers;

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
            try
            {
                using var client = _httpClient;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = client.GetAsync("recipe").Result;


                if (responseMessage.IsSuccessStatusCode)
                {
                    return await responseMessage.Content.ReadAsStringAsync();
                }

                throw new Exception("Http response is failed status");
            }
            catch (Exception ex)
            {
                return $"Exception in GetRecipeData: {ex.Message}";
            }
        }
    }
}
