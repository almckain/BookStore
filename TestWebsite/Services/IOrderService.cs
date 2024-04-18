using System;
namespace TestWebsite
{
	public interface IOrderService
	{
		bool PlaceOrder(int customerID, Dictionary<int,int> cart);
	}
}

