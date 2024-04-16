using System;
namespace TestWebsite.Models
{
	public class CheckoutCart
	{
        public Dictionary<int, int> Items { get; set; } = new Dictionary<int, int>();

        public void AddItem(int bookId)
        {
            if (Items.ContainsKey(bookId))
            {
                Items[bookId]++;
            }
            else
            {
                Items.Add(bookId, 1);
            }
        }

        public void RemoveItem(int bookId)
        {
            if (Items.ContainsKey(bookId))
            {
                if (Items[bookId] > 1)
                {
                    Items[bookId]--;
                }
                else
                {
                    Items.Remove(bookId);
                }
            }
        }

        public void RemoveItemCompletely(int bookId)
        {
            if (Items.ContainsKey(bookId))
            {
                Items.Remove(bookId);
            }
        }
    }
}

