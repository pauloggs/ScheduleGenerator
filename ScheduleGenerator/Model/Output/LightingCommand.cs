namespace ScheduleGenerator.Model.Output
{
    public class LightingCommand : Command
    {
        public LightIntensity LightIntensity { get; set; }

        public LightingCommand()
        {
            CommandType = CommandType.Lighting;
        }
    }
}
