using SQLite;

namespace TravelApp
{
    public class AccountDatabase
    {
        SQLiteAsyncConnection Database;

        public AccountDatabase() {}

        private async Task Init()
        {
            if (Database != null)
                return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "AccountsSQLite.db3");
            var flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;
            Database = new SQLiteAsyncConnection(databasePath, flags);
            await Database.CreateTableAsync<Account>();
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            await Init();
            return await Database.Table<Account>().ToListAsync();
        }

        public async Task<int> SaveAccountAsync(Account account)
        {
            await Init();
            if (account.Id == 0)
                return await Database.InsertAsync(account);
            return await Database.UpdateAsync(account);
        }
    }
}
