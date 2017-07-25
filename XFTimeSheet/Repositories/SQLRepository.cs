using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using XFTimeSheet.Services;
using System.Linq;

namespace XFTimeSheet.Repositories
{
    public class SQLRepository<T> where T : class, new()
    {
		//static object locker = new object();
		public string DBPath { get; set; }
		//public SQLiteConnection database;
		public SQLiteAsyncConnection databaseAsync;
		public SQLRepository()
		{
			databaseAsync = DependencyService.Get<ISQLite>().GetConnectionAsync();
			databaseAsync.CreateTableAsync<T>().Wait(); ;
		}

		#region 非同步的 SQLiteAsyncConnection 用法
		public async Task<List<T>> GetAllAsync()
		{
			return await databaseAsync.Table<T>().ToListAsync();
		}

		public async Task<int> InsertAsync(T item)
		{
			return await databaseAsync.InsertAsync(item);
		}

		public async Task<int> InsertAsync(List<T> items)
		{
			return await databaseAsync.InsertAllAsync(items);
		}

		public async Task<int> UpdateAsync(T item)
		{
			return await databaseAsync.UpdateAsync(item);
		}

		public async Task<int> DeleteAsync(T item)
		{
			return await databaseAsync.DeleteAsync(item);
		}

		public async Task<int> DeleteAsync(List<T> items)
		{
			int result = 0;
			foreach (T item in items)
			{
				result += await DeleteAsync(item);
			}
			return result;
		}

		#endregion
	}
}
