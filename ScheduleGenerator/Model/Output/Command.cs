namespace ScheduleGenerator.Model.Output
{
    public abstract class Command
    {
        public DateTime ExecutionDateTime { get; set; }

        public int TrayNumber { get; set; }

        public string? RecipeName { get; set; }
    }
}
