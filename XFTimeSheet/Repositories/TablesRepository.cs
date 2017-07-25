using System;
using XFTimeSheet.Models;

namespace XFTimeSheet.Repositories
{
	public class TablesRepository
	{
		public SQLRepository<Absent> AbsentTables { get; set; } = new SQLRepository<Absent>();
		public SQLRepository<AbsentCategory> AbsentCategoryTables { get; set; } = new SQLRepository<AbsentCategory>();
		public SQLRepository<TravelExpense> TravelExpenseTables { get; set; } = new SQLRepository<TravelExpense>();
		public SQLRepository<TravelExpensesCategory> TravelExpensesCategoryTables { get; set; } = new SQLRepository<TravelExpensesCategory>();
		public SQLRepository<User> UserTables { get; set; } = new SQLRepository<User>();
	}
}
