using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using Prism.Events;
using XFTimeSheet.Models;
using XFTimeSheet.Infrastructure;

namespace XFTimeSheet.ViewModels
{
	public class TravelExpensesPageViewModel : BindableBase, INavigationAware
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

		#region TravelDate
		DateTime _travelDate = DateTime.Now;
		/// <summary>
		/// TravelDate
		/// </summary>
		public DateTime TravelDate
		{
			get { return this._travelDate; }
			set { this.SetProperty(ref this._travelDate, value); }
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

		#region Title
		string _title;
		/// <summary>
		/// Title
		/// </summary>
		public string Title
		{
			get { return this._title; }
			set { this.SetProperty(ref this._title, value); }
		}
		#endregion

		#region Location
		string _location;
		/// <summary>
		/// Location
		/// </summary>
		public string Location
		{
			get { return this._location; }
			set { this.SetProperty(ref this._location, value); }
		}
		#endregion

		#region Expense
		double _expense;
		/// <summary>
		/// Expense
		/// </summary>
		public double Expense
		{
			get { return this._expense; }
			set { this.SetProperty(ref this._expense, value); }
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

		#region Domestic
		bool _domestic;
		/// <summary>
		/// Domestic
		/// </summary>
		public bool Domestic
		{
			get { return this._domestic; }
			set { this.SetProperty(ref this._domestic, value); }
		}
		#endregion

		#region HasDocument
		bool _hasDocument;
		/// <summary>
		/// HasDocument
		/// </summary>
		public bool HasDocument
		{
			get { return this._hasDocument; }
			set { this.SetProperty(ref this._hasDocument, value); }
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
		TravelExpense _travelExpense;

		public DelegateCommand SaveCommand { get; private set; }
		public DelegateCommand DeleteCommand { get; private set; }
		public DelegateCommand CancelCommand { get; private set; }

		public TravelExpensesPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService dialogService)
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
			if (_travelExpense.Category != Category)
			{
				isChange = true;
			}
			else if (_travelExpense.Domestic != Domestic)
			{
				isChange = true;
			}
			else if (_travelExpense.Expense != Expense)
			{
				isChange = true;
			}
			else if (_travelExpense.HasDocument != HasDocument)
			{
				isChange = true;
			}
			else if (_travelExpense.Location != Location)
			{
				isChange = true;
			}
			else if (_travelExpense.Memo != Memo)
			{
				isChange = true;
			}
			else if (_travelExpense.Title != Title)
			{
				isChange = true;
			}
			else if (_travelExpense.TravelDate.Date != TravelDate.Date)
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
			await AppData.DataService.DeleteTravelExpensesAsync(ID);
            var items = (await AppData.DataService.GetTravelExpensesAsync(AppData.Account)).ToList();
			AppData.AllTravelExpense = items;
			_eventAggregator.GetEvent<CRUDEvent>().Publish("Refresh");
			await _navigationService.GoBackAsync();
		}

		async void Save()
		{
			Category = readPickerSelectItemDel();
			_travelExpense = new TravelExpense
			{
				ID = ID,
				Account = AppData.Account,
				Category = Category,
				Domestic = Domestic,
				Expense = Expense,
				HasDocument = HasDocument,
				Location = Location,
				Memo = Memo,
				Title = Title,
				TravelDate = TravelDate,
				Updatetime = DateTime.Now,
			};
			if (EventModel == "新增")
			{
				await AppData.DataService.PostTravelExpensesAsync(_travelExpense);
			}
			else
			{
				await AppData.DataService.PutTravelExpensesAsync(_travelExpense);
			}
			var items = (await AppData.DataService.GetTravelExpensesAsync(AppData.Account)).ToList();
			AppData.AllTravelExpense = items;
			_eventAggregator.GetEvent<CRUDEvent>().Publish("Refresh");
			await _navigationService.GoBackAsync();
		}

		void Init()
		{
			CategoryList = new List<string>();
			var items = AppData.AllTravelExpensesCategory;
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
			EventModel = parameters["模式"] as string;
			_travelExpense = parameters["TravelExpense"] as TravelExpense;
			ID = _travelExpense.ID;
			Category = _travelExpense.Category;
			Domestic = _travelExpense.Domestic;
			Expense = _travelExpense.Expense;
			HasDocument = _travelExpense.HasDocument;
			Location = _travelExpense.Location;
			Memo = _travelExpense.Memo;
			Title = _travelExpense.Title;
			TravelDate = _travelExpense.TravelDate;
			if (EventModel == "新增")
			{
				ShowDeleteMode = false;
				PageTitleInit("差旅費用 新增");
			}
			else
			{
				ShowDeleteMode = true;
				TypePickerInit(_travelExpense.Category);
				PageTitleInit("差旅費用 修改");
			}
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}
	}
}
