namespace ScheduleGenerator.Model.Output
{
    public class TowerSchedule
    {
        public List<WateringCommand> WateringCommands { get; set; }

        public List<LightingCommand> LightingCommands { get; set; }
    }
}
