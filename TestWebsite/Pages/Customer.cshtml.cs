using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebsite.Pages
{
	public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        public Customer ?Customer { get; private set; }
        public List<Order> ?Orders { get; private set; }

        public CustomerModel(ICustomerService customerService, IOrderService orderService, IBookService bookService)
        {
            _customerService = customerService;
            _orderService = orderService;
            _bookService = bookService;
        }

        public void OnGet()
        {
            var name = HttpContext.Session.GetString("CustomerName");
            var email = HttpContext.Session.GetString("CustomerEmail");
            var customerID = HttpContext.Session.GetInt32("CustomerID");

            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && customerID.HasValue)
            {
                Customer = new Customer((int)customerID, name, email);
                Orders = _orderService.GetOrdersForCustomer(customerID.Value);

            }
            else
            {
                RedirectToPage("/Login");
            }
            foreach (var order in Orders)
            {
                //order.OrderLines = _bookService.GetBooksByOrderNumber(order.OrderNumber);
            }
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("CustomerName");
            HttpContext.Session.Remove("CustomerEmail");

            return RedirectToPage("/Index");
        }
    }
}
