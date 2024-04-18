using System;
namespace TestWebsite
{
	public interface IAdminService
	{
		List<Book> GetLowStockBooks();
	}
}

