using Prism.Unity;
using XFTimeSheet.Views;

namespace XFTimeSheet
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override void OnInitialized()
		{
			InitializeComponent();

			//NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
			NavigationService.NavigateAsync("LoadingPage");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<MainPage>();
			Container.RegisterTypeForNavigation<LoadingPage>();
			Container.RegisterTypeForNavigation<LoginPage>();
			Container.RegisterTypeForNavigation<NaviPage>();
			Container.RegisterTypeForNavigation<MainMDPage>();
			Container.RegisterTypeForNavigation<AbsentListPage>();
			Container.RegisterTypeForNavigation<AbsentPage>();
			Container.RegisterTypeForNavigation<TravelExpensesListPage>();
			Container.RegisterTypeForNavigation<TravelExpensesPage>();
		}
	}
}

