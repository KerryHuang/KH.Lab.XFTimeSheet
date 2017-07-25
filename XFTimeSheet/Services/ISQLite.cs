using System;
using SQLite;

namespace XFTimeSheet.Services
{
    public interface ISQLite
    {
		SQLiteConnection GetConnection();
		SQLiteAsyncConnection GetConnectionAsync();
    }
}
