using System;
namespace TestWebsite
{
	public interface IPublisherService
	{
		Publisher GetPublisherByID(int publisherID);
		bool PlacePublisherOrder(Dictionary<int, int[]> cart);
		List<Publisher> GetAllPublishers();
    }
}

