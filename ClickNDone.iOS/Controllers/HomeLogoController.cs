// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ClickNDone.Core;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace ClickNDone.iOS
{
	public partial class HomeLogoController : MyViewController
	{

		readonly UserModel loginViewModel = (UserModel)DependencyInjectionWrapper.Instance.ServiceContainer ().GetService (typeof(UserModel));
		readonly OrdersModel ordersModel = (OrdersModel)DependencyInjectionWrapper.Instance.ServiceContainer ().GetService (typeof(OrdersModel));
		static int taskId;
		static volatile bool running;


		public HomeLogoController (IntPtr handle) : base (handle)
		{

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.LoadLeftbarButton ();

			btnStartTaskt.TouchUpInside += (sender, e) => {
				 OnStartTask ();
			};

			if (loginViewModel.UserType.Equals (UserType.CONSUMER)) 
			{
				KillBackGroundTastk ();
			} 
			else 
			{
				//btnStartTaskt.Hidden = false;
				OnStartTask ();

			}

		}


		async void OnStartTask()
		{
			// Stop the task if it's running.
			if (taskId > 0) {
				btnStartTaskt.SetTitle("Activar Recepción de Ordenes", UIControlState.Normal);
				Console.WriteLine("Stopping Task...");
				running = false;
				taskId = 0;
				return;
			}

			running = true;
			btnStartTaskt.SetTitle("Detener Recepción de Ordenes", UIControlState.Normal);
			taskId = 1;

			var cts = new CancellationTokenSource();
			taskId = UIApplication.SharedApplication.BeginBackgroundTask("Long-Running Task", () => {
				Console.WriteLine("Task {0} timeout occurred, canceled.", taskId);
				cts.Cancel();
			});

			// Kick off .NET Task to run in the background.
			try {
				await Task.Run(() => {
					for (long count = 1; running == true ; count++) {
						this.BeginInvokeOnMainThread(() => {
							Console.WriteLine("Task {0} running.. {1}", taskId, count);
							var ordersList = ordersModel.GetOrdersList(loginViewModel.User.id,ServiceState.ABIERTO,UserType.SUPPLIER);
							if(ordersList == null)
							{
								//this.BeginInvokeOnMainThread(() => {
									Console.WriteLine("Orders List Null");
								//});
							}
							else
							{
								this.BeginInvokeOnMainThread(() => {
									Console.WriteLine("Orders List Items Count: "+ordersList.Count);							
								});
								if((ordersList.Count() > 0))
								{
									ordersModel.RequestedOrder = ordersList.First();
									this.BeginInvokeOnMainThread(() => {
										LoadUIController();
										TriggerLocalNotification();
										UIApplication.SharedApplication.EndBackgroundTask(taskId);
										taskId = 0;
										running = false;
									});
								}
							}
						});

						cts.Token.ThrowIfCancellationRequested();
						Thread.Sleep(Constants.GET_ORDER_STATUS_WAIT_TIME);
					}
				}, cts.Token);
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("Task {0} was cancelled.", taskId);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: {0}", ex.Message);
			}
			finally
			{
				UIApplication.SharedApplication.EndBackgroundTask(taskId);
				taskId = 0;
				running = false;
			}
		}


		private void TriggerLocalNotification()
		{
			var notification = new UILocalNotification();
			notification.FireDate = DateTime.Now.AddSeconds(1);
			notification.AlertAction = "Servicio Solicitado";
			notification.AlertBody = "Usted tiene una nueva consulta de servicio.";
			notification.ApplicationIconBadgeNumber = 1;
			notification.SoundName = UILocalNotification.DefaultSoundName;
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
		}

		private void LoadUIController()
		{
			var controller = Storyboard.InstantiateViewController("SupplierServiceController") as UIViewController;
			PresentViewController (controller,true,null);
		}


		public static void KillBackGroundTastk()
		{
			UIApplication.SharedApplication.EndBackgroundTask(taskId);
			taskId = 0;
			running = false;
		}

	}
}
