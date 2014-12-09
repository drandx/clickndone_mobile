using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClickNDone.Core
{
	public class OrdersModel:BaseViewModel
	{
		public int RequestedOrderId { get; set;}
		public Order RequestedOrder { get; set;}
		public List<Order> SupplierAgenda { get; set;}
		public string Location { get; set; }
		public string Comments { get; set; }
		public string MinCost { get; set; }
		public string MaxCost { get; set; }
		public DateTime ReservationDate { get; set; }

		public DateTime InitTime { get; set;}
		public DateTime EndTime { get; set;}


		public OrdersModel ()
		{
			this.RequestedOrder = new Order ();
		}

		public async Task<bool> RequestService(Category selectedSubCategroy, Action RequestCallback)
		{
			if (string.IsNullOrEmpty(Location) || string.IsNullOrEmpty(Comments) || string.IsNullOrEmpty(MinCost.ToString()) 
				|| string.IsNullOrEmpty(MaxCost.ToString()) || string.IsNullOrEmpty(ReservationDate.ToString()))
				throw new Exception("Ingrese los campos correctamente");
			IsBusy = true;
			try
			{
				ServiceRequest serviceRequest = new ServiceRequest();
				serviceRequest.Reference = this.Comments;
				serviceRequest.Location = Location;
				serviceRequest.MinCost = MinCost;
				serviceRequest.MaxCost = MaxCost;
				serviceRequest.ReservationDate = ReservationDate;
				serviceRequest.SubCategory = selectedSubCategroy;
				var ret = await service.RequestServiceAsync(serviceRequest,settings.User.sessionToken,settings.DeviceToken, settings.User.id);
				this.RequestedOrderId = ret;
				return true;
			}
			finally {
				IsBusy = false;
				RequestCallback();
			}

		}

		public async Task<Order> GetOrderAsync(int orderId)
		{
			try
			{
				IsBusy = true;
				var retOrder = await service.GetOrderAsync(orderId);
				this.RequestedOrder = retOrder;
				return retOrder;

			}
			finally {
				IsBusy = false;
			}

		}

		public Order GetOrder(int orderId)
		{
			try
			{
				var retOrder = service.GetOrder(orderId);
				this.RequestedOrder = retOrder;
				return retOrder;

			}
			finally {
			}

		}


		public async Task<List<Order>> GetOrdersListAsync(int userId, ServiceState state, UserType userType)
		{
			try
			{
				IsBusy = true;
				var retOrdersList = await service.GetOrdersListAsync(userId,state,userType);
				return retOrdersList;
			}
			finally {
				IsBusy = false;
			}

		}

		public List<Order> GetOrdersList(int userId, ServiceState state, UserType userType)
		{
			try
			{
				IsBusy = true;
				var retOrdersList = service.GetOrdersList(userId,state,userType);
				return retOrdersList;
			}
			finally {
				IsBusy = false;
			}

		}

		public async Task<bool> ChangeRequestedOrderStateAsync(ServiceState state, string comments = null, string ranking = null)
		{
			try
			{
				IsBusy = true;
				var retOrder = await service.ChangeOrderStateAsync(this.RequestedOrder.Id, state, comments, ranking);
				return retOrder;

			}
			finally {
				this.RequestedOrder.Status = state;
				IsBusy = false;

			}

		}


		public async Task<bool> PostSuggestion(int userId, UserType userType, string message)
		{
			try
			{
				IsBusy = true;
				var retSuggestion = await service.PostSuggestion(userId, userType, message);
				return retSuggestion;
			}
			finally {
				IsBusy = false;
			}

		}

		public async Task<bool> RateUser(int userId, string message, double rate, int id_order, UserType userType)
		{
			try
			{
				IsBusy = true;
				var retSuggestion = await service.RateUser(userId, message, rate, id_order, userType);
				return retSuggestion;
			}
			finally {
				IsBusy = false;
			}

		}

	}
}

