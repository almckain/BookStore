using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebsite.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBookService _bookService;
    private readonly IPublisherService _publisherService;

    public List<Book> Books { get; private set; }
    public List<Book> BestSellers { get; private set; }
    public List<Genre> Genres { get; private set; }
    public List<Publisher> Publishers { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, IBookService bookService, IPublisherService publisherService)
    {
        _logger = logger;
        _bookService = bookService;
        _publisherService = publisherService;
    }

    public void OnGet()
    {
        Books = _bookService.GetBooks();
        BestSellers = _bookService.GetBestSellers();
        Genres = _bookService.GetAllGenres();
        Publishers = _publisherService.GetAllPublishers();
    }

    public void OnPost(List<int> selectedGenres, int? selectedPublisher)
    {
        Genres = _bookService.GetAllGenres();
        BestSellers = _bookService.GetBestSellers();
        Publishers = _publisherService.GetAllPublishers();

        List<Book> genreFilteredBooks = new List<Book>();
        List<Book> publisherFilteredBooks = new List<Book>();

        // Fetch books by selected genres
        if (selectedGenres != null && selectedGenres.Count > 0)
        {
            genreFilteredBooks = _bookService.GetBooksByGenres(selectedGenres);
        }
        else
        {
            genreFilteredBooks = _bookService.GetBooks();
        }

        // Fetch books by selected publisher
        if (selectedPublisher.HasValue && selectedPublisher.Value > 0)
        {
            publisherFilteredBooks = _bookService.GetBooksByPublishers(selectedPublisher.Value);
        }
        else
        {
            publisherFilteredBooks = _bookService.GetBooks();
        }

        // Intersect the two lists to get books that match both filters
        Books = genreFilteredBooks.Intersect(publisherFilteredBooks, new BookComparer()).ToList();
    }


}

