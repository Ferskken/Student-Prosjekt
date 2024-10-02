using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace;

[ApiController]
[Route("book/")]
public class BookController : ControllerBase
{
    
private readonly BookRepository _bookRepository;

public BookController(BookRepository bookRepository)
{
    _bookRepository = bookRepository;
}


[HttpGet("{title}")]

    [HttpGet]
    public List<BookModel> GetAllBooks()
    {
        return _bookRepository.GetAllBooks();
    }
    
    public async Task<ActionResult<List<BookModel>>> GetBookTitle(string title)
    {  
        // Search for books where title contains the given string
        var books = await _bookRepository.GetBooksByTitle(title);
   
        // Handle case when no books are found
        if (books == null || books.Count == 0)
        {
            return NotFound(new { Message = "No books found with that title" });
        }
   
        // Return the list of books if found
        return Ok(books); 
    }
}



