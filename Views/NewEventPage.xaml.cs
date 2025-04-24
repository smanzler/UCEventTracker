using UCEventTracker.Models;
using UCEventTracker.ViewModels;

namespace UCEventTracker;

public partial class NewEventPage : ContentPage
{
    public NewEventPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
