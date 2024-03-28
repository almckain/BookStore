using System;
namespace TestWebsite
{
	public class OrderServices : IOrderService
	{
        public List<Order> GetOrdersForCustomer(int customerId)
        {
            /*
            * Eventually will be a query that returns all orders
            */
            return new List<Order>
            {
                new Order
                {
                    OrderNumber = 1,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2002, 4, 16),
                    Total = 36.40m
                },
                new Order
                {
                    OrderNumber = 2,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2003, 7, 21),
                    Total = 52.25m
                },
                new Order
                {
                    OrderNumber = 3,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2005, 11, 2),
                    Total = 19.99m
                },
                new Order
                {
                    OrderNumber = 4,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2007, 5, 30),
                    Total = 99.95m
                },
                new Order
                {
                    OrderNumber = 5,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2010, 3, 15),
                    Total = 75.50m
                },
            };
        }


        public OrderServices()
		{
		}
	}
}

