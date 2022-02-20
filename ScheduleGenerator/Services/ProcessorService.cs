namespace ScheduleGenerator.Services
{
    using ScheduleGenerator.Model.Input;
    using ScheduleGenerator.Model.Output;

    public interface IProcessorService
    {
        TowerSchedule Process(List<Recipe> recipes, RecipeTrayStarts recipeTrayStarts);
    }

    public class ProcessorService : IProcessorService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipes"></param>
        /// <exception cref="NotImplementedException"></exception>
        public TowerSchedule Process(List<Recipe> recipes, RecipeTrayStarts recipeTrayStarts)
        {
            throw new NotImplementedException();
        }
    }
}
