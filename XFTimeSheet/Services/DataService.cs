using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using XFTimeSheet.Models;
using XFTimeSheet.Repositories;

namespace XFTimeSheet.Services
{
	public class DataService
	{
		TablesRepository _repository { get; set; } = new TablesRepository();

		readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};

		HttpClient GetClient()
		{
			HttpClient client = new HttpClient(new HttpClientHandler());
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		#region Absent
		public async Task<IEnumerable<Absent>> GetAbsentAsync(string account)
		{
			return (await _repository.AbsentTables.GetAllAsync()).Where(x => x.Account == account);
		}

		public async Task PostAbsentAsync(Absent absent)
		{
			await this._repository.AbsentTables.InsertAsync(absent);
		}

		public async Task PutAbsentsAsync(Absent absent)
		{
			await this._repository.AbsentTables.UpdateAsync(absent);
		}

		public async Task DeleteAbsentAsync(int id)
		{
			var item = (await _repository.AbsentTables.GetAllAsync()).Where(x => x.ID == id).FirstOrDefault();
			await _repository.AbsentTables.DeleteAsync(item);
			return;
		}
		#endregion

		#region AbsentCategory
		public async Task<List<AbsentCategory>> GetAbsentCategoryAsync()
		{
			var items = (await this._repository.AbsentCategoryTables.GetAllAsync()).ToList();
			if (!items.Any())
			{
				//using (HttpClient client = GetClient())
				//{
				//	HttpResponseMessage httpResponseMessage = await client.GetAsync(AppData.TravelExpensesCategoryUrl);
				//	httpResponseMessage.EnsureSuccessStatusCode();
				//	string content = await httpResponseMessage.Content.ReadAsStringAsync();
				//	items = JsonConvert.DeserializeObject<List<AbsentCategory>>(content);
				//	await _repository.AbsentCategoryTables.InsertAsync(items.ToList());
				//}
				items.Add(new AbsentCategory() { ID = 1, Name = "事假" });
				items.Add(new AbsentCategory() { ID = 2, Name = "病假" });
				items.Add(new AbsentCategory() { ID = 3, Name = "特休假" });
				items.Add(new AbsentCategory() { ID = 4, Name = "公假" });
			}
			return items;
		}
		#endregion

		#region TravelExpense
		public async Task<IEnumerable<TravelExpense>> GetTravelExpensesAsync(string account)
		{
			//using (HttpClient client = GetClient())
			//{
			//	var fooStr = $"{AppData.TravelExpenseUrl}?account={account}";
			//	HttpResponseMessage httpResponseMessage = await client.GetAsync(fooStr);
			//	httpResponseMessage.EnsureSuccessStatusCode();
			//	string content = await httpResponseMessage.Content.ReadAsStringAsync();
			//	return JsonConvert.DeserializeObject<IEnumerable<TravelExpense>>(content);
			//}

			return (await _repository.TravelExpenseTables.GetAllAsync()).Where(x => x.Account == account);
		}

		public async Task PostTravelExpensesAsync(TravelExpense travelExpense)
		{
			//using (HttpClient client = GetClient())
			//{
			//	HttpResponseMessage httpResponseMessage = await client.PostAsync(AppData.TravelExpenseUrl,
			//	new StringContent(
			//		JsonConvert.SerializeObject(travelExpense, _jsonSerializerSettings),
			//		Encoding.UTF8, "application/json"));
			//	httpResponseMessage.EnsureSuccessStatusCode();
			//	string content = await httpResponseMessage.Content.ReadAsStringAsync();
			//	return;
			//}
			await this._repository.TravelExpenseTables.InsertAsync(travelExpense);
		}

		public async Task PutTravelExpensesAsync(TravelExpense travelExpense)
		{
			//using (HttpClient client = GetClient())
			//{
			//	HttpResponseMessage httpResponseMessage = await client.PutAsync(AppData.TravelExpenseUrl,
			//	new StringContent(
			//		JsonConvert.SerializeObject(travelExpense, _jsonSerializerSettings),
			//		Encoding.UTF8, "application/json"));
			//	httpResponseMessage.EnsureSuccessStatusCode();
			//	string content = await httpResponseMessage.Content.ReadAsStringAsync();
			//	return;
			//}
			await this._repository.TravelExpenseTables.UpdateAsync(travelExpense);
		}

		public async Task DeleteTravelExpensesAsync(int id)
		{
			//using (HttpClient client = GetClient())
			//{
			//	HttpResponseMessage httpResponseMessage = await client.DeleteAsync($"{AppData.TravelExpenseUrl}?id={id}");
			//	httpResponseMessage.EnsureSuccessStatusCode();
			//	string content = await httpResponseMessage.Content.ReadAsStringAsync();
			//	return;
			//}

			var item = (await _repository.TravelExpenseTables.GetAllAsync()).Where(x => x.ID == id).FirstOrDefault();
			await _repository.TravelExpenseTables.DeleteAsync(item);
			return;
		}
		#endregion

		#region TravelExpensesCategory
		public async Task<List<TravelExpensesCategory>> GetTravelExpensesCategoryAsync()
		{
			var items = (await this._repository.TravelExpensesCategoryTables.GetAllAsync()).ToList();
			if (!items.Any())
			{
				//using (HttpClient client = GetClient())
				//{
				//	HttpResponseMessage httpResponseMessage = await client.GetAsync(AppData.TravelExpensesCategoryUrl);
				//	httpResponseMessage.EnsureSuccessStatusCode();
				//	string content = await httpResponseMessage.Content.ReadAsStringAsync();
				//	items = JsonConvert.DeserializeObject<List<TravelExpensesCategory>>(content);
				//	await _repository.TravelExpensesCategoryTables.InsertAsync(items.ToList());
				//}
				items.Add(new TravelExpensesCategory() { ID = 1, Name = "交通費" });
				items.Add(new TravelExpensesCategory() { ID = 2, Name = "住宿費" });
				items.Add(new TravelExpensesCategory() { ID = 3, Name = "伙食費" });
			}
			return items;
		}
		#endregion

		#region User
		public async Task<AuthUserResult> AuthUserAsync(AuthUser authUser)
		{
			//using (HttpClient client = GetClient())
			//{
			//	HttpResponseMessage httpResponseMessage = await client.PostAsync(AppData.UserAuthUrl,
			//	new StringContent(
			//		JsonConvert.SerializeObject(authUser, _jsonSerializerSettings),
			//		Encoding.UTF8, "application/json"));
			//	httpResponseMessage.EnsureSuccessStatusCode();
			//	string content = await httpResponseMessage.Content.ReadAsStringAsync();
			//	return JsonConvert.DeserializeObject<AuthUserResult>(content);
			//}

			var items = (await this._repository.UserTables.GetAllAsync()).ToList();
			AuthUserResult result = new AuthUserResult();
			result.Status = items.Any(x => x.Account == authUser.Account && x.Password == authUser.Password);
			return result;
		}

		public async Task PostUser(User user)
		{
			await this._repository.UserTables.InsertAsync(user);
		}

		public async Task PutUser(User user)
		{
			await this._repository.UserTables.UpdateAsync(user);
		}

		public async Task<List<User>> GetUserAsync()
		{
			return await this._repository.UserTables.GetAllAsync();
		}
		#endregion
	}
}
