using System;
using TestWebsite;

namespace TestWebsite
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        //public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}

