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
	[Register ("SupplierAgendaCController")]
	partial class SupplierAgendaCController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView indicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblAgendaServicios { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrollerAgenda { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (indicator != null) {
				indicator.Dispose ();
				indicator = null;
			}

			if (scrollerAgenda != null) {
				scrollerAgenda.Dispose ();
				scrollerAgenda = null;
			}

			if (lblAgendaServicios != null) {
				lblAgendaServicios.Dispose ();
				lblAgendaServicios = null;
			}
		}
	}
}
