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




    [HttpGet]
    public List<BookModel> GetAllBooks()
    {
        return _bookRepository.GetAllBooks();
    }
    
    [HttpGet("search/title/{title}")]
public async Task<ActionResult<List<BookModel>>> GetBookTitle(string title)
{
    // Call the repository to fetch books by title
    var books = await _bookRepository.GetBooksByTitle(title);

    // If no books are found, return 404
    if (books == null || books.Count == 0)
    {
        return NotFound(new { Message = "No books found with that title" });
    }

    // Return the list of books if found
    return Ok(books);
}
    
    
    [HttpGet("search/id/{id:int}")]
    public async Task<ActionResult<BookModel>> GetBookId(int id)
    {
        var book = await _bookRepository.GetBookById(id);

        if (book == null)
        {
            return NotFound(new { Message = "No book found with that ID" });
        }

        return Ok(book);
    }

   [HttpGet("search/author/{author}")]
    public async Task<ActionResult<List<BookModel>>> GetBookAuthor(string author)
    {  
      
        var books = await _bookRepository.GetBooksByAuthor(author);
   
        if (books == null || books.Count == 0)
        {
            return NotFound(new { Message = "No books found with that author" });
        }
   
        return Ok(books); 
    }

   [HttpGet("search/publisher/{publisher}")]
    public async Task<ActionResult<List<BookModel>>> GetBookPublisher(string publisher)
    {  
      
        var books = await _bookRepository.GetBooksByPublisher(publisher);
   
        if (books == null || books.Count == 0)
        {
            return NotFound(new { Message = "No books found with that publisher" });
        }
   
        return Ok(books); 
    }

}



