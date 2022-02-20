using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScheduleGenerator.Model.Input;
using ScheduleGenerator.Model.Validators;

namespace ScheduleGenerator.Services
{
    public interface IConverterService
    {
        List<Recipe> GetRecipies(string rawData);

        void ValidateRecipeTrayStarts(RecipeTrayStarts recipeTrayStarts);
    }
    
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

        public void ValidateRecipeTrayStarts(RecipeTrayStarts recipeTrayStarts)
        {
            try
            {
                var validator = new RecipeTrayStartsValidator();

                var result = validator.Validate(recipeTrayStarts);

                if (!result.IsValid)
                {
                    throw new Exception($"{result}");
                }
            }
            catch(Exception e)
            {
                throw new Exception($"GetRecipeTrayStarts exception: {e.Message}");
            }
        }
    }
}
