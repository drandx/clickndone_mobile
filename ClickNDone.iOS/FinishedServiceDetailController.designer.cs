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
	[Register ("FinishedServiceDetailController")]
	partial class FinishedServiceDetailController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView imgCat { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblSubcategory { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblUserType { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtClickCode { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtDate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtLastName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtOrderState { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtUserName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgCat != null) {
				imgCat.Dispose ();
				imgCat = null;
			}

			if (lblSubcategory != null) {
				lblSubcategory.Dispose ();
				lblSubcategory = null;
			}

			if (txtClickCode != null) {
				txtClickCode.Dispose ();
				txtClickCode = null;
			}

			if (txtDate != null) {
				txtDate.Dispose ();
				txtDate = null;
			}

			if (txtLastName != null) {
				txtLastName.Dispose ();
				txtLastName = null;
			}

			if (txtOrderState != null) {
				txtOrderState.Dispose ();
				txtOrderState = null;
			}

			if (txtUserName != null) {
				txtUserName.Dispose ();
				txtUserName = null;
			}

			if (lblUserType != null) {
				lblUserType.Dispose ();
				lblUserType = null;
			}
		}
	}
}
