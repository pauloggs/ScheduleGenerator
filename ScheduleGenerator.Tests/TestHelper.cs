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
    }
}
