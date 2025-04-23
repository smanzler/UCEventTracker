using UCEventTracker.Models;
using UCEventTracker.ViewModels;

namespace UCEventTracker;

public partial class NewEventPage : ContentPage
{
    public NewEventPage(NewEventViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
