using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models;
using SQLite;

namespace Server.Data
{
    class Database
    {
        readonly SQLiteAsyncConnection database;
        public Database(string path)
        {
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<Table>().Wait();
            database.CreateTableAsync<User>().Wait();
        }
        public Task<List<Table>> GetTablesAsync()
        {
            return database.Table<Table>().ToListAsync();
        }
        public Task<Table> GetTableAsync(int id)
        {
            return database.Table<Table>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveTableAsync(Table table)
        {
            if(table.ID != 0)
            {
                return database.UpdateAsync(table);
            }
            else
            {
                return database.InsertAsync(table);
            }
        }
        public Task<int> DeleteTableAsync(Table table)
        {
            return database.DeleteAsync(table);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return database.Table<User>().ToListAsync();
        }
        public Task<User> GetUserAsync(int id)
        {
            return database.Table<User>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(User user)
        {
            if (user.ID != 0)
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
    }
}
