using Dapper;
using Npgsql;


namespace DefaultNamespace;

public class BookRepository
{
private string ConnectionString { get; }
  public BookRepository(IConfiguration Configuration)
  {
    ConnectionString = Configuration.GetValue<string>("ConnectionString");
  }
  
  public List<BookModel> GetAllBooks()
  {
    string sql = "select * from Books";
    using var connection = new NpgsqlConnection(ConnectionString);
    connection.Open();
    var result = connection.Query<BookModel>(sql);
    return result.ToList();
  }

  
 public async Task<List<BookModel>> GetBooksByTitle(string title)
  {
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();
    
    var query = "SELECT * FROM Books WHERE title ILIKE @Title";
    var books = await connection.QueryAsync<BookModel>(query, new { Title = "%" + title + "%" });
    return books.ToList();
  } 
 
  
  public async Task<List<BookModel>> GetBookById(int id)
  {
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();
    
    // Use "=" for exact integer matching instead of "LIKE"
    var query = "SELECT * FROM Books WHERE id = @Id";
    var books = await connection.QueryAsync<BookModel>(query, new { Id = id });
    return books.ToList();
  }
   
 
}  


/*
 *public async Task<ActionResult<BookModel>> GetBookTitle(string title)
       {  
           // Search for the book by title (using partial match with LIKE)
           var book = await _bookRepository.GetBookByTitleContaining(title);

           // Handle case when no book is found
           if (book == null)
           {
               return NotFound(new { Message = "No book found with that title" });
           }

           // Return the book if found
           return Ok(book); 
       }
   }
 * 
 */