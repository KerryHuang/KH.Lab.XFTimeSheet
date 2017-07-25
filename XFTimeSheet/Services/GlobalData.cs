using System;
using XFTimeSheet.Repositories;

namespace XFTimeSheet.Services
{
    public class GlobalData
    {
		public static string Filter = "Filter";
		public static string DBName = "DB.db3";

		public static TablesRepository tablesRepository = new TablesRepository();
    }
}
