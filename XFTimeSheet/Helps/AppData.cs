using System;
using System.Collections.Generic;
using XFTimeSheet.Models;
using XFTimeSheet.Services;

namespace XFTimeSheet
{
	public class AppData
	{
		public static string UserUrl { get; set; } = "http://localhost/api/user";
		public static string TravelExpensesCategoryUrl { get; set; } = "http://localhost/api/TravelExpensesCategory";
		public static string UserAuthUrl { get; set; } = "http://localhost/api/User/Auth";
		public static string TravelExpenseUrl { get; set; } = "http://localhost/api/TravelExpense";
		public static DataService DataService = new DataService();
		public static List<Absent> AllAbsent = new List<Absent>();
		public static List<AbsentCategory> AllAbsentCategory = new List<AbsentCategory>();
		public static List<TravelExpense> AllTravelExpense = new List<TravelExpense>();
		public static List<TravelExpensesCategory> AllTravelExpensesCategory = new List<TravelExpensesCategory>();
		public static string Account = "";

		public static Menu NowMenu = Menu.TravelExpenses;

	}

	public enum Menu
	{
		Absent,
		TravelExpenses,
		LogOut
	}
}
