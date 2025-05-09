﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using UCEventTracker.Models;
using UCEventTracker.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UCEventTracker.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty] private string title;
    [ObservableProperty] private string description;
    [ObservableProperty] private DateTime date = DateTime.Today;
    [ObservableProperty] private bool isImportant;
    [ObservableProperty] private bool isPersonal;

    [ObservableProperty]
    private ObservableCollection<Event> events = new();

    [ObservableProperty]
    private ObservableCollection<Event> importantTasks = new();

    [ObservableProperty]
    private ObservableCollection<DayViewModel> calendarDays = new();

    private readonly EventDatabase _database;

    public MainPageViewModel(EventDatabase database)
    {
        _database = database;
        LoadEventsCommand = new AsyncRelayCommand(LoadEventsAsync);
        ToggleCompletedCommand = new AsyncRelayCommand<Event>(ToggleCompletedAsync);
        LoadCalendar();
    }

    public IAsyncRelayCommand LoadEventsCommand { get; }
    public IAsyncRelayCommand<Event> ToggleCompletedCommand { get; }

    private async Task LoadEventsAsync()
    {
        var eventsFromDb = await _database.GetEventsAsync();
        Events.Clear();
        ImportantTasks.Clear();

        foreach (var ev in eventsFromDb.OrderBy(e => e.Date))
        {
            Events.Add(ev);

            if (ev.IsImportant && !ev.IsCompleted)
            {
                ImportantTasks.Add(ev);
            }

            Debug.WriteLine($"Event Loaded: {ev.Title}, Important: {ev.IsImportant}, Completed: {ev.IsCompleted}");
        }
    }

    private async Task ToggleCompletedAsync(Event evt)
    {
        evt.IsCompleted = !evt.IsCompleted;
        await _database.SaveEventAsync(evt);
        await LoadEventsAsync();
    }

    public async Task LoadCalendar()
    {
        await LoadEventsAsync();

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

    [RelayCommand]
    private async Task SaveEvent()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter a title.", "OK");
            return;
        }

        var newEvent = new Event
        {
            Title = Title,
            Description = Description,
            Date = Date,
            IsImportant = IsImportant,
            IsPersonal = IsPersonal,
            IsCompleted = false
        };

        await _database.SaveEventAsync(newEvent);

        Title = string.Empty;
        Description = string.Empty;
        Date = DateTime.Today;
        IsImportant = false;
        IsPersonal = false;

        Debug.WriteLine($"Event saved: {newEvent.Title}, {newEvent.Description}");

        await Shell.Current.Navigation.PopAsync();
    }
}