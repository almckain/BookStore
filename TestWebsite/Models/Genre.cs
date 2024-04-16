using System;

namespace TestWebsite
{
	public class Genre
	{
        public int GenreID { get; set; }
        public string GenreName { get; set; }

        public Genre(string name, int id)
		{
			GenreID = id;
			GenreName = name;
		}

		public Genre()
		{

		}
	}
}

