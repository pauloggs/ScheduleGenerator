using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScheduleGenerator.Model;

namespace ScheduleGenerator.Services
{
    public interface IConverterService
    {
        List<Recipe> GetRecipies(string rawData);
    }
    
    // should, return a List<Recipe> if provided with a correctly formatted rawData
    // should, throw an exception if there are any issues with deserialisation
    public class ConverterService : IConverterService
    {
        public List<Recipe> GetRecipies(string rawData)
        {
            try
            {
                var o = JsonConvert.DeserializeObject<JObject>(rawData);

                var recipes = o.Value<JArray>("recipes").ToObject<List<Recipe>>();

                return recipes;
            }
            catch (Exception e)
            {
                throw new Exception($"Exception in GetRecipies: {e.Message}");
            }            
        }
    }
}
