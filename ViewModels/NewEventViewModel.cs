using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using UCEventTracker.Models;
using UCEventTracker.Services;

namespace UCEventTracker.ViewModels
{
    public partial class NewEventViewModel : ObservableObject
    {
        [ObservableProperty] private string title;
        [ObservableProperty] private string description;
        [ObservableProperty] private DateTime date = DateTime.Today;
        [ObservableProperty] private bool isImportant;
        [ObservableProperty] private bool isPersonal;

        private readonly EventDatabase _database;

        public NewEventViewModel(EventDatabase database)
        {
            _database = database;
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

            Debug.WriteLine($"Event saved: {newEvent.Title}, {newEvent.Description}");

            await Shell.Current.Navigation.PopAsync();
        }
    }
}