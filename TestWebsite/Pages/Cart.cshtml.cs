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
        private readonly IOrderService _orderService;

        public CheckoutCart Cart { get; set; }

        public CartModel(IBookService bookService, IOrderService orderService)
        {
            _bookService = bookService;
            _orderService = orderService;
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
                case "checkout":
                    return ProcessCheckout();
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

        private IActionResult ProcessCheckout()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (!customerId.HasValue)
            {
                ModelState.AddModelError("", "You must be logged in to complete the checkout.");
                return Page();
            }

            bool isSuccess = _orderService.PlaceOrder(customerId.Value, Cart.Items);
            if (isSuccess)
            {
                HttpContext.Session.Remove("Cart");  // Clear the cart after successful checkout
                return RedirectToPage("/OrderConfirmation");  // Redirect to a confirmation page
            }
            else
            {
                ModelState.AddModelError("", "Failed to process the checkout.");
                return Page();
            }
        }
    }
}
