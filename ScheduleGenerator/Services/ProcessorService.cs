namespace ScheduleGenerator.Services
{
    using ScheduleGenerator.Model.Input;

    public interface IProcessorService
    {
        void Process(List<Recipe> recipes);
    }

    public class ProcessorService : IProcessorService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipes"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Process(List<Recipe> recipes)
        {
            throw new NotImplementedException();
        }
    }
}
