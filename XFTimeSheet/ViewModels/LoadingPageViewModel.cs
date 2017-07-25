using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Threading.Tasks;
using XFTimeSheet.Models;

namespace XFTimeSheet.ViewModels
{
	/// <summary>
	/// 系統初始化
	/// </summary>
	public class LoadingPageViewModel : BindableBase, INavigationAware
	{
		readonly INavigationService _navigationService;
		public LoadingPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}

		public async void OnNavigatedTo(NavigationParameters parameters)
		{
			await Task.Delay(500);
			AppData.AllTravelExpensesCategory = (await AppData.DataService.GetTravelExpensesCategoryAsync()).ToList();
			AppData.AllAbsentCategory = (await AppData.DataService.GetAbsentCategoryAsync()).ToList();
			await Task.Delay(500);

			await _navigationService.NavigateAsync("xf:///LoginPage");
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();

			HasUser();
		}

		async void HasUser()
		{
            var items = (await AppData.DataService.GetUserAsync()).ToList();
			if (!items.Any())
			{
				var user = new User()
				{
					Account = "001",
					Password = "001"
				};
				await AppData.DataService.PostUser(user);
			}
		}
	}
}
