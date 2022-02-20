namespace ScheduleGenerator.Model.Output
{
    using ScheduleGenerator.Model.Input;

    public class LightingCommand : Command
    {
        public LightIntensity LightIntensity { get; set; }
    }
}
