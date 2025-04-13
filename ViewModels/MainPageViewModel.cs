using System.Collections.ObjectModel;
using UCEventTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace UCEventTracker.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Event> events = new();

    public MainPageViewModel()
    {
        LoadEventsCommand = new AsyncRelayCommand(LoadEventsAsync);
        ToggleCompletedCommand = new AsyncRelayCommand<Event>(ToggleCompletedAsync);
    }

    public IAsyncRelayCommand LoadEventsCommand { get; }
    public IAsyncRelayCommand<Event> ToggleCompletedCommand { get; }

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
