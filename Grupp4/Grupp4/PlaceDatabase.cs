using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
namespace Grupp4
{
    public class PlaceDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public PlaceDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Place>();
        }
        public Task<List<Place>> GetPlacesAsync()
        {
            return _database.Table<Place>().ToListAsync();

        }

        public Task<int> SavePlaceAsync(Place place)
        {
            return _database.InsertAsync(place);
        }

        public Task<int> DeletePlaceAsync(Place place)
        {
            return _database.DeleteAsync(place);
        }
    }
}
