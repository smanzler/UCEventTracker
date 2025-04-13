using System.Collections.ObjectModel;
using System.Windows.Input;
using UCEventTracker.Models;
using UCEventTracker.Services;

namespace UCEventTracker.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<Event> Events { get; set; } = new();

        public ICommand LoadEventsCommand { get; }
        public ICommand ToggleCompletedCommand { get; }

        public MainPageViewModel()
        {
            LoadEventsCommand = new Command(async () => await LoadEventsAsync());
            ToggleCompletedCommand = new Command<Event>(async (evt) => await ToggleCompletedAsync(evt));
        }

        private async Task LoadEventsAsync()
        {
            Events.Clear();
            var eventsFromDb = await App.Database.GetEventsAsync();
            foreach (var ev in eventsFromDb.OrderBy(e => e.Date))
                Events.Add(ev);
        }

        private async Task ToggleCompletedAsync(Event evt)
        {
            evt.IsCompleted = !evt.IsCompleted;
            await App.Database.SaveEventAsync(evt);
            await LoadEventsAsync();
        }
    }
}
