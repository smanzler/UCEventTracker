namespace UCEventTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewEventPage), typeof(NewEventPage));
        }
    }
}
