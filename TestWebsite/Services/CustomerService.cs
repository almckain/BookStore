using System;
namespace TestWebsite
{
	public class CustomerService : ICustomerService
	{
        public Customer GetCustomerByEmail(string email)
        {
            //Eventually will be a query
            return new Customer(1,"John Doe", "john.doe@example.com" );
        }
    }
}

