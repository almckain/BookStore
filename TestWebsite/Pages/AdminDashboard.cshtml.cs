using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebsite.Pages
{
	public class AdminDashboardModel : PageModel
    {
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }

        public void OnGet()
        {
            CustomerName = HttpContext.Session.GetString("CustomerName") ?? "Not logged in";
            CustomerEmail = HttpContext.Session.GetString("CustomerEmail") ?? "No email";
        }
    }
}
