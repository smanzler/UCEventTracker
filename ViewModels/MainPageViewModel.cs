using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UCEventTracker.Models;

namespace UCEventTracker.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Event> events = new();

    [ObservableProperty]
    private ObservableCollection<Event> importantTasks = new();

    [ObservableProperty]
    private ObservableCollection<DayViewModel> calendarDays = new();

    public MainPageViewModel()
    {
        ToggleCompletedCommand = new AsyncRelayCommand<Event>(ToggleCompletedAsync);
        LoadCalendar();
    }

    public IAsyncRelayCommand LoadEventsCommand { get; }
    public IAsyncRelayCommand<Event> ToggleCompletedCommand { get; }

    private async Task ToggleCompletedAsync(Event evt)
    {
        evt.IsCompleted = !evt.IsCompleted;
    }

    public void LoadCalendar()
    {
        CalendarDays.Clear();

        var today = DateTime.Today;
        var firstOfMonth = new DateTime(today.Year, today.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
        int startDayOfWeek = (int)firstOfMonth.DayOfWeek;
        int totalCells = 30;

        for (int i = 0; i < totalCells; i++)
        {
            var dayView = new DayViewModel();

            if (i >= startDayOfWeek && (i - startDayOfWeek + 1) <= daysInMonth)
            {
                int day = i - startDayOfWeek + 1;
                dayView.DayNumber = day;
                dayView.Date = new DateTime(today.Year, today.Month, day);
                dayView.Events = Events.Where(e => e.Date.Date == dayView.Date.Date).ToList();
            }
            else
            {
                dayView.DayNumber = 0;
            }

            CalendarDays.Add(dayView);
        }
    }

}
