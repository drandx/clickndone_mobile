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
	[Register ("SupplierRateFinishedServiceController")]
	partial class SupplierRateFinishedServiceController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnRate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView indicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtClickCode { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtComments { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtDate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtEndTime { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtRanking { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtStartTime { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtState { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtClickCode != null) {
				txtClickCode.Dispose ();
				txtClickCode = null;
			}

			if (txtComments != null) {
				txtComments.Dispose ();
				txtComments = null;
			}

			if (txtDate != null) {
				txtDate.Dispose ();
				txtDate = null;
			}

			if (txtEndTime != null) {
				txtEndTime.Dispose ();
				txtEndTime = null;
			}

			if (txtRanking != null) {
				txtRanking.Dispose ();
				txtRanking = null;
			}

			if (txtStartTime != null) {
				txtStartTime.Dispose ();
				txtStartTime = null;
			}

			if (txtState != null) {
				txtState.Dispose ();
				txtState = null;
			}

			if (btnRate != null) {
				btnRate.Dispose ();
				btnRate = null;
			}

			if (indicator != null) {
				indicator.Dispose ();
				indicator = null;
			}
		}
	}
}
