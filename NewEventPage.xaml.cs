using UCEventTracker.Models;

namespace UCEventTracker;

public partial class NewEventPage : ContentPage
{
    public NewEventPage()
    {
        InitializeComponent();
        DatePicker.Date = DateTime.Today;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a title.", "OK");
            return;
        }

        var newEvent = new Event
        {
            Title = TitleEntry.Text,
            Description = DescriptionEditor.Text,
            Date = DatePicker.Date,
            IsImportant = IsImportantCheck.IsChecked,
            IsPersonal = IsPersonalCheck.IsChecked,
            IsCompleted = false
        };

        await App.Database.SaveEventAsync(newEvent);

        await Navigation.PopAsync(); 
    }
}
