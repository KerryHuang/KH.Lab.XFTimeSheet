using System.Linq;
using Xamarin.Forms;
using XFTimeSheet.ViewModels;

namespace XFTimeSheet.Views
{
	/// <summary>
	/// 差旅費用項目資料維護
	/// </summary>
	public partial class TravelExpensesPage : ContentPage
	{
		TravelExpensesPageViewModel fooTravelExpensesPageViewModel;
		public TravelExpensesPage()
		{
			InitializeComponent();

			fooTravelExpensesPageViewModel = this.BindingContext as TravelExpensesPageViewModel;
			SetPickerSelectContent();
			fooTravelExpensesPageViewModel.readPickerSelectItemDel = ReadPickerSelectItemDel;
			fooTravelExpensesPageViewModel.TypePickerInit = TypePickerInit;
			fooTravelExpensesPageViewModel.PageTitleInit = PageTitleInit;
		}

		protected override bool OnBackButtonPressed()
		{
			return true;
		}

		private void PageTitleInit(string obj)
		{
			this.Title = obj;
		}

		public void SetPickerSelectContent()
		{
			foreach (var item in AppData.AllTravelExpensesCategory)
			{
				picker分類.Items.Add(item.Name);
			}
		}

		public string ReadPickerSelectItemDel()
		{
			string ret = "";
			var fooIdx = picker分類.SelectedIndex;
			if (fooIdx >= 0)
			{
				ret = picker分類.Items[fooIdx];
			}
			else
			{
				ret = "";
			}
			return ret;
		}

		public void TypePickerInit(string category)
		{
			var item = AppData.AllTravelExpensesCategory.FirstOrDefault(x => x.Name == category);
			if (item != null)
			{
				var idx = AppData.AllTravelExpensesCategory.IndexOf(item);
				if (idx >= 0)
				{
					picker分類.SelectedIndex = idx;
				}
			}
		}
	}
}

