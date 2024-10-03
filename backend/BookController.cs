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



	/// <summary>
	/// Retrieves all books in the database.
	/// </summary>
	/// <returns>A list of all available books.</returns>
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
    
    /// <summary>
	/// Retrieves a book by its ID.
	/// </summary>
	/// <param name="id">The ID of the book.</param>
	/// <returns>A single book with the matching ID.</returns>
	/// <response code="200">Returns the book with the specified ID.</response>
	/// <response code="404">If the book is not found.</response>
    [HttpGet("search/id/{id:int}")]
	[ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)] // Success case
	[ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)] // Not found
	[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal error
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


	[HttpGet("search/isbn/{isbn}")]
    public async Task<ActionResult<List<BookModel>>> GetBookIsbn(string isbn)
    {  
      
        var books = await _bookRepository.GetBooksByIsbn(isbn);
   
        if (books == null || books.Count == 0)
        {
            return NotFound(new { Message = "No books found with that isbn" });
        }
   
        return Ok(books); 
    }

	[HttpGet("search/language/{language}")]
    public async Task<ActionResult<List<BookModel>>> GetBookLanguage(string language)
    {  
      
        var books = await _bookRepository.GetBooksByLanguage(language);
   
        if (books == null || books.Count == 0)
        {
            return NotFound(new { Message = "No books found with that language" });
        }
   
        return Ok(books); 
    }

	[HttpPost("create")]
	[ProducesResponseType(typeof(BookModel), StatusCodes.Status201Created)] // Book created successfully
	[ProducesResponseType(StatusCodes.Status400BadRequest)] // Invalid data
	[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Error during creation
	public async Task<ActionResult<BookModel>> CreateBook([FromBody] BookModel newBook) 
	{
		if (newBook == null)
           {
               return BadRequest(new { Message = "Invalid book data" });
           }
           var createdBook = await _bookRepository.CreateBook(newBook);

           return createdBook == null ? StatusCode(500, new { Message = "An error occurred while creating the book" }) : CreatedAtAction(nameof(GetBookId), new { id = createdBook.Id }, createdBook);
    }

[HttpPut("update/{id}")]
[ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)] // Book updated successfully
[ProducesResponseType(StatusCodes.Status404NotFound)] // Book not found
[ProducesResponseType(StatusCodes.Status400BadRequest)] // Invalid book data
[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal server error
public async Task<ActionResult<BookModel>> UpdateBook(int id, [FromBody] BookModel updatedBook)
{
    if (updatedBook == null)
    {
        return BadRequest("Invalid book data.");
    }
   
    var existingBook = await _bookRepository.UpdateBook(id, updatedBook);

    if (existingBook == null)
    {
        return NotFound(new { Message = "Book not found." });
    }

    return Ok(existingBook);
}

/// <summary>
/// Deletes an existingBook by ID
/// </summary>
[HttpDelete("delete/{id}")]
[ProducesResponseType(StatusCodes.Status204NoContent)] // Successfully deleted
[ProducesResponseType(StatusCodes.Status404NotFound)] // Book not found
[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Error during deletion
public async Task<ActionResult> DeleteBook(int id)
{
    try
    {
        var existingBook = await _bookRepository.GetBookById(id);

        if (existingBook == null || !existingBook.Any())  // Check if no book was returned
        {
            return NotFound(new { Message = "Book not found" }); //404
        }

        var wasDeleted = await _bookRepository.DeleteBook(id);

        if (wasDeleted)
        {
            return NoContent(); //204
        }
        else
        {
            return NotFound(new { Message = "Book not found or could not be deleted" }); //404
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { Message = "An error occurred while deleting the book" });
    }
}

}



