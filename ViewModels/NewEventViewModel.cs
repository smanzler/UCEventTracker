using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UCEventTracker.Models;

namespace UCEventTracker.ViewModels
{
    public partial class NewEventViewModel : ObservableObject
    {

        private readonly MainPageViewModel _mainViewModel;

        public NewEventViewModel(MainPageViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        [ObservableProperty] private string title;
        [ObservableProperty] private string description;
        [ObservableProperty] private DateTime date = DateTime.Today;
        [ObservableProperty] private bool isImportant;
        [ObservableProperty] private bool isPersonal;

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

            _mainViewModel.Events.Add(newEvent);
            _mainViewModel.LoadCalendar();

            Console.WriteLine($"Event saved: {newEvent.Title}, {newEvent.Description}");

            await Shell.Current.GoToAsync("..");
        }
    }
}