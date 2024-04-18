using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Collections.Specialized.BitVector32;

namespace TestWebsite.Pages
{
	public class AdminDashboardModel : PageModel
    {
        private readonly IAdminService _adminService;
        private readonly IBookService _bookService;

        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }

        public List<Book> LowStockBooks { get; set; }

        [BindProperty]
        public Dictionary<int, int> StockUpdates { get; set; } = new Dictionary<int, int>();

        public AdminDashboardModel(IAdminService adminService, IBookService bookService)
        {
            _adminService = adminService;
            _bookService = bookService;
        }

        public void OnGet()
        {
            LoadDashboardData();
            LowStockBooks = _adminService.GetLowStockBooks();
            foreach(var book in LowStockBooks)
            {
                StockUpdates.Add(book.BookID, 0);
            }
        }

        private void LoadDashboardData()
        {
            CustomerName = HttpContext.Session.GetString("CustomerName") ?? "Not logged in";
            CustomerEmail = HttpContext.Session.GetString("CustomerEmail") ?? "No email";
        }

        public IActionResult OnPostUpdateStock()
        {
            foreach (var update in StockUpdates)
            {
                var bookId = update.Key;
                var newQuantity = update.Value;

                if (newQuantity < 0)
                {
                    ModelState.AddModelError("", "Stock quantity cannot be negative.");
                    return Page();
                }

                //_adminService.UpdateStock(bookId, newQuantity);
            }

            return RedirectToPage();
        }
    }
}
