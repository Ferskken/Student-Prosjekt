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
    

	/// <summary>
	/// Retrieves a book by its title.
	/// </summary>
	/// <param name="title">The title of the book.</param>
	/// <returns>A list of books matching the specified title.</returns>
	/// <response code="200">Returns the list of books that match the title.</response>
	/// <response code="404">If no books are found with the given title.</response>
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
	
	/// <summary>
	/// Retrieves a book by its author.
	/// </summary>
	/// <param name="author">The author of the book.</param>
	/// <returns>A list of books matching the specified author.</returns>
	/// <response code="200">Returns the list of books that match the author.</response>
	/// <response code="404">If no books are found with the given author.</response>
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
	

	/// <summary>
	/// Retrieves a book by its publisher.
	/// </summary>
	/// <param name="publisher">The publisher of the book.</param>
	/// <returns>A list of books matching the specified publisher.</returns>
	/// <response code="200">Returns the list of books that match the publisher.</response>
	/// <response code="404">If no books are found with the given publisher.</response>
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

	
	/// <summary>
	/// Retrieves a book by its isbn.
	/// </summary>
	/// <param name="isbn">The isbn of the book.</param>
	/// <returns>A list of books matching the specified isbn.</returns>
	/// <response code="200">Returns the list of books that match the isbn.</response>
	/// <response code="404">If no books are found with the given isbn.</response>
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
	
	/// <summary>
	/// Retrieves a book by its language.
	/// </summary>
	/// <param name="isbn">The language of the book.</param>
	/// <returns>A list of books matching the specified language.</returns>
	/// <response code="200">Returns the list of books that match the language.</response>
	/// <response code="404">If no books are found with the given language.</response>
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
	
	/// <summary>
	/// Creates a new book in the database.
	/// </summary>
	/// <remarks>
	/// Sample request:
	/// 
	///     POST /book/create
	///     {
	///         "title": "New Book Title",
	///         "author": "John Doe",
	///         "isbn": "9780261103666",
	///         "publisher": "Some Publisher",
	///         "publicationDate": "2024-10-10",
	///         "edition": "1st",
	///         "language": "English"
	///     }
	///     
	/// The publication date should be in the format 'yyyy-MM-dd'.
	/// </remarks>
	/// <param name="newBook">The book data to create, provided in the request body.</param>
	/// <returns>The created book with its generated ID.</returns>
	/// <response code="201">The book was created successfully, and the created book is returned.</response>
	/// <response code="400">Invalid book data provided. Check the request body.</response>
	/// <response code="500">An error occurred while creating the book.</response>
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



