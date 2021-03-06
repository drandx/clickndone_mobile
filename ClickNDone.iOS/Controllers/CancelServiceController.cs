// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ClickNDone.Core;

namespace ClickNDone.iOS
{
	public partial class CancelServiceController : UIViewController
	{
		readonly OrdersModel ordersModel = (OrdersModel)DependencyInjectionWrapper.Instance.ServiceContainer ().GetService (typeof(OrdersModel));
		readonly CategoriesModel categoriesModel = (CategoriesModel)DependencyInjectionWrapper.Instance.ServiceContainer ().GetService (typeof(CategoriesModel));


		public CancelServiceController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.SetHidesBackButton (true, false);
			lblSubcategory.Text = categoriesModel.SelectedSubcategory.Name;
			imgCat.Image = UIImage.FromBundle (categoriesModel.SelectedCategory.ImageName);


			txtClickCode.Text = ordersModel.RequestedOrder.ClickCode;
			txtState.Text = ordersModel.RequestedOrder.Status.ToString ();
			txtName.Text = ordersModel.RequestedOrder.Supplier.names;
			txtLastName.Text = ordersModel.RequestedOrder.Supplier.surnames;
			lblRanking.Text = "?";

			btnCancelConfirm.TouchUpInside += async(sender, e) =>
			{
				try {
					await ordersModel.ChangeRequestedOrderStateAsync(ServiceState.RECHAZADO_USUARIO);
					lblMsgText1.Text = "El servicio ha sido";
					lblMsgText2.Text = "cancelado con exito";
					btnCancelConfirm.Hidden = true;
					btnNotCancel.Hidden = true;
					PerformSegue("OnConsummerCanceledService",this);
				}
				catch (Exception exc)
				{
					Console.WriteLine("Error relacionado con ordersModel.ChangeRequestedOrderStateAsync " + exc.Message);
				}
			};

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(false);
			ordersModel.IsBusyChanged += OnIsBusyChanged;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(false);
			ordersModel.IsBusyChanged -= OnIsBusyChanged;
		}

		void OnIsBusyChanged(object sender, EventArgs e)
		{
			btnCancelConfirm.Enabled = 
				btnNotCancel.Enabled =
					indicator.Hidden = !ordersModel.IsBusy;
		}

	}
}
