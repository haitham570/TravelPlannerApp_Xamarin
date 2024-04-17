using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerAPP.Model
{
    public class TripDatabase
    {
        readonly SQLiteAsyncConnection tripDB;

        public TripDatabase(string dbPath)
        {
            tripDB = new SQLiteAsyncConnection(dbPath);
            tripDB.CreateTableAsync<Trip>().Wait();
        }

        public Task<List<Trip>> GetTripsAsync()
        {
            return tripDB.Table<Trip>().ToListAsync();
        }
        public Task<List<Trip>> GetUpcomingTripAsync(int userId)
        {
            DateTime currentDate = DateTime.Today;
            return tripDB.Table<Trip>().Where(t => t.StartDate >= currentDate && t.UserId == userId).ToListAsync();
        }

        public Task<int> SaveTripAsync(Trip trip)
        {
            if(trip.Id != 0)
            {
                return tripDB.UpdateAsync(trip);
            }
            else
            {
                return tripDB.InsertAsync(trip);
            }
        }

        public Task<int> DeleteTripAsync(Trip trip)
        {
            return tripDB.DeleteAsync(trip);
        }

    }
}
