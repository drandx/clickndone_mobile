using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.ComponentModel.Design;
using ClickNDone.Core;
using FlyoutNavigation;
using Parse;

namespace ClickNDone.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{

		public AppDelegate ()
		{
			// Initialize the Parse client with your Application ID and .NET Key found on
			// your Parse dashboard
			ParseClient.Initialize("Phwwy7aQcjd4eJgS5TZ3Q3NN8pr4tcJn5zp7snd7", "Y0obMKKkZBtBZFWnWYdMgOTwatRY7G2Zumui8S2q");

		}


		// class-level declarations
		public override UIWindow Window {
			get;
			set;
		}

		public static string _deviceToken="";
		private ISettings deviceSettinigs;
		
		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}


		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			//Styles
			Helper.setAppearances ();
			deviceSettinigs = new IOSSettings ();
			//TODO-temporary solution for debugging
			deviceSettinigs.DeviceToken = _deviceToken;

			//View Settings
			DependencyInjectionWrapper.Instance.ServiceContainer ().AddService (typeof(ISettings),deviceSettinigs);
			DependencyInjectionWrapper.Instance.ServiceContainer ().AddService (typeof(IWebService),new RESTWebServices());

			//ViewModels
			DependencyInjectionWrapper.Instance.ServiceContainer ().AddService (typeof(UserModel),new UserModel());
			DependencyInjectionWrapper.Instance.ServiceContainer ().AddService (typeof(TermsConditionsViewModel),new TermsConditionsViewModel());
			DependencyInjectionWrapper.Instance.ServiceContainer ().AddService (typeof(CategoriesModel),new CategoriesModel());
			DependencyInjectionWrapper.Instance.ServiceContainer ().AddService (typeof(OrdersModel),new OrdersModel());

			//Extracode / Code IOS8n and later  / Same code line 91
			UIUserNotificationType notificationTypesExtra = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
			UIUserNotificationSettings sttingsExtra = UIUserNotificationSettings.GetSettingsForTypes (notificationTypesExtra, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings(sttingsExtra);

			//Push Notifications
			UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge;
			UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);

			//Local Notifications
			//if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				//var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, new NSSet());
				//UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			//}

			return true;

		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			try
			{
				string token = deviceToken.Description;
				token = token.Substring(1, token.Length - 2);
				deviceSettinigs.DeviceToken = token;
				_deviceToken = token;
			}
			catch (Exception exc)
			{
				Console.WriteLine("***Error registering push: " + exc);
			}
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application , NSError error)
		{
			new UIAlertView("Error registering push notifications*", error.LocalizedDescription, null, "OK", null).Show();
		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			// show an alert
			new UIAlertView(notification.AlertAction, notification.AlertBody, null, "OK", null).Show();

			// reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}


	}
}

