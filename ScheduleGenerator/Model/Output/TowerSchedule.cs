namespace ScheduleGenerator.Model.Output
{
    public class TowerSchedule
    {
        public List<Command> WateringCommands { get; set; }

        public List<Command> LightingCommands { get; set; }
    }
}
