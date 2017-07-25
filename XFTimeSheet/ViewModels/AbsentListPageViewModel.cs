using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Prism.Events;
using XFTimeSheet.Infrastructure;

namespace XFTimeSheet.ViewModels
{
	public class AbsentListPageViewModel : BindableBase, INavigationAware
	{
		readonly INavigationService _navigationService;
		readonly IEventAggregator _eventAggregator;

		public DelegateCommand MyDataClickedCommand { get; set; }
		public DelegateCommand UpdateDataCommand { get; private set; }
		public DelegateCommand AddCommand { get; private set; }

		#region MyData
		ObservableCollection<AbsentListItemViewModel> myData = new ObservableCollection<AbsentListItemViewModel>();
		/// <summary>
		/// MyData
		/// </summary>
		public ObservableCollection<AbsentListItemViewModel> MyData
		{
			get { return this.myData; }
			set { this.SetProperty(ref this.myData, value); }
		}
		#endregion


		#region MyDataSelected
		AbsentListItemViewModel myDataSelected;
		/// <summary>
		/// MyDataSelected
		/// </summary>
		public AbsentListItemViewModel MyDataSelected
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

		public AbsentListPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
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
			navPara.Add("Absent", new Absent
			{
				Account = AppData.Account,
				Category = "",
				Memo = "",
				StartDate = DateTime.Now,
				EndDate = DateTime.Now
			});
			await _navigationService.NavigateAsync("AbsentPage", navPara);
		}

		async void MyDataClicked()
		{
			var item = AppData.AllAbsent.FirstOrDefault(x => x.ID == MyDataSelected.ID);
			var navPara = new NavigationParameters();
			navPara.Add("模式", "修改");
			navPara.Add("Absent", new Absent
			{
				ID = item.ID,
				Account = AppData.Account,
				Category = item.Category,
				Memo = item.Memo,
				StartDate = item.StartDate,
				EndDate = item.EndDate
			});
			await _navigationService.NavigateAsync("AbsentPage", navPara);
		}

		async void Edit()
		{
			var items = (await AppData.DataService.GetAbsentAsync(AppData.Account)).ToList();
			AppData.AllAbsent = items;
			Reload();
			IsBusy = false;
		}

		public void Reload()
		{
			MyData.Clear();
			foreach (var item in AppData.AllAbsent)
			{
				MyData.Add(new AbsentListItemViewModel
				{
					ID = item.ID,
					Title = item.Category,
					StartDate = item.StartDate,
					EndDate = item.EndDate
				});
			}
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
			Reload();
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}
	}
}
