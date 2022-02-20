namespace ScheduleGenerator.Model.Output
{
    public abstract class Command
    {
        private DateTime executionDateTime;

        public DateTime ExecutionDateTime
        {
            get { return executionDateTime; }
            set { executionDateTime = value.ToUniversalTime(); }
        }

        public int TrayNumber { get; set; }

        public string? RecipeName { get; set; }
    }
}
