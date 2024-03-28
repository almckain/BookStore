using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebsite.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBookService _bookService;

    public List<Book> Books { get; private set; }
    public List<Book> BestSellers { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, IBookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    public void OnGet()
    {
        Books = _bookService.GetBooks();
        BestSellers = _bookService.GetBestSellers();
    }
}

