using System;
namespace TestWebsite 
{
	public class BookStockUpdateModel
	{
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public int NewQuantity { get; set; }

        public BookStockUpdateModel()
        {

        }
    }
}

