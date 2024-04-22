using System;
namespace TestWebsite
{
	public class RecentStockRefillViewModel
	{
		public string BookTitle { get; set; }
		public string PublisherName { get; set; }
		public int QuantityOrdered { get; set; }
		public DateTime OrderDate { get; set; }

		public RecentStockRefillViewModel()
		{
		}
	}
}

