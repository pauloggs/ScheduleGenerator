namespace ScheduleGenerator.Model.Input
{
    public class RecipeTrayStarts
    {
        public List<RecipeTrayStart> Input { get; set; }        
    }

    public  class RecipeTrayStart
    {
        private DateTime startDate;

        public int TrayNumber { get; set; }

        public string RecipeName { get; set; }

        public DateTime StartDate/* { get; set; }*/
        {
            get { return startDate; }
            set { startDate = value.ToUniversalTime(); }
        }
    }
}
