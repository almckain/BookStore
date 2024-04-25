using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Collections.Specialized.BitVector32;

namespace TestWebsite.Pages
{
	public class AdminDashboardModel : PageModel
    {
        private readonly IAdminService _adminService;
        private readonly IPublisherService _publisherService;
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }
        public List<int> publisherIds = new List<int>();
        public List<Book> LowStockBooks { get; set; }
        public List<RecentStockRefillViewModel> RecentStockRefills { get; set; }
        public List<Customer> TopCustomers { get; set; }
        public List<MonthlyProfit> MonthlyProfits { get; set; }


        [BindProperty]
        public Dictionary<int, int> StockUpdates { get; set; } = new Dictionary<int, int>();

        public AdminDashboardModel(IAdminService adminService, IPublisherService publisherService)
        {
            _adminService = adminService;
            _publisherService = publisherService;
        }

        public void OnGet()
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            CustomerName = HttpContext.Session.GetString("CustomerName") ?? "Not logged in";
            CustomerEmail = HttpContext.Session.GetString("CustomerEmail") ?? "No email";
            LowStockBooks = _adminService.GetLowStockBooks();
            RecentStockRefills = _adminService.GetRecentStockRefills();
            TopCustomers = _adminService.GetTopCustomers();
            MonthlyProfits = _adminService.GetMonthlyProfit();
            foreach (var book in LowStockBooks)
            {

                publisherIds.Add(book.PublisherID);
                StockUpdates.Add(book.BookID, 0);
            }
        }

        public IActionResult OnPostUpdateStock()
        {
            Dictionary<int, int[]> updatedStocks = new Dictionary<int, int[]>();

            foreach (var update in StockUpdates)
            {
                int bookId = update.Key;
                int newQuantity = update.Value;

                if (newQuantity < 0)
                {
                    ModelState.AddModelError("", "Stock quantity cannot be negative.");
                    return Page();
                }
                else
                {
                    int[] quantityArray = new int[] { bookId, newQuantity };
                    updatedStocks.Add(bookId, quantityArray);
                }
            }

            _publisherService.PlacePublisherOrder(updatedStocks);

            return RedirectToPage();
        }

    }
}
