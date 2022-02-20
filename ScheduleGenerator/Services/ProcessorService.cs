namespace ScheduleGenerator.Services
{
    using ScheduleGenerator.Model.Input;
    using ScheduleGenerator.Model.Output;

    public interface IProcessorService
    {
        TowerSchedule Process(List<Recipe> recipes, RecipeTrayStarts recipeTrayStarts);

        Recipe GetRecipe(string recipeName, List<Recipe> recipes);

        List<WateringCommand> ProcessWateringPhases(string recipeName, int trayNumber, DateTime startDateTime, List<WateringPhase> wateringPhases);

        List<LightingCommand> ProcessLightingPhases(string recipeName, int trayNumber, DateTime startDateTime, List<LightingPhase> lightingPhases);
    }

    public class ProcessorService : IProcessorService
    {
        public TowerSchedule Process(List<Recipe> recipes, RecipeTrayStarts recipeTrayStarts)
        {
            TowerSchedule towerSchedule = new TowerSchedule()
            {
                WateringCommands = new List<WateringCommand>(),
                LightingCommands = new List<LightingCommand>()
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

        public List<WateringCommand> ProcessWateringPhases(
            string recipeName, 
            int trayNumber, 
            DateTime startDateTime, 
            List<WateringPhase> wateringPhases)
        {
            var result = new List<WateringCommand>();

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

        public List<LightingCommand> ProcessLightingPhases(
            string recipeName,
            int trayNumber,
            DateTime startDateTime,
            List<LightingPhase> lightingPhases)
        {
            var result = new List<LightingCommand>();

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

                        currentDateTime = startDateTime
                            .AddHours(hours * repetition)
                            .AddMinutes(minutes * repetition);

                        foreach (var operation in orderedOperations)
                        {
                            var amount = operation.LightIntensity;

                            var offsetHours = operation.OffsetHours;

                            var offsetMinutes = operation.OffsetMinutes;

                            var executionTime = currentDateTime
                                .AddHours(offsetHours)
                                .AddMinutes(offsetMinutes);                            

                            result.Add(new LightingCommand()
                            {
                                ExecutionDateTime = executionTime,
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
