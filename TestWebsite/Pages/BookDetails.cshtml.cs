using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebsite.Pages
{
	public class BookDetailsModel : PageModel
    {
        private readonly IBookService _bookService;
        public Book Book { get; private set; }

        public BookDetailsModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public void OnGet(int id)
        {
            Book = _bookService.GetBookDetails(id);
        }
    }
}
