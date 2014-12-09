using System;
using System.Threading.Tasks;
using System.Collections;

namespace ClickNDone.Core
{
	public class UserModel : BaseViewModel
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public UserType UserType { get; set; }

		public string Email { get; set; }

		public string Image { get; set; }

		public string LastName { get; set; }

		public string Name { get; set; }

		public string PhoneNumber { get; set; }

		private Hashtable CachedUsers = new Hashtable ();

		public User User {
			get{ return this.settings.LoadUserLocallly (); }
		}

		public async Task Login ()
		{
			if (string.IsNullOrEmpty (Username))
				throw new Exception ("Ingrese el nombre de usuario.");
			if (string.IsNullOrEmpty (Password))
				throw new Exception ("Ingrese el password");
			IsBusy = true;
			try {
				settings.User = await service.LoginAsync (Username, Password, UserType, settings.DeviceToken);
				settings.SaveUserLocallly ();
			} finally {
				IsBusy = false;
			}
		}

		public async Task Register ()
		{
			if (string.IsNullOrEmpty (Email) || string.IsNullOrEmpty (LastName) || string.IsNullOrEmpty (PhoneNumber) || string.IsNullOrEmpty (Name))
				throw new Exception ("Ingrese los campos correctamente");
			IsBusy = true;
			try {
				User user = new User ();

				user.password = Password;
				user.mobile = PhoneNumber;
				user.email = Email;
				user.userType = UserType;
				user.names = Name;
				user.surnames = LastName;
				user.urlAvatar = Image;

				User retUser = await service.RegisterAsync (user, settings.DeviceToken);

				//Completing the User Object after SigningUp
				retUser.names = Name;
				retUser.surnames = LastName;
				retUser.email = Email;
				retUser.mobile = PhoneNumber;
				retUser.userType = UserType;
				settings.User = retUser;
				settings.SaveUserLocallly ();

			} finally {
				IsBusy = false;
			}

		}

		public async Task<User> GetUserAsync (int userId, UserType userType)
		{
			IsBusy = true;
			if (CachedUsers.ContainsKey (userId)) {
				User user = (User)CachedUsers [userId];
				if (user != null) {
					IsBusy = false;
					return user;
				} else {
					CachedUsers.Remove (userId);
					return await GetUserAsync (userId, userType);
				}
			} else {
				try {
					User loadedUser = await service.GetUserAsync (userId, userType);
					this.CachedUsers.Add (userId, loadedUser);
					return loadedUser;
				} finally {
					IsBusy = false;
				}
			}
		}

		public User GetUser (int userId, UserType userType)
		{
			IsBusy = true;
			if (CachedUsers.ContainsKey (userId)) {
				User user = (User)CachedUsers [userId];
				if (user != null) {
					IsBusy = false;
					return user;
				} else {
					CachedUsers.Remove (userId);
					return GetUser (userId, userType);
				}
			} else {
				try {
					User loadedUser = service.GetUser (userId, userType);
					this.CachedUsers.Add (userId, loadedUser);
					return loadedUser;
				} finally {
					IsBusy = false;
				}
			}
		}


		public async Task<bool> ChangePassword (int userId, UserType userType, string oldPass, string newPass)
		{
			IsBusy = true;
			try {
				await service.ChangePassword (userId,oldPass,userType,newPass);
				return true;
			} finally {
				IsBusy = false;
			}

		}



	}

}

