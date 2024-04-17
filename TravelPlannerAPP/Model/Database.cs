using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerAPP.Model
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(int id)
        {
            return database.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task SaveUserAsync(User user)
        {
            if(user.Id != 0)
            {
                return database.UpdateAsync(user);
            }
            else
            {
                return database.InsertAsync(user);
            }
        }
        public Task<int> DeleteUserAsync(User user)
        {
            return database.DeleteAsync(user);
        }
        public async Task<(bool isAuthenticated, int userId)> AuthenticateUserAsync(string email, string password)
        {
            var user = await database.Table<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            if(user != null)
            {
                return (true, user.Id);
            }else { return (false, -1); }
        }
    }
}
