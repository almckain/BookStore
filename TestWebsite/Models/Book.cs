using System;
namespace TestWebsite
{
	public class Book
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public decimal Price { get; set; }
		public string ImageURL { get; set; }
		public int BookID { get; set; }

		public Book(string title, string author, decimal price, string imageURL, int id)
		{
			Title = title;
			Author = author;
			Price = price;
			ImageURL = imageURL;
			BookID = id;
		}

		public Book()
		{

		}

	}
}

