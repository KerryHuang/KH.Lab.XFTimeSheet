using System.Linq;
using Xamarin.Forms;
using XFTimeSheet.ViewModels;

namespace XFTimeSheet.Views
{
	public partial class AbsentPage : ContentPage
	{
		AbsentPageViewModel absentPageViewModel;
		public AbsentPage()
		{
			InitializeComponent();

			absentPageViewModel = this.BindingContext as AbsentPageViewModel;
			SetPickerSelectContent();
			absentPageViewModel.readPickerSelectItemDel = ReadPickerSelectItemDel;
			absentPageViewModel.TypePickerInit = TypePickerInit;
			absentPageViewModel.PageTitleInit = PageTitleInit;
		}

		protected override bool OnBackButtonPressed()
		{
			return true;
		}

		void PageTitleInit(string obj)
		{
			this.Title = obj;
		}

		public void SetPickerSelectContent()
		{
			foreach (var item in AppData.AllAbsentCategory)
			{
				picker分類.Items.Add(item.Name);
			}
		}

		public string ReadPickerSelectItemDel()
		{
			string ret = "";
			var idx = picker分類.SelectedIndex;
			if (idx >= 0)
			{
				ret = picker分類.Items[idx];
			}
			else
			{
				ret = "";
			}
			return ret;
		}

		public void TypePickerInit(string category)
		{
			var item = AppData.AllAbsentCategory.FirstOrDefault(x => x.Name == category);
			if (item != null)
			{
				var idx = AppData.AllAbsentCategory.IndexOf(item);
				if (idx >= 0)
				{
					picker分類.SelectedIndex = idx;
				}
			}
		}
	}
}

