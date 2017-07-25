using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XFTimeSheet.ViewModels
{
	/// <summary>
	/// 差旅費用清單資料
	/// </summary>
	public class TravelExpensesListItemViewModel : BindableBase
	{
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


		#region TravelDate
		DateTime _travelDate;
		/// <summary>
		/// TravelDate
		/// </summary>
		public DateTime TravelDate
		{
			get { return this._travelDate; }
			set { this.SetProperty(ref this._travelDate, value); }
		}
		#endregion
	}
}
