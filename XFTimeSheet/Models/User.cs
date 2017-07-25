using System;
using SQLite;

namespace XFTimeSheet.Models
{
    /// <summary>
    /// 使用者
    /// </summary>
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
		public bool Remember { get; set; }
    }
}
