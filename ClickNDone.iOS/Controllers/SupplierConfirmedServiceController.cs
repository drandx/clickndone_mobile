// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ClickNDone.Core;

namespace ClickNDone.iOS
{
	public partial class SupplierConfirmedServiceController : UIViewController
	{
		readonly OrdersModel ordersModel = (OrdersModel)DependencyInjectionWrapper.Instance.ServiceContainer ().GetService (typeof(OrdersModel));

		public SupplierConfirmedServiceController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.SetHidesBackButton (true, false);
			try {
				txtAddress.Text = ordersModel.RequestedOrder.Location;
				txtReference.Text = ordersModel.RequestedOrder.Reference;
				txtDate.Text = ordersModel.RequestedOrder.GetReservationDate();
				txtTime.Text = ordersModel.RequestedOrder.GetReservationTime();
				txtUserName.Text = ordersModel.RequestedOrder.User.names;
				txtUserLastName.Text = ordersModel.RequestedOrder.User.surnames;

			} catch (Exception exc) {
				new UIAlertView ("Oops!", exc.Message, null, "Ok").Show ();
			}
		}
	}
}