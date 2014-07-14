﻿using System;
using System.Threading.Tasks;

namespace ClickNDone.Core
{
	public class LoginViewModel : BaseViewModel
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public User User
		{
			get{return this.settings.User;}
		}


		public async Task Login()
		{
			if (string.IsNullOrEmpty(Username))
				throw new Exception("Username is blank.");
			if (string.IsNullOrEmpty(Password))
				throw new Exception("Password is blank.");
			IsBusy = true;
			try
			{
				settings.User = await service.Login(Username, Password);
			}
			finally {
				IsBusy = false;
			}
		}

		public void IsEndUser(Boolean endUser)
		{
			this.settings.IsEnduser = endUser;
		}

		public void SetDeviceToken (String token)
		{
			this.settings.DeviceToken = token;
		}

	}
}

