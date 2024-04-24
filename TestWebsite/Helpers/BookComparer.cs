using System;
namespace TestWebsite
{
	public class BookComparer: IEqualityComparer<Book>
	{
		public BookComparer()
		{
		}

		public bool Equals(Book x, Book y)
		{
			return x.BookID == y.BookID;
		}

		public int GetHashCode(Book obj)
		{
			return obj.BookID.GetHashCode();
		}
	}
}

