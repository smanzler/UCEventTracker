using UCEventTracker.Services;

namespace UCEventTracker
{
    public partial class App : Application
    {
        private static EventDatabase _database;

        public static EventDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "events.db3");
                    _database = new EventDatabase(dbPath);
                }
                return _database;
            }
        }

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}