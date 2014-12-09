// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ClickNDone.iOS
{
	public partial class LogoutController : MyViewController
	{
		public LogoutController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.LoadLeftbarButton ();


			UITapGestureRecognizer labelYesTap = new UITapGestureRecognizer (() => {
				PerformSegue("OnLogOut",this);
			});

			UITapGestureRecognizer labelNoTap = new UITapGestureRecognizer (() => {
				PerformSegue("OnGoHome",this);
			});

			lblYes.UserInteractionEnabled = true;
			lblYes.AddGestureRecognizer (labelYesTap);

			lblNo.UserInteractionEnabled = true;
			lblNo.AddGestureRecognizer (labelNoTap);


		}
	}
}
