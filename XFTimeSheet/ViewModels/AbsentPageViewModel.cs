using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using Prism.Events;
using XFTimeSheet.Infrastructure;

namespace XFTimeSheet.ViewModels
{
	public class AbsentPageViewModel : BindableBase, INavigationAware
	{
		#region ViewModel Property

		#region ID
		int _id;
		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			get { return this._id; }
			set { this.SetProperty(ref this._id, value); }
		}
		#endregion

		#region Account
		string _account;
		/// <summary>
		/// Account
		/// </summary>
		public string Account
		{
			get { return this._account; }
			set { this.SetProperty(ref this._account, value); }
		}
		#endregion

		#region StartDate
		DateTime _startDate = DateTime.Now;
		/// <summary>
		/// TravelDate
		/// </summary>
		public DateTime StartDate
		{
			get { return this._startDate; }
			set { this.SetProperty(ref this._startDate, value); }
		}
		#endregion

		#region EndDate
		DateTime _endDate = DateTime.Now;
		/// <summary>
		/// TravelDate
		/// </summary>
		public DateTime EndDate
		{
			get { return this._endDate; }
			set { this.SetProperty(ref this._endDate, value); }
		}
		#endregion

		#region Category
		string _category;
		/// <summary>
		/// Category
		/// </summary>
		public string Category
		{
			get { return this._category; }
			set { this.SetProperty(ref this._category, value); }
		}
		#endregion

		#region Memo
		string _memo;
		/// <summary>
		/// Memo
		/// </summary>
		public string Memo
		{
			get { return this._memo; }
			set { this.SetProperty(ref this._memo, value); }
		}
		#endregion

		#region Updatetime
		DateTime _updatetime;
		/// <summary>
		/// Updatetime
		/// </summary>
		public DateTime Updatetime
		{
			get { return this._updatetime; }
			set { this.SetProperty(ref this._updatetime, value); }
		}
		#endregion


		#region CategoryList
		List<string> _categoryList;
		/// <summary>
		/// CategoryList
		/// </summary>
		public List<string> CategoryList
		{
			get { return this._categoryList; }
			set { this.SetProperty(ref this._categoryList, value); }
		}
		#endregion


		#region ShowDeleteMode
		bool _isDeleteMode;
		/// <summary>
		/// PropertyDescription
		/// </summary>
		public bool ShowDeleteMode
		{
			get { return this._isDeleteMode; }
			set { this.SetProperty(ref this._isDeleteMode, value); }
		}
		#endregion

		#endregion

		public string EventModel { get; set; }
		readonly INavigationService _navigationService;
		public readonly IPageDialogService _dialogService;
		readonly IEventAggregator _eventAggregator;

		public delegate string ReadPickerSelectItemDel();
		public ReadPickerSelectItemDel readPickerSelectItemDel;
		public Action<string> TypePickerInit;
		public Action<string> PageTitleInit;
		Absent _absent;

		public DelegateCommand SaveCommand { get; private set; }
		public DelegateCommand DeleteCommand { get; private set; }
		public DelegateCommand CancelCommand { get; private set; }

		public AbsentPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService dialogService)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;
			_eventAggregator = eventAggregator;

			SaveCommand = new DelegateCommand(Save);
			DeleteCommand = new DelegateCommand(Delete);
			CancelCommand = new DelegateCommand(Cancel);
		}

		bool IsCheck()
		{
			bool isChange = false;

			Category = readPickerSelectItemDel();
			if (_absent.Category != Category)
			{
				isChange = true;
			}
			else if (_absent.Memo != Memo)
			{
				isChange = true;
			}
			else if (_absent.StartDate.Date != StartDate.Date)
			{
				isChange = true;
			}
			else if (_absent.EndDate.Date != EndDate.Date)
			{
				isChange = true;
			}
			return isChange;
		}

		async void Cancel()
		{
			if (IsCheck() == true)
			{
				var answer = await _dialogService.DisplayAlertAsync("警告", "資料已經有異動，您確定要取消此次資料輸入嗎?", "是", "否");
				if (answer == true)
				{
					await _navigationService.GoBackAsync();
				}
			}
			else
			{
				await _navigationService.GoBackAsync();
			}
		}

		async void Delete()
		{
			await AppData.DataService.DeleteAbsentAsync(ID);
			var items = (await AppData.DataService.GetAbsentAsync(AppData.Account)).ToList();
			AppData.AllAbsent = items;
			_eventAggregator.GetEvent<CRUDEvent>().Publish("Refresh");
			await _navigationService.GoBackAsync();
		}

		async void Save()
		{
			Category = readPickerSelectItemDel();
			_absent = new Absent
			{
				ID = ID,
				Account = AppData.Account,
				Category = Category,
				Memo = Memo,
				StartDate = StartDate,
				EndDate = EndDate,
				Updatetime = DateTime.Now,
			};
			if (EventModel == "新增")
			{
				await AppData.DataService.PostAbsentAsync(_absent);
			}
			else
			{
				await AppData.DataService.PutAbsentsAsync(_absent);
			}
			var items = (await AppData.DataService.GetAbsentAsync(AppData.Account)).ToList();
			AppData.AllAbsent = items;
			_eventAggregator.GetEvent<CRUDEvent>().Publish("Refresh");
			await _navigationService.GoBackAsync();
		}

		void Init()
		{
			CategoryList = new List<string>();
			var items = AppData.AllAbsentCategory;
			foreach (var item in items)
			{
				CategoryList.Add(item.Name);
			}
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
			EventModel = parameters["模式"] as string;
			_absent = parameters["Absent"] as Absent;
			ID = _absent.ID;
			Category = _absent.Category;
			Memo = _absent.Memo;
			StartDate = _absent.StartDate;
			EndDate = _absent.EndDate;
			if (EventModel == "新增")
			{
				ShowDeleteMode = false;
                PageTitleInit("請假單 新增");
			}
			else
			{
				ShowDeleteMode = true;
				TypePickerInit(_absent.Category);
				PageTitleInit("請假單 修改");
			}
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}
	}
}
