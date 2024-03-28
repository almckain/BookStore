using System;
namespace TestWebsite
{
	public interface IOrderService
	{
		List<Order> GetOrdersForCustomer(int customerID);
	}
}

