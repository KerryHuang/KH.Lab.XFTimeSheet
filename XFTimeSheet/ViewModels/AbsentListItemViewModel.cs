using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XFTimeSheet.ViewModels
{
	public class AbsentListItemViewModel : BindableBase
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


		#region StartDate
		DateTime _starDate;
		/// <summary>
		/// TravelDate
		/// </summary>
		public DateTime StartDate
		{
			get { return this._starDate; }
			set { this.SetProperty(ref this._starDate, value); }
		}
		#endregion

		#region EndDate
		DateTime _endDate;
		/// <summary>
		/// TravelDate
		/// </summary>
		public DateTime EndDate
		{
			get { return this._endDate; }
			set { this.SetProperty(ref this._endDate, value); }
		}
		#endregion
	}
}
