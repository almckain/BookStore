using System;
using TestWebsite.Models;

namespace TestWebsite
{
	public interface IAdminService
	{
		List<Book> GetLowStockBooks();
		List<RecentStockRefillViewModel> GetRecentStockRefills();
		List<Customer> GetTopCustomers();
		List<MonthlyProfit> GetMonthlyProfit();
    }
}

