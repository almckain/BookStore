using System;
namespace TestWebsite
{
    public class MonthlyProfit
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Profit { get; set; }

        public string MonthYearDisplay => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
    }
}

