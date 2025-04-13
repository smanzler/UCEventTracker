namespace UCEventTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Add", "Add Event Clicked!", "OK");
        }
    }

}
