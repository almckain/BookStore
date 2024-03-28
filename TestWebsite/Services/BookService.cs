using System;
namespace TestWebsite
{
	public class BookService : IBookService
	{
        /*
		 * Eventually will be a call to the database for the books
		 */
        public List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book("Harry Potter and the Sorcerer's Stone", "J. K. Rowling", 12.99m, "/images/HarryPotter1.png"),
                new Book("To Kill a Mockingbird", "Harper Lee", 15.49m, "/images/ToKillAMockingbird.png"),
                new Book("Game of Thrones: A Song of Ice and Fire", "George R. R. Martin", 15.99m, "/images/GameOfThrones.png"),
                new Book("The Great Gatsby", "F. Scott Fitzgerald", 13.99m, "/images/GreatGatsby.png"),
                new Book("Fahrenheit 451: A Novel", "Ray Bradbury", 13.99m, "/images/Fahrenheit451.png"),
                new Book("Of Mice and Men", "John Steinbeck", 11.99m, "/images/OfMiceAndMen.png"),
                new Book("Great Expectations", "Charles Dickens", 8.95m, "/images/GreatExpectations.png"),
                new Book("Project Hail Mary", "Andy Weir", 18.00m, "/images/ProjectHailMary.png")
            };
        }

        /*
         * Eventually will be a call to the database for the best sellers
         */
        public List<Book> GetBestSellers()
        {
            return new List<Book>
            {
                new Book("Of Mice and Men", "John Steinbeck", 11.99m, "/images/OfMiceAndMen.png"),
                new Book("Harry Potter and the Sorcerer's Stone", "J. K. Rowling", 12.99m, "/images/HarryPotter1.png"),
                new Book("The Great Gatsby", "F. Scott Fitzgerald", 13.99m, "/images/GreatGatsby.png")
            };
        }

        /*
         *  Eventually there may be another query to return all the books with a given order id???
         */
        public List<Book> GetBooksByOrderNumber(int orderNumber)
        {
            // Hardcoded for demonstration; you'll replace this with actual logic to fetch data
            var allBooks = GetBooks();
            //return allBooks.Where(book => book.OrderNumber == orderNumber).ToList();
            return null;
        }

        public BookService()
		{
		}
	}
}

