namespace ScheduleGenerator.Model
{
    using Newtonsoft.Json.Converters;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum LightIntensity
    {
        Off = 0,
        Low = 1,
        Medium = 2,
        High = 3
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommandType
    {
        Lighting = 0,
        Watering = 1
    }
}
