using System;
namespace TestWebsite
{
	public interface IOrderService
	{
		List<Order> GetOrdersForCustomer(int customerID);
		void PlaceOrder(int customerID, Dictionary<int,int> cart);
	}
}

