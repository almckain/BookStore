using System;
using System.Collections.Generic;
namespace TestWebsite
{
	public interface IBookService
	{
		List<Book> GetBooks();
		List<Book> GetBestSellers();
		List<Book> GetBooksByOrderNumber(int orderNumber);
		List<Genre> GetAllGenres();
		List<Book> GetBooksByGenres(List<int> id);
		Book GetBookDetails(int id);
	}
}

