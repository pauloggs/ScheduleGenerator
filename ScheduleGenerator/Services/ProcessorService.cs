namespace ScheduleGenerator.Services
{
    using ScheduleGenerator.Model.Input;
    using ScheduleGenerator.Model.Output;

    public interface IProcessorService
    {
        TowerSchedule Process(List<Recipe> recipes, RecipeTrayStarts recipeTrayStarts);

        Recipe GetRecipe(string recipeName, List<Recipe> recipes);

        List<Command> ProcessWateringPhases(string recipeName, int trayNumber, DateTime startDateTime, List<WateringPhase> wateringPhases);
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
                WateringCommands = new List<Command>(),
                LightingCommands = new List<Command>()
            }; 

            foreach (var recipeTrayStart in recipeTrayStarts.Input)
            {
                var startDateTime = recipeTrayStart.StartDate;

                var recipe = GetRecipe(recipeTrayStart.RecipeName, recipes);

                towerSchedule.WateringCommands.AddRange(ProcessWateringPhases(recipe.Name, recipeTrayStart.TrayNumber, startDateTime, recipe.WateringPhases));

                towerSchedule.LightingCommands.AddRange(ProcessLightingPhases(recipe.Name, recipeTrayStart.TrayNumber, startDateTime, recipe.LightingPhases));
            }

            return towerSchedule;
        }

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

        public List<Command> ProcessWateringPhases(
            string recipeName, 
            int trayNumber, 
            DateTime startDateTime, 
            List<WateringPhase> wateringPhases)
        {
            var result = new List<Command>();

            var orderedWateringPhases = wateringPhases.OrderBy(wp => wp.Order);

            var currentDateTime = startDateTime;

            foreach (var wateringPhase in orderedWateringPhases)
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

        public List<Command> ProcessLightingPhases(
            string recipeName,
            int trayNumber,
            DateTime startDateTime,
            List<LightingPhase> lightingPhases)
        {
            var result = new List<Command>();

            var orderedLightigPhases = lightingPhases.OrderBy(wp => wp.Order);

            var currentDateTime = startDateTime;

            foreach (var lightingPhase in orderedLightigPhases)
            {

                var hours = lightingPhase.Hours;

                var minutes = lightingPhase.Minutes;

                var repetitions = lightingPhase.Repetitions;

                if (lightingPhase.Operations != null)
                {
                    for (int repetition = 0; repetition < repetitions; repetition++)
                    {
                        var orderedOperations = lightingPhase.Operations.OrderBy(lp => lp.OffsetHours);

                        foreach (var operation in orderedOperations)
                        {
                            var amount = operation.LightIntensity;

                            var offsetHours = operation.OffsetHours;

                            var offsetMinutes = operation.OffsetMinutes;

                            currentDateTime = startDateTime
                            .AddHours(hours * repetition + offsetHours)
                            .AddMinutes(minutes * repetition + offsetMinutes);

                            result.Add(new LightingCommand()
                            {
                                ExecutionDateTime = currentDateTime,
                                TrayNumber = trayNumber,
                                RecipeName = recipeName,
                                LightIntensity = operation.LightIntensity
                            });
                        }
                    } 
                }                
            }

            return result;
        }
    }
}
