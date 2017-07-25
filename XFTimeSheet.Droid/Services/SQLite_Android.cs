using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using XFTimeSheet.Droid.Services;
using XFTimeSheet.Services;

[assembly: Dependency(typeof(SQLite_Android))]
namespace XFTimeSheet.Droid.Services
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {
        }

        public SQLiteConnection GetConnection()
        {
			var path = GetDBPath();
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection 
			return conn;
        }

        public SQLiteAsyncConnection GetConnectionAsync()
        {
			var path = GetDBPath();
			var asyncDb = new SQLiteAsyncConnection(path);
			// Return the database connection 
			return asyncDb;
        }

		public string GetDBPath()
		{
			var sqliteFilename = GlobalData.DBName;
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			return path;
		}
    }
}
