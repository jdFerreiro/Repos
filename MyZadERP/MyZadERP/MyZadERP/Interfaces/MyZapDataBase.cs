using System.Collections.Generic;
using System.Threading.Tasks;
using MyZadERP.ViewModels.DTO;
using SQLite;


namespace MyZadERP.Interfaces
{
    public class MyZapDataBase
    {
        readonly SQLiteAsyncConnection database;
        public MyZapDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UserDTO>().Wait();
        }

        public Task<int> SaveUserAsync(UserDTO userInfo)
        {
            var _user = database.Table<UserDTO>().Where(i => i.UserName == userInfo.UserName).FirstOrDefaultAsync();
            if (_user.Result != null)
            {
                return  database.UpdateAsync(userInfo);
            }
            else
            {
                return database.InsertAsync(userInfo);
            }
        }

        public  Task<UserDTO> GetUserAsync()
        {
            return database.Table<UserDTO>().FirstOrDefaultAsync();
        }

    }
}
