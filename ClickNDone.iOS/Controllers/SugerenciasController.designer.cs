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
	[Register ("SugerenciasController")]
	partial class SugerenciasController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnClean { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSendComment { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtApellido { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtComments { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtUserName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSendComment != null) {
				btnSendComment.Dispose ();
				btnSendComment = null;
			}

			if (btnClean != null) {
				btnClean.Dispose ();
				btnClean = null;
			}

			if (txtApellido != null) {
				txtApellido.Dispose ();
				txtApellido = null;
			}

			if (txtComments != null) {
				txtComments.Dispose ();
				txtComments = null;
			}

			if (txtUserName != null) {
				txtUserName.Dispose ();
				txtUserName = null;
			}
		}
	}
}
