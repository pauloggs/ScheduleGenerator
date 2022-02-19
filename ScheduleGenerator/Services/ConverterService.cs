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
            throw new NotImplementedException();
        }
    }
}
