namespace ScheduleGenerator.Tests
{
    using Newtonsoft.Json;
    using ScheduleGenerator.Model.Input;
    using System.Collections.Generic;

    public static class TestHelper
    {

        public static List<Recipe> TestRecipeList()
        {
            var recipeString = "[{\"name\": \"Basil\",\"lightingPhases\": [{\"operations\": [{\"offsetHours\": 0,\"offsetMinutes\": 0,\"lightIntensity\": 1},{\"offsetHours\": 6,\"offsetMinutes\": 0,\"lightIntensity\": 2},{\"offsetHours\": 12,\"offsetMinutes\": 0,\"lightIntensity\": 3},{\"offsetHours\": 16,\"offsetMinutes\": 0,\"lightIntensity\": 0}],\"name\": \"LightingPhase 1\",\"order\": 0,\"hours\": 24,\"minutes\": 0,\"repetitions\": 5}],\"wateringPhases\": [{\"amount\": 100,\"name\": \"Watering Phase 1\",\"order\": 0,\"hours\": 24,\"minutes\": 0,\"repetitions\": 5}]},{\"name\": \"Strawberries\",\"lightingPhases\": [{\"operations\": [{\"offsetHours\": 0,\"offsetMinutes\": 0,\"lightIntensity\": 3},{\"offsetHours\": 20,\"offsetMinutes\": 0,\"lightIntensity\": 0}],\"name\": \"Phase 3\",\"order\": 0,\"hours\": 24,\"minutes\": 0,\"repetitions\": 5},{\"operations\": [{\"offsetHours\": 0,\"offsetMinutes\": 0,\"lightIntensity\": 1},{\"offsetHours\": 6,\"offsetMinutes\": 0,\"lightIntensity\": 2},{\"offsetHours\": 12,\"offsetMinutes\": 0,\"lightIntensity\": 3},{\"offsetHours\": 16,\"offsetMinutes\": 30,\"lightIntensity\": 2},{\"offsetHours\": 24,\"offsetMinutes\": 30,\"lightIntensity\": 1},{\"offsetHours\": 30,\"offsetMinutes\": 0,\"lightIntensity\": 0}],\"name\": \"Phase 2\",\"order\": 1,\"hours\": 36,\"minutes\": 30,\"repetitions\": 10},{\"operations\": [{\"offsetHours\": 0,\"offsetMinutes\": 0,\"lightIntensity\": 1},{\"offsetHours\": 6,\"offsetMinutes\": 0,\"lightIntensity\": 2},{\"offsetHours\": 12,\"offsetMinutes\": 0,\"lightIntensity\": 0}],\"name\": \"Phase 3\",\"order\": 2,\"hours\": 24,\"minutes\": 0,\"repetitions\": 2}],\"wateringPhases\": [{\"amount\": 0,\"name\": \"Phase 1\",\"order\": 0,\"hours\": 24,\"minutes\": 0,\"repetitions\": 5},{\"amount\": 55,\"name\": \"Phase 2\",\"order\": 1,\"hours\": 24,\"minutes\": 0,\"repetitions\": 6},{\"amount\": 30,\"name\": \"Phase 3\",\"order\": 3,\"hours\": 24,\"minutes\": 0,\"repetitions\": 2},{\"amount\": 30,\"name\": \"Phase 4\",\"order\": 2,\"hours\": 12,\"minutes\": 30,\"repetitions\": 4}]}]";

            return JsonConvert.DeserializeObject<List<Recipe>>(recipeString);
        }

        public static RecipeTrayStarts TestRecipeTrayStarts()
        {
            var recipeTrayStarts = "{input:[{trayNumber:1,recipeName:\"Basil\",startDate:\"2022-01-24T12:30:00.0000000Z\"},{trayNumber:2,recipeName:\"Strawberries\",startDate:\"2021-13-08T17:33:00.0000000Z\"},{trayNumber:3,recipeName:\"Basil\",startDate:\"2030-01-01T23:45:00.0000000Z\"}]}";
        
            return JsonConvert.DeserializeObject<RecipeTrayStarts>(recipeTrayStarts);
        }

        public static List<WateringPhase> TestWateringPhases(int numberOfPhases, short numberOfRepetitions)
        {
            var wateringPhases = new List<WateringPhase>();

            for (short i = 1; i <= numberOfPhases; i++)
            {
                var wateringPhase = new WateringPhase(
                    Name: $"WateringPhase_{i}",
                    Order: i,
                    Hours: 24,
                    Minutes: 0,
                    Amount: 100,
                    Repetitions: numberOfRepetitions);

                wateringPhases.Add(wateringPhase);
            }

            return wateringPhases;
        }

        public static List<LightingPhase> TestLightingPhases(int numberOfPhases, short numberOfRepetitions, short numberOfOperations)
        {
            var lightingPhases = new List<LightingPhase>();

            for (short i = 1; i <= numberOfPhases; i++)
            {
                var lightingPhase = new LightingPhase(
                    Name: $"LightingPhase_{i}",
                    Order: i,
                    Hours: 24,
                    Minutes: 0,
                    Repetitions: numberOfRepetitions
                    );

                lightingPhases.Add(lightingPhase);

                if (numberOfOperations > 0)
                {
                    var ops = new LightingPhaseOperation[numberOfOperations];

                    for (short j = 0; j < numberOfOperations; j++)
                    {
                        ops[j] = new LightingPhaseOperation(
                            OffsetHours: j,
                            OffsetMinutes: 0,
                            LightIntensity: (LightIntensity)(j % 4)
                            );
                    }

                    lightingPhase.Operations = ops;
                }
            }

            return lightingPhases;
        }
    }
}
