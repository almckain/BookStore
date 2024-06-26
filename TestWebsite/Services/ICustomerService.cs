﻿using System;
namespace TestWebsite
{
	public interface ICustomerService
	{
		Customer GetCustomerByEmail(string email);
		List<Order> ReturnOrdersByCustomers(int customerID);
		bool CreateNewCustomer(string name, string email);
	}
}

