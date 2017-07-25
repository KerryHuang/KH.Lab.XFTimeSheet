using System;
using XFTimeSheet.Droid;
using XFTimeSheet.Infrastructure;

[assembly: Xamarin.Forms.Dependency(typeof(DebugMode))]
namespace XFTimeSheet.Droid
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
