// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ClickNDone.iOS
{
	[Register ("LogoutController")]
	partial class LogoutController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblNo { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblYes { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblYes != null) {
				lblYes.Dispose ();
				lblYes = null;
			}

			if (lblNo != null) {
				lblNo.Dispose ();
				lblNo = null;
			}
		}
	}
}
