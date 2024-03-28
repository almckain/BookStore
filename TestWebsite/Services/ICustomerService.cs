using System;
namespace TestWebsite
{
	public interface ICustomerService
	{
		Customer GetCustomerByEmail(string email);
	}
}

