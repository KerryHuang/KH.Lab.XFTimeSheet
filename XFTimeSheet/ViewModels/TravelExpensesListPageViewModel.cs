using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Collections.ObjectModel;
using XFTimeSheet.Models;
using XFTimeSheet.Infrastructure;
using Prism.Events;

namespace XFTimeSheet.ViewModels
{
	public class TravelExpensesListPageViewModel : BindableBase, INavigationAware
	{
		readonly INavigationService _navigationService;
		readonly IEventAggregator _eventAggregator;

		public DelegateCommand MyDataClickedCommand { get; set; }
		public DelegateCommand UpdateDataCommand { get; private set; }
		public DelegateCommand AddCommand { get; private set; }

		#region MyData
		ObservableCollection<TravelExpensesListItemViewModel> myData = new ObservableCollection<TravelExpensesListItemViewModel>();
		/// <summary>
		/// MyData
		/// </summary>
		public ObservableCollection<TravelExpensesListItemViewModel> MyData
		{
			get { return this.myData; }
			set { this.SetProperty(ref this.myData, value); }
		}
		#endregion


		#region MyDataSelected
		TravelExpensesListItemViewModel myDataSelected;
		/// <summary>
		/// MyDataSelected
		/// </summary>
		public TravelExpensesListItemViewModel MyDataSelected
		{
			get { return this.myDataSelected; }
			set { this.SetProperty(ref this.myDataSelected, value); }
		}
		#endregion


		#region IsBusy
		bool isBusy;
		/// <summary>
		/// IsBusy
		/// </summary>
		public bool IsBusy
		{
			get { return this.isBusy; }
			set { this.SetProperty(ref this.isBusy, value); }
		}
		#endregion

		public TravelExpensesListPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
		{
			_navigationService = navigationService;
			_eventAggregator = eventAggregator;

			MyDataClickedCommand = new DelegateCommand(MyDataClicked);
			UpdateDataCommand = new DelegateCommand(Edit);
			AddCommand = new DelegateCommand(Create);

			_eventAggregator.GetEvent<CRUDEvent>().Subscribe(CRUDEventing);
		}

		void CRUDEventing(string obj)
		{
			if (obj == "Refresh")
			{
				Reload();
			}
		}

		async void Create()
		{
			var navPara = new NavigationParameters();
			navPara.Add("模式", "新增");
			navPara.Add("TravelExpense", new TravelExpense
			{
				Account = AppData.Account,
				Category = "",
				Domestic = true,
				Expense = 0,
				HasDocument = false,
				Location = "",
				Memo = "",
				Title = "",
				TravelDate = DateTime.Now
			});
			await _navigationService.NavigateAsync("TravelExpensesPage", navPara);
		}

		async void MyDataClicked()
		{
			var item = AppData.AllTravelExpense.FirstOrDefault(x => x.ID == MyDataSelected.ID);
			var navPara = new NavigationParameters();
			navPara.Add("模式", "修改");
			navPara.Add("TravelExpense", new TravelExpense
			{
				ID = item.ID,
				Account = AppData.Account,
				Category = item.Category,
				Domestic = item.Domestic,
				Expense = item.Expense,
				HasDocument = item.HasDocument,
				Location = item.Location,
				Memo = item.Memo,
				Title = item.Title,
				TravelDate = item.TravelDate,
			});
			await _navigationService.NavigateAsync("TravelExpensesPage", navPara);
		}

		async void Edit()
		{
			var items = (await AppData.DataService.GetTravelExpensesAsync(AppData.Account)).ToList();
			AppData.AllTravelExpense = items;
			Reload();
			IsBusy = false;
		}

		public void Reload()
		{
			MyData.Clear();
			foreach (var item in AppData.AllTravelExpense)
			{
				MyData.Add(new TravelExpensesListItemViewModel
				{
					ID = item.ID,
					Title = item.Title,
					TravelDate = item.TravelDate,
				});
			}
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			Reload();
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}
	}
}
