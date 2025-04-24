using System.Diagnostics;
using UCEventTracker.ViewModels;

namespace UCEventTracker
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            Debug.WriteLine("MainPage OnAppearing called");
            base.OnAppearing();
            await viewModel.LoadCalendar(); 
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewEventPage));
        }
    }
}