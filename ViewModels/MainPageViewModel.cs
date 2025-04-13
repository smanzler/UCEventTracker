using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UCEventTracker.Models;
using UCEventTracker.Services;

namespace UCEventTracker.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Event> events = new();

    private readonly EventDatabase _database;

    public MainPageViewModel(EventDatabase database)
    {
        _database = database;
        LoadEventsCommand = new AsyncRelayCommand(LoadEventsAsync);
        ToggleCompletedCommand = new AsyncRelayCommand<Event>(ToggleCompletedAsync);
    }

    public IAsyncRelayCommand LoadEventsCommand { get; }
    public IAsyncRelayCommand<Event> ToggleCompletedCommand { get; }

    private async Task LoadEventsAsync()
    {
        var eventsFromDb = await _database.GetEventsAsync();
        Events.Clear();

        foreach (var ev in eventsFromDb.OrderBy(e => e.Date))
            Events.Add(ev);
    }

    private async Task ToggleCompletedAsync(Event evt)
    {
        evt.IsCompleted = !evt.IsCompleted;
        await _database.SaveEventAsync(evt);
        await LoadEventsAsync();
    }
}
