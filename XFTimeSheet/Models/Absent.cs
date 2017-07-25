using System;
using SQLite;

namespace XFTimeSheet
{
	/// <summary>
	/// 請假單申請
	/// </summary>
	public class Absent
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Account { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Category { get; set; }
		public string Memo { get; set; }
		public DateTime Updatetime { get; set; }
	}
}
