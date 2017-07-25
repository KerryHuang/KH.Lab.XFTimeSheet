using System;
namespace XFTimeSheet.Models
{
	/// <summary>
	/// 使用者認證
	/// </summary>
	public class AuthUser
	{
		public string Account { get; set; }
		public string Password { get; set; }
	}
	/// <summary>
	/// 認證結果
	/// </summary>
	public class AuthUserResult
	{
		public bool Status { get; set; }
	}
}
