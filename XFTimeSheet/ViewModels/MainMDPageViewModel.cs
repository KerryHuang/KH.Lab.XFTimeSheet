using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Xamarin.Forms;

namespace XFTimeSheet.ViewModels
{
	/// <summary>
	/// 主系統導航抽屜
	/// </summary>
	public class MainMDPageViewModel : BindableBase
	{
		readonly INavigationService _navigationService;

		#region AbsentColor
		Color _AbsentColor;
		/// <summary>
		/// 請假單申請
		/// </summary>
		public Color AbsentColor
		{
			get { return this._AbsentColor; }
			set { this.SetProperty(ref this._AbsentColor, value); }
		}
		#endregion

		#region TravelExpenseColor
		Color _TravelExpenseColor;
		/// <summary>
		/// 差旅費用申請
		/// </summary>
		public Color TravelExpenseColor
		{
			get { return this._TravelExpenseColor; }
			set { this.SetProperty(ref this._TravelExpenseColor, value); }
		}
		#endregion

		#region LogOutColor
		Color _LogOutColor;
		/// <summary>
		/// 登出
		/// </summary>
		public Color LogOutColor
		{
			get { return this._LogOutColor; }
			set { this.SetProperty(ref this._LogOutColor, value); }
		}
		#endregion

		public DelegateCommand AbsentCommand { get; private set; }
		public DelegateCommand TravelExpenseCommand { get; private set; }
		public DelegateCommand LogOutCommand { get; private set; }

		public MainMDPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;

			TravelExpenseCommand = new DelegateCommand(TravelExpense);
			LogOutCommand = new DelegateCommand(LogOut);
			AbsentCommand = new DelegateCommand(Absent);

			UpdateMenuColor();
		}

		async void LogOut()
		{
			AppData.NowMenu = Menu.LogOut;
			UpdateMenuColor();
			await _navigationService.NavigateAsync("xf:///LoginPage");
		}

		async void TravelExpense()
		{
			if (AppData.NowMenu != Menu.TravelExpenses)
			{
				var items = (await AppData.DataService.GetTravelExpensesAsync(AppData.Account)).ToList();
				AppData.AllTravelExpense = items;
				AppData.NowMenu = Menu.TravelExpenses;
				UpdateMenuColor();
				await _navigationService.NavigateAsync("xf:///MainMDPage/NaviPage/TravelExpensesListPage");
			}
		}

		async void Absent()
		{
			if (AppData.NowMenu != Menu.Absent)
			{
				var items = (await AppData.DataService.GetAbsentAsync(AppData.Account)).ToList();
				AppData.AllAbsent = items;
				AppData.NowMenu = Menu.Absent;
				UpdateMenuColor();
				await _navigationService.NavigateAsync("xf:///MainMDPage/NaviPage/AbsentListPage");
			}
		}

		public void UpdateMenuColor()
		{
			ClearAllColor();
			switch (AppData.NowMenu)
			{
				case Menu.Absent:
					AbsentColor = Color.FromHex("AA0000");
					break;
				case Menu.TravelExpenses:
					TravelExpenseColor = Color.FromHex("AA0000");
					break;
				case Menu.LogOut:
					LogOutColor = Color.FromHex("AA0000");
					break;
				default:
					break;
			}
		}

		public void ClearAllColor()
		{
			AbsentColor = Color.FromHex("040000");
			TravelExpenseColor = Color.FromHex("040000");
			LogOutColor = Color.FromHex("040000");
		}
	}
}
