using System;
using SQLite;

namespace XFTimeSheet.Models
{
	/// <summary>
	/// 差旅費用項目紀錄
	/// </summary>
	public class TravelExpense
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Account { get; set; }
		public DateTime TravelDate { get; set; }
		public string Category { get; set; }
		public string Title { get; set; }
		public string Location { get; set; }
		public double Expense { get; set; }
		public string Memo { get; set; }
		public bool Domestic { get; set; }
		public bool HasDocument { get; set; }
		public DateTime Updatetime { get; set; }
	}
}
