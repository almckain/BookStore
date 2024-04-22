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
            int customerID = Convert.ToInt32(HttpContext.Session.GetInt32("CustomerID"));
            Orders = _customerService.ReturnOrdersByCustomers(customerID);

            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
            {
                Customer = new Customer((int)customerID, name, email);
                Orders = _customerService.ReturnOrdersByCustomers(customerID);

            }
            else
            {
                RedirectToPage("/Login");
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
