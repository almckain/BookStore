using System;
namespace TestWebsite
{
	public class Publisher
	{
		public int PublisherID { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string StreetAddress { get; set; }

		public Publisher()
		{
		}

		public Publisher(int id, string name)
		{
			PublisherID = id;
			Name = name;
		}
	}
}

