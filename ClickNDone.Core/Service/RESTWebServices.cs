using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ClickNDone.Core
{
	public class RESTWebServices : IWebService
	{

		private WebClient client;

		public int SleepDuration { get; set; }

		private Task Sleep ()
		{
			return Task.Delay (SleepDuration);
		}
		/*
		 * 
		 * 
		 * 
		 */
		public RESTWebServices ()
		{
			SleepDuration = 1;
			client = new WebClient ();
		}
		/*
		 * 
		 * 
		 * 
		 */
		public async Task<User> LoginAsync (string username, string password, UserType userType, String deviceToken)
		{
			client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
			client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 

			LoginObj loginObj = new LoginObj ();
			loginObj.pwd = password;
			loginObj.email = username;
			loginObj.userType = userType.ToString ();

			var json = JsonConvert.SerializeObject (loginObj);
			string url = Constants.WebServiceHost + "login";

			client.Headers.Set ("X-Origin-OS", "Iphone 7");
			client.Headers.Set ("X-Origin-Token", deviceToken);
			client.Headers.Set ("User-Agent", "IOS7");
			var response = await client.UploadStringTaskAsync (url, "POST", json);
			var objResp = JObject.Parse (response);

			User user = new User ();
			user.password = password;
			user.email = objResp ["email"].ToString ();
			user.names = objResp ["names"].ToString ();
			user.surnames = objResp ["surnames"].ToString ();
			//TODO-Use this property to store session token
			//user.sessionToken = objResp["token"].ToString();
			user.sessionToken = "";
			user.userType = (objResp ["userType"].ToString () == UserType.CONSUMER.ToString ()) ? UserType.CONSUMER : UserType.SUPPLIER;
			user.birthAge = new DateTime (); //TODO - Create DateTime object
			user.mobile = objResp ["cellphone"].ToString ();
			var iduser = objResp ["id"].ToString ();
			user.id = iduser == "" || iduser == null ? 0 : Convert.ToInt32 (iduser);

			return user;
		}
		/*
		 * 
		 * 
		 * 
		 */
		public async Task<TermsConditions> GetTermsConditionsAsync (bool isEndUser)
		{
			string url = Constants.WebServiceHost + "terms_conditions/";
			if (!isEndUser)
				url = url + "SERVICE_PROVIDER_TYPE";
			else
				url = url + "END_USER_TYPE";
			var response = await client.DownloadStringTaskAsync (url);
			var terms = JsonConvert.DeserializeObject<TermsConditions> (response);

			return terms;
		}
		/*
		 * 
		 * 
		 * 
		 */
		public async Task<User> RegisterAsync (User user, String deviceToken)
		{
			string url = Constants.WebServiceHost + "signup";

			client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
			client.Headers.Add (HttpRequestHeader.ContentType, "application/json");
			client.Headers.Set ("X-Origin-OS", "Iphone 7");
			client.Headers.Set ("X-Origin-Token", deviceToken);
			client.Headers.Set ("User-Agent", "IOS7");

			IDictionary<String,Object> userAttributes = new Dictionary<string, object> ();
			userAttributes.Add ("username", user.email);
			userAttributes.Add ("password", user.password);
			userAttributes.Add ("email", user.email);
			userAttributes.Add ("names", user.names);
			userAttributes.Add ("surnames", user.surnames);
			userAttributes.Add ("mobile", user.mobile);
			userAttributes.Add ("userType", user.userType.ToString ());

			var userJson = JsonConvert.SerializeObject (userAttributes);

			var response = await client.UploadStringTaskAsync (url, "POST", userJson);
			var objResp = JObject.Parse (response);

			//TODO-Pedir que me devuelvan un token basura
			User myUser = new User ();
			//myUser.sessionToken = objResp["token"].ToString();
			return myUser;

		}
		/*
		 * 
		 * 
		 * 
		 */
		public async Task<List<Category>> GetCategoriesAsync (String sessionToken, String deviceToken)
		{			
			List<Category> categoriesRet = new List<Category> ();
			try {
				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("X-Origin-Token", deviceToken);
				client.Headers.Set ("User-Agent", "IOS7");

				string url = Constants.WebServiceHost + "allcategories";

				var responseString = await client.DownloadStringTaskAsync (url);
				var objResp = JObject.Parse (responseString);

				foreach (var cat in objResp["category"]) {
					List<Category> subCategories = new List<Category> ();
					Category category = new Category ();
					category.Name = cat ["categoryName"].ToString ();
					category.Description = cat ["categoryDescription"].ToString ();
					category.Convention = cat ["categoryConvention"].ToString ();
					var idCat = cat ["id"].ToString ();
					category.Id = idCat == "" || idCat == null ? 0 : Convert.ToInt32 (idCat);
					if(category.Id == 1)
						category.ImageName = "logo_beauty.png";
					if(category.Id == 2)
						category.ImageName = "logo_home.png";

					foreach (var subcat in cat["subCategories"]) {
						Category subcategory = new Category ();
						subcategory.Name = subcat ["subCategoryName"].ToString ();
						subcategory.Description = subcat ["subCategoryDescription"].ToString ();
						subcategory.Convention = subcat ["subCategoryConvention"].ToString ();
						var idSubCat = subcat ["id_sub"].ToString ();
						subcategory.Id = idSubCat == "" || idSubCat == null ? 0 : Convert.ToInt32 (idSubCat);
						subcategory.ParentId = category.Id;

						if((subcategory.Name != "" ) && (subcategory != null))
							subCategories.Add (subcategory);

					}

					category.Subcategories = subCategories;
					categoriesRet.Add (category);
				}
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on GetCategoriesAsync - " + exc.Message);

			}
			return categoriesRet;
		}
		/*
		 * 
		 * 
		 * 
		 */
		public async Task<int> RequestServiceAsync (ServiceRequest order, String sessionToken, String deviceToken, int userId)
		{
			client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
			client.Headers.Add (HttpRequestHeader.ContentType, "application/json");
			client.Headers.Set ("X-Origin-OS", "Iphone 7");
			client.Headers.Set ("X-Origin-Token", deviceToken);
			client.Headers.Set ("User-Agent", "IOS7");

			string url = Constants.WebServiceHost + "requestservice";

			IDictionary<String,Object> serviceRequestAttributes = new Dictionary<string, object> ();
			serviceRequestAttributes.Add ("reference", order.Reference);
			serviceRequestAttributes.Add ("minimumCost", order.MinCost);
			serviceRequestAttributes.Add ("maximumCost", order.MaxCost);
			serviceRequestAttributes.Add ("location", order.Location);
			serviceRequestAttributes.Add ("reservationDate", order.ReservationDate.ToString ("yyyy-MM-dd"));
			serviceRequestAttributes.Add ("hora", order.ReservationDate.ToString ("hh:mm tt"));
			serviceRequestAttributes.Add ("allowanceToken", sessionToken);
			serviceRequestAttributes.Add ("iduser", userId);
			serviceRequestAttributes.Add ("category", order.SubCategory.ParentId);
			serviceRequestAttributes.Add ("idsub_category", order.SubCategory.Id);

			var serviceRequestJson = JsonConvert.SerializeObject (serviceRequestAttributes);

			var response = await client.UploadStringTaskAsync (url, "POST", serviceRequestJson);
			var objResp = JObject.Parse (response);
			return Convert.ToInt16 (objResp ["serviceID"].ToString ());
		}
		/*
		 * 
		 * 
		 * 
		 */
		public Task<User> GetUserAsync (int userId, UserType UserType)
		{
			try {
				return Task.Run (() => GetUser (userId, UserType));
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on GetUserAsync - " + exc.Message);
				return null;
			}
		}

		/*
		 * 
		 * 
		 * 
		 */
		public User GetUser (int userId, UserType UserType)
		{
			string userJson = "";

			try {

				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("User-Agent", "IOS7");

				string url = Constants.WebServiceHost + "getuser";

				IDictionary<String,Object> userAttributes = new Dictionary<string, object> ();
				userAttributes.Add ("id", userId);
				userAttributes.Add ("userType", UserType.ToString ());

				userJson = JsonConvert.SerializeObject (userAttributes);
				var response = client.UploadString (url, "POST", userJson);

				var objResp = JObject.Parse (response);
				User user = new User ();
				user.id = userId;
				user.birthAge = new DateTime ();
				user.email = objResp ["email"].ToString ();
				user.names = objResp ["names"].ToString ();
				user.surnames = objResp ["surnames"].ToString ();
				user.urlAvatar = objResp ["urlAvatar"].ToString ();
				user.userType = UserType;
				user.mobile = objResp ["cellphone"].ToString ();

				return user;
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on GetUser - " + exc.Message + "Json To Send: "+userJson);
				return null;
			}
		}

		/*
		 * 
		 * 
		 * 
		 */
		public Task<Order> GetOrderAsync (int orderId)
		{
			try {
				return Task.Run (() => GetOrder (orderId));
			} catch (Exception exc) {
				Console.WriteLine ("Crashing when gets order Async - " + exc.Message);
				return null;
			}

		}


		/*
		 * 
		 * 
		 * 
		 */
		public Order GetOrder (int orderId)
		{
			try {

				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json");
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("X-Origin-Token", "");
				client.Headers.Set ("User-Agent", "IOS7");

				string url = Constants.WebServiceHost + "GetOrderDetail";

				IDictionary<String,Object> orderAttributes = new Dictionary<string, object> ();
				orderAttributes.Add ("orderId", orderId);

				var orderJson = JsonConvert.SerializeObject (orderAttributes);
				Order order = new Order ();

				if (!client.IsBusy) {
					var response = client.UploadString (url, "POST", orderJson);
					var objResp = JObject.Parse (response);
					order.Id = orderId;

					var idUser = objResp ["id_user"].ToString ();
					var idSupplier = objResp ["id_proveedor"].ToString ();
					var catId = objResp ["id_category"].ToString ();
					var subCatId = objResp ["id_subcategory"].ToString ();

					var status = objResp ["status"].ToString ();
					order.Status = status == "" || status == null ? ServiceState.UNKNOWN : (ServiceState)Convert.ToInt32 (status);

					order.UserId = idUser == "" || idUser == null ? 0 : Convert.ToInt32 (idUser);
					order.SupplierId = idSupplier == "" || idSupplier == null ? 0 : Convert.ToInt32 (idSupplier);
					order.ClickCode = objResp ["code_click"].ToString ();
					order.CategoryId = catId == "" || catId == null ? 0 : Convert.ToInt32 (catId);
					order.SubCategoryId = subCatId == "" || subCatId == null ? 0 : Convert.ToInt32 (subCatId);
					var reservation_date = objResp ["reservation_date"].ToString ();
					DateTime finalDate;
					try {
						finalDate = reservation_date == "" || reservation_date == null ? new DateTime () : Convert.ToDateTime (reservation_date);
					} catch (Exception exc) {
						Console.WriteLine ("Issues parsing date from service: " + exc.Message);
						finalDate = new DateTime();
					}
					order.ReservationDate = finalDate;
					order.MinCost = objResp ["minimum_cost"].ToString ();
					order.MaxCost = objResp ["maximum_cost"].ToString ();
					order.Location = objResp ["location"].ToString ();
					order.Reference = objResp ["reference"].ToString ();
					order.Comments = objResp ["comments"].ToString ();
					order.Time = objResp ["time"].ToString ();
					return order;
				}
				return null;

			} catch (Exception exc) {
				Console.WriteLine ("Crashing when gets order - " + exc.Message);
				return null;
			}

		}

		/*
		* 
		* 
		* 
		*/
		public Task<List<Order>> GetOrdersListAsync (int userId, ServiceState orderState, UserType userType)
		{
			try {
				return Task.Run (() => GetOrdersList (userId, orderState, userType));
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on GetOrdersListAsync - " + exc.Message);
				return null;
			}
		}

		/*
		* 
		* 
		* 
		*/
		public List<Order> GetOrdersList (int userId, ServiceState orderState, UserType userType)
		{
			string orderJson = ""; 
			try {
				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("User-Agent", "IOS7");

				string url = Constants.WebServiceHost + "GetOrdersList";

				IDictionary<String,Object> orderAttributes = new Dictionary<string, object> ();
				orderAttributes.Add ("STATE", (int)orderState);
				orderAttributes.Add ("UserType", userType.ToString ());
				orderAttributes.Add ("UserID", userId);
				List<Order> ordersList = new List<Order> ();

				if (!client.IsBusy) {
					orderJson = JsonConvert.SerializeObject (orderAttributes);
					var response = client.UploadString (url, "POST", orderJson);
					var objListResp = JObject.Parse (response);

					foreach (var objResp in objListResp["orders"]) {
						Order orderItem = new Order ();

						var status = objResp ["status"].ToString ();
						orderItem.Status = status == "" || status == null ? ServiceState.UNKNOWN : (ServiceState)Convert.ToInt32 (status);
						var idUser = objResp ["id_user"].ToString ();
						orderItem.UserId = idUser == "" || idUser == null ? 0 : Convert.ToInt32 (idUser);
						var idSupplier = objResp ["id_proveedor"].ToString ();
						orderItem.SupplierId = idSupplier == "" || idSupplier == null ? 0 : Convert.ToInt32 (idSupplier);
						orderItem.ClickCode = objResp ["code_click"].ToString ();
						var catId = objResp ["id_category"].ToString ();
						orderItem.CategoryId = catId == "" || catId == null ? 0 : Convert.ToInt32 (catId);
						var subCatId = objResp ["id_subcategory"].ToString ();
						orderItem.SubCategoryId = subCatId == "" || subCatId == null ? 0 : Convert.ToInt32 (subCatId);
						var reservation_date = objResp ["reservation_date"].ToString();
						DateTime finalDate;
						try {
							finalDate = reservation_date == "" || reservation_date == null ? new DateTime () : Convert.ToDateTime (reservation_date);
						} catch (Exception exc) {
							Console.WriteLine ("Issues parsing date from service: " + exc.Message);
							finalDate = new DateTime();
						}
						orderItem.ReservationDate = finalDate;
						orderItem.MinCost = objResp ["minimum_cost"].ToString ();
						orderItem.MaxCost = objResp ["maximum_cost"].ToString ();
						orderItem.Location = objResp ["location"].ToString ();
						orderItem.Reference = objResp ["reference"].ToString ();
						orderItem.Comments = objResp ["comments"].ToString ();
						orderItem.Time = objResp ["time"].ToString ();
						var idOder = objResp ["id_orden"].ToString ();
						orderItem.Id = idOder == "" || idOder == null ? 0 : Convert.ToInt32 (idOder);

						ordersList.Add (orderItem);
					}
				}
				return ordersList;
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on GetOrdersList - " + exc.Message + " "+orderJson);
				return null;
			}


		}

		/*
		* 
		* 
		* 
		*/
		public async Task<bool> ChangeOrderStateAsync (int orderId, ServiceState state, string comments = null, string ranking = null)
		{
			try {
				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("User-Agent", "IOS7");

				string url = Constants.WebServiceHost + "change_order_state";
				IDictionary<String,Object> orderAttributes = new Dictionary<string, object> ();
				orderAttributes.Add ("STATE", (int)state + "");
				orderAttributes.Add ("OrderID", orderId);

				if (comments != null)
					orderAttributes.Add ("comments", comments);
					
				if(ranking != null)
					orderAttributes.Add ("rate", ranking);


				if (!client.IsBusy) {
					var orderJson = JsonConvert.SerializeObject (orderAttributes);
					await client.UploadStringTaskAsync (url, "POST", orderJson);
					return true;
				}
				return true;
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on ChangeOrderStateAsync - " + exc.Message);
				return false;
			}
		}

		/*
		* 
		* 
		* 
		*/
		public async Task<List<ServicePrices>> GetServicePrices ()
		{
			List<ServicePrices> prices = new List<ServicePrices> ();
			try{
				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("User-Agent", "IOS7");
				string url = Constants.WebServiceHost + "get_price_range";

				if (!client.IsBusy) {
					var response = await client.DownloadStringTaskAsync (url);
					var objListResp = JObject.Parse (response);
					foreach (var objResp in objListResp["range"]) {
						ServicePrices price = new ServicePrices();
						price.Value =  objResp ["price"].ToString ();
						prices.Add(price);
					}
				}


			}
			catch(Exception exc){
				Console.WriteLine ("Crashing on GetServicePrices - " + exc.Message);
			}
			return prices;

		}

		/**
		 * 
		 * 
		 * 
		 * */
		public async Task<bool> PostSuggestion(int userId, UserType userType, string message)
		{
			try {
				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("User-Agent", "IOS7");

				string url = Constants.WebServiceHost + "InsertMessage";
				IDictionary<String,Object> messageAttributes = new Dictionary<string, object> ();
				messageAttributes.Add ("mensaje", message);
				messageAttributes.Add ("iduser", userId);
				messageAttributes.Add ("userType", userType.ToString());

				if (!client.IsBusy) {
					var orderJson = JsonConvert.SerializeObject (messageAttributes);
					await client.UploadStringTaskAsync (url, "POST", orderJson);
					return true;
				}
				return true;
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on PostSuggestion - " + exc.Message);
				return false;
			}

		}


		/**
		 * 
		 * 
		 * 
		 * */
		public async Task<bool> RateUser(int userId, string message, double rate, int id_order, UserType userType)
		{
			try {
				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("User-Agent", "IOS7");

				string url = Constants.WebServiceHost + "InsertRating";
				IDictionary<String,Object> messageAttributes = new Dictionary<string, object> ();
				messageAttributes.Add ("iduser", userId);
				messageAttributes.Add ("calificacion", rate);
				messageAttributes.Add ("mensaje", message);
				messageAttributes.Add ("id_orden", id_order);
				messageAttributes.Add ("type", userType.Equals(UserType.CONSUMER) ? 1 : 2);

				if (!client.IsBusy) {
					var orderJson = JsonConvert.SerializeObject (messageAttributes);
					await client.UploadStringTaskAsync (url, "POST", orderJson);
					return true;
				}
				return true;
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on RateUser - " + exc.Message);
				return false;
			}

		}


		/**
		 * 
		 * 
		 * 
		 * */
		public async Task<bool> ChangePassword(int userId, string oldPass, UserType userType, string newPass)
		{
			try {
				client.Headers.Add (HttpRequestHeader.Accept, "application/json"); 
				client.Headers.Add (HttpRequestHeader.ContentType, "application/json"); 
				client.Headers.Set ("X-Origin-OS", "Iphone 7");
				client.Headers.Set ("User-Agent", "IOS7");

				string url = Constants.WebServiceHost + "changePassword";
				IDictionary<String,Object> messageAttributes = new Dictionary<string, object> ();
				messageAttributes.Add ("iduser", userId);
				messageAttributes.Add ("pwd", oldPass);
				messageAttributes.Add ("userType", userType.ToString());
				messageAttributes.Add ("newpassword", newPass);

				if (!client.IsBusy) {
					var orderJson = JsonConvert.SerializeObject (messageAttributes);
					await client.UploadStringTaskAsync (url, "POST", orderJson);
					return true;
				}
				return true;
			} catch (Exception exc) {
				Console.WriteLine ("Crashing on ChangePassword - " + exc.Message);
				return false;
			}
		}

	}
}

