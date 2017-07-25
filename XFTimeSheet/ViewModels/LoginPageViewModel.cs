using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using XFTimeSheet.Infrastructure;
using Prism.Services;
using XFTimeSheet.Models;
using System.Threading.Tasks;

namespace XFTimeSheet.ViewModels
{
	/// <summary>
	/// 使用者登入
	/// </summary>
	public class LoginPageViewModel : BindableBase
	{
		readonly INavigationService _navigationService;
		IDebugMode _debugMode;
		public readonly IPageDialogService _dialogService;

		#region Account
		string _account;
		/// <summary>
		/// 帳號
		/// </summary>
		public string Account
		{
			get { return this._account; }
			set { this.SetProperty(ref this._account, value); }
		}
		#endregion


		#region Password
		string _password;
		/// <summary>
		/// 密碼
		/// </summary>
		public string Password
		{
			get { return this._password; }
			set { this.SetProperty(ref this._password, value); }
		}
		#endregion

		#region Remember
		/// <summary>
		/// 記往我
		/// </summary>
		bool _remember;
		public bool Remember
		{
			get { return _remember; }
			set { this.SetProperty(ref this._remember, value); }
		}
		#endregion

		#region User
		User _user;
		public User user
		{
			get { return _user; }
			set { this.SetProperty(ref _user, value); }
		}
		#endregion

		public DelegateCommand LoginCommand { get; private set; }

		public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDebugMode debugMode)
		{
			_navigationService = navigationService;
			_debugMode = debugMode;
			_dialogService = dialogService;

			LoginCommand = new DelegateCommand(Login);

			if (_debugMode.IsDebugMode() == true)
			{
				Account = "001";
				Password = "001";
				Remember = true;
			}

			//var items = AppData.DataService.GetUserAsync();
			//user = items.Result.Where(x => x.Account == Account).FirstOrDefault();
		}

		async void Login()
		{
			var result = await AppData.DataService.AuthUserAsync(new AuthUser
			{
				Account = Account,
				Password = Password
			});

			if (result.Status == true)
			{
				//user.Remember = Remember;
				//await AppData.DataService.PutUser(user);
				AppData.Account = Account;
				AppData.AllTravelExpense = (await AppData.DataService.GetTravelExpensesAsync(AppData.Account)).ToList();
				AppData.AllAbsent = (await AppData.DataService.GetAbsentAsync(AppData.Account)).ToList();
				AppData.NowMenu = Menu.Absent;
				await _navigationService.NavigateAsync("xf:///MainMDPage/NaviPage/AbsentListPage");
			}
			else
			{
				await _dialogService.DisplayAlertAsync("錯誤", "帳號或者密碼錯誤", "確定");
			}
		}
	}
}
