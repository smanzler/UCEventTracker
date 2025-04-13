using UCEventTracker.ViewModels;

namespace UCEventTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            this.viewModel = viewModel;
        }

        private readonly MainPageViewModel viewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadEventsCommand.Execute(null);
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEventPage());
        }
    }

}
