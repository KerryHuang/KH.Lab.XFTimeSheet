using System;
using XFTimeSheet.Infrastructure;
using XFTimeSheet.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(DebugMode))]
namespace XFTimeSheet.iOS
{
	public class DebugMode : IDebugMode
	{
		public bool IsDebugMode()
		{
#if DEBUG
			return true;
#else
			return false;
#endif
		}
	}
}
