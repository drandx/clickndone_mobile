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
	[Register ("SupplierConfirmedServiceController")]
	partial class SupplierConfirmedServiceController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField txtAddress { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtDate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtReference { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtState { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtTime { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtUserLastName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtUserName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtUserName != null) {
				txtUserName.Dispose ();
				txtUserName = null;
			}

			if (txtUserLastName != null) {
				txtUserLastName.Dispose ();
				txtUserLastName = null;
			}

			if (txtDate != null) {
				txtDate.Dispose ();
				txtDate = null;
			}

			if (txtTime != null) {
				txtTime.Dispose ();
				txtTime = null;
			}

			if (txtAddress != null) {
				txtAddress.Dispose ();
				txtAddress = null;
			}

			if (txtReference != null) {
				txtReference.Dispose ();
				txtReference = null;
			}

			if (txtState != null) {
				txtState.Dispose ();
				txtState = null;
			}
		}
	}
}
