using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebsite.Models;

namespace TestWebsite.Pages
{
	public class BookDetailsModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        public Book Book { get; private set; }

        public BookDetailsModel(IBookService bookService, IPublisherService publisherService, IGenreService genreService)
        {
            _bookService = bookService;
            _genreService = genreService;
            _publisherService = publisherService;
        }

        public void OnGet(int id)
        {
            LoadBookDetails(id);
        }

        public IActionResult OnPost(int bookId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<CheckoutCart>("Cart") ?? new CheckoutCart();
            cart.AddItem(bookId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            LoadBookDetails(bookId);
            return Page();
        }

        private void LoadBookDetails(int id)
        {
            Book = _bookService.GetBookDetails(id);
            if (Book != null)
            {
                Book.Publisher = _publisherService.GetPublisherByID(Book.PublisherID);
                Book.Genre = _genreService.GetGenreByID(Book.GenreID);
            }
        }
    }
}
