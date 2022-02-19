using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScheduleGenerator.Model;

namespace ScheduleGenerator.Services
{
    public interface IConverterService
    {
        List<Recipe> GetRecipies(string rawData);
    }
    
    public class ConverterService : IConverterService
    {
        public List<Recipe> GetRecipies(string rawData)
        {
            var o = JsonConvert.DeserializeObject<JObject>(rawData);

            var recipes = o.Value<JArray>("recipes").ToObject<List<Recipe>>();

            return recipes;
        }
    }
}
