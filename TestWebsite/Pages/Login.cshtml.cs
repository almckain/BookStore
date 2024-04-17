using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebsite.Pages
{
	public class LoginModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public LoginModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public string Email { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (Email.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                HttpContext.Session.SetString("CustomerName", "Admin");
                HttpContext.Session.SetString("CustomerEmail", "customerservice@bookstore.com");
                HttpContext.Session.SetInt32("CustomerID", 0); 
                return RedirectToPage("/AdminDashboard"); 
            }

            var customer = _customerService.GetCustomerByEmail(Email);
            if (customer.CustomerID != 0)
            {
                HttpContext.Session.SetString("CustomerName", customer.Name);
                HttpContext.Session.SetString("CustomerEmail", customer.Email);
                HttpContext.Session.SetInt32("CustomerID", customer.CustomerID);

                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError("", "No account found with this email.");
                return Page();
            }
        }

    }
}
