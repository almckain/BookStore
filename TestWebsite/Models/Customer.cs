using System;
namespace TestWebsite
{
	public class Customer
	{
		public int CustomerID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public int OrderCount { get; set; }
		public decimal TotalMoneySpent { get; set; }


		public Customer(int custumerId, string name, string email)
		{
			CustomerID = custumerId;
			Name = name;
			Email = email;
		}

		public Customer(string name, string email)
		{
			Name = name;
			Email = email;
		}

		public Customer() { }

	}
}

