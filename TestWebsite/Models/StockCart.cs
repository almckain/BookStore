using System;
namespace TestWebsite
{
	public class StockCart
	{
        public Dictionary<int, int> StockChanges { get; private set; } = new Dictionary<int, int>();

        public StockCart()
        {
        }

        public void AddItem(int bookId)
        {
            if (StockChanges.ContainsKey(bookId))
            {
                StockChanges[bookId]++;
            }
            else
            {
                StockChanges.Add(bookId, 0);
            }
        }

        public void RemoveItem(int bookId)
        {
            if (StockChanges.ContainsKey(bookId))
            {
                if (StockChanges[bookId] > 1)
                {
                    StockChanges[bookId]--;
                }
                else
                {
                    StockChanges.Remove(bookId);
                }
            }
        }
    }
}

