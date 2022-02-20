namespace ScheduleGenerator.Services
{
    using ScheduleGenerator.Model.Input;
    using ScheduleGenerator.Model.Output;

    public interface IProcessorService
    {
        TowerSchedule Process(List<Recipe> recipes, RecipeTrayStarts recipeTrayStarts);

        Recipe GetRecipe(string recipeName, List<Recipe> recipes);
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
            TowerSchedule towerSchedule = new TowerSchedule()
            {
                Commands = new List<Command>()
            }; 

            foreach (var recipeTrayStart in recipeTrayStarts.Input)
            {
                var startDateTime = recipeTrayStart.StartDate;

                var recipe = GetRecipe(recipeTrayStart.RecipeName, recipes);

                towerSchedule.Commands.AddRange(ProcessWateringPhases(recipe.Name, recipeTrayStart.TrayNumber, startDateTime, recipe.WateringPhases));
            }

            return towerSchedule;
        }

        // proccess each RecipeTrayStart
        // what to return?
        public Recipe GetRecipe(string recipeName, List<Recipe> recipes)
        {
            try
            {
                return recipes.Single(r => r.Name == recipeName);
            }
            catch (Exception e)
            {
                throw new Exception($"Recipe {recipeName} not found in recipe list", e);
            }
        }

        public List<Command> ProcessWateringPhases(string recipeName, int trayNumber, DateTime startDateTime, List<WateringPhase> wateringPhases)
        {
            var result = new List<Command>();

            var orderedWateringPhases = wateringPhases.OrderBy(wp => wp.Order);

            var currentDateTime = startDateTime;

            foreach (var wateringPhase in  orderedWateringPhases)
            {
                var amount = wateringPhase.Amount;

                var hours = wateringPhase.Hours;

                var minutes = wateringPhase.Minutes;

                var repetitions = wateringPhase.Repetitions;

                for (int repetition = 0; repetition < repetitions; repetition++)
                {
                    currentDateTime = startDateTime
                        .AddHours(hours * repetition)
                        .AddMinutes(minutes * repetition);

                    result.Add(new WateringCommand()
                    {
                        ExecutionDateTime = currentDateTime,
                        TrayNumber = trayNumber,
                        RecipeName = recipeName,
                        Amount = amount,
                    });
                }
            }

            return result;
        }
    }
}
