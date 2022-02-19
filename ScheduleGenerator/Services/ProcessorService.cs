namespace ScheduleGenerator.Services
{
    using ScheduleGenerator.Model;

    public interface IProcessorService
    {
        void Process(List<Recipe> recipes);
    }

    public class ProcessorService : IProcessorService
    {
        public void Process(List<Recipe> recipes)
        {
            throw new NotImplementedException();
        }
    }
}
