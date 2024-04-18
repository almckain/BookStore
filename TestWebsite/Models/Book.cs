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
		public int PublisherID { get; set; }
		public int GenreID { get; set; }

		public string ISBN { get; set; }
		public Publisher Publisher { get; set; }
		public Genre Genre { get; set; }
		public int StockQuantity { get; set; }

		public Book(string title, string author, decimal price, string imageURL, int id)
		{
			Title = title;
			Author = author;
			Price = price;
			ImageURL = imageURL;
			BookID = id;
		}

        public Book(string title, string author, decimal price, string imageURL, int id, string isbn, int publisherID, int genreID, int stockQuantity)
        {
            Title = title;
            Author = author;
            Price = price;
            ImageURL = imageURL;
            BookID = id;
			ISBN = isbn;
			PublisherID = publisherID;
			GenreID = genreID;
			StockQuantity = stockQuantity;
        }

        public Book()
		{

		}

	}
}

