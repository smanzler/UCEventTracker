using UCEventTracker.Models;

namespace UCEventTracker.ViewModels
{
    public class DayViewModel
    {
        public int DayNumber { get; set; }
        public DateTime Date { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
    }
}
