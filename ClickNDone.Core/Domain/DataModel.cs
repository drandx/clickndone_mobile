using System;
using System.Collections.Generic;

namespace ClickNDone.Core
{

	public class User
	{
		public User()
		{
			this.price = "";
		}
		public int id { get; set;}
		public string password { get; set; }
		public DateTime birthAge { get; set;}
		public string email { get; set;}
		public string names { get; set;}
		public string surnames { get; set;}
		public string mobile { get; set;}
		public string gender { get; set;}
		public UserType userType { get; set;}
		public string urlAvatar { get; set;}
		public string sessionToken { get; set;}
		public string price { get; set;}
		public float ranking { get; set;}
	}

	public class LoginObj
	{
		public string email { get; set; }
		public string pwd { get; set; }
		public string userType { get; set; }
	}

	public class TermsConditions
	{
		public string terms { get; set; }
		public string conditions { get; set; }
	}

	public class Category
	{
		public int Id { get; set;}
		public int ParentId { get; set;}
		public string Name { get; set; }
		public string Description { get; set; }
		public string Convention { get; set; }
		public List<Category> Subcategories { get; set; }
		public string ImageName { get; set;}
	}

	public class ServiceRequest
	{
		public string Location { get; set; }
		public string Reference { get; set; }
		public string MinCost { get; set; }
		public string MaxCost { get; set; }
		public DateTime ReservationDate { get; set; }
		public Category SubCategory { get; set;}
	}

	public class ServicePrices
	{
		public string Value { get; set;}
	}

	public class Order
	{
		public int Id { get; set;}
		public ServiceState Status { get; set; }
		public int UserId { get; set;}
		public int SupplierId { get; set;}
		public string ClickCode { get; set;}
		public int CategoryId { get; set;}
		public int SubCategoryId { get; set;}
		public DateTime ReservationDate { get ; set; }
		public string Time { get; set;}
		public string MinCost { get; set; }
		public string MaxCost { get; set; }
		public string Location { get; set; }
		public string Reference { get; set; }
		public string Comments { get; set; }

		public Category SubCategory { get; set;}
		public User Supplier { get; set;}
		public User User { get; set;}


		public Order()
		{
			this.Supplier = new User ();
			this.User = new User ();
		}

		public string GetReservationDate()
		{
			return this.ReservationDate.ToString ("MMMM dd, yyyy");
		}

		public string GetReservationTime()
		{
			return this.Time;
		}

	}

}

