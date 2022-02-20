namespace ScheduleGenerator.Model.Output
{
    public class WateringCommand : Command
    {
        public int Amount { get; set; }

        public WateringCommand()
        {
            CommandType = CommandType.Watering;
        }
    }
}
