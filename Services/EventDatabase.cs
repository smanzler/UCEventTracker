using SQLite;
using UCEventTracker.Models;

namespace UCEventTracker.Services
{
    public class EventDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public EventDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Event>().Wait();
        }

        public Task<List<Event>> GetEventsAsync()
        {
            return _database.Table<Event>().ToListAsync();
        }

        public Task<int> SaveEventAsync(Event plannerEvent)
        {
            if (plannerEvent.Id != 0)
                return _database.UpdateAsync(plannerEvent);
            else
                return _database.InsertAsync(plannerEvent);
        }

        public Task<int> DeleteEventAsync(Event plannerEvent)
        {
            return _database.DeleteAsync(plannerEvent);
        }
    }
}
