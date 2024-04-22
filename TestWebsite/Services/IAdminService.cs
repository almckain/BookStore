using System;
namespace TestWebsite
{
	public interface IAdminService
	{
		List<Book> GetLowStockBooks();
		List<RecentStockRefillViewModel> GetRecentStockRefills();
		List<Customer> GetTopCustomers();

    }
}

