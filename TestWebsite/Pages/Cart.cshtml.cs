using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebsite.Models;

namespace TestWebsite.Pages
{
	public class CartModel : PageModel
    {
        private readonly IBookService _bookService;
        public CheckoutCart Cart { get; set; }

        public CartModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public void OnGet()
        {
            Cart = HttpContext.Session.GetObjectFromJson<CheckoutCart>("Cart") ?? new CheckoutCart();
        }

        public IActionResult OnPost(int bookId, string action)
        {
            Cart = HttpContext.Session.GetObjectFromJson<CheckoutCart>("Cart") ?? new CheckoutCart();

            switch (action)
            {
                case "increase":
                    Cart.AddItem(bookId);
                    break;
                case "decrease":
                    Cart.RemoveItem(bookId);
                    break;
                case "remove":
                    Cart.RemoveItemCompletely(bookId);
                    break;
            }

            HttpContext.Session.SetObjectAsJson("Cart", Cart);
            return Page();
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var entry in Cart.Items)
            {
                var book = GetBookDetails(entry.Key);
                total += book.Price * entry.Value;
            }
            return total;
        }

        public Book GetBookDetails(int bookId)
        {
            return _bookService.GetBookDetails(bookId);
        }
    }
}
