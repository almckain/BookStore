﻿using System;
using System.Collections.Generic;
namespace TestWebsite
{
	public interface IBookService
	{
		List<Book> GetBooks();
		List<Book> GetBestSellers();
		List<Genre> GetAllGenres();
		List<Book> GetBooksByGenres(List<int> id);
		List<Book> GetBooksByPublishers(int id);
		Book GetBookDetails(int id);
	}
}

