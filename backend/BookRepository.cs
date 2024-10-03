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

	  if (string.IsNullOrWhiteSpace(title))
    {
        var allBooksQuery = "SELECT * FROM Books";
        var allBooks = await connection.QueryAsync<BookModel>(allBooksQuery);
        return allBooks.ToList();
    }    

    var query = "SELECT * FROM Books WHERE title ILIKE @Title";
    var books = await connection.QueryAsync<BookModel>(query, new { Title = "%" + title + "%" });
    return books.ToList();
  } 
  
  public async Task<List<BookModel>> GetBookById(int id)
  {
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();
    
    
    var query = "SELECT * FROM Books WHERE id = @Id";
    var books = await connection.QueryAsync<BookModel>(query, new { Id = id });
    return books.ToList();
  }
   
  public async Task<List<BookModel>> GetBooksByAuthor(string author)
  {
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();
    
    var query = "SELECT * FROM Books WHERE author ILIKE @Author";
    var books = await connection.QueryAsync<BookModel>(query, new { Author = "%" + author + "%" });
    return books.ToList();
  } 

  public async Task<List<BookModel>> GetBooksByPublisher(string publisher)
  {
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();
    
    var query = "SELECT * FROM Books WHERE publisher ILIKE @Publisher";
    var books = await connection.QueryAsync<BookModel>(query, new { Publisher = "%" + publisher + "%" });
    return books.ToList();
  } 
  


	 public async Task<List<BookModel>> GetBooksByIsbn(string isbn)
  {
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();
    
    var query = "SELECT * FROM Books WHERE isbn = @Isbn";

    var books = await connection.QueryAsync<BookModel>(query, new { Isbn =  isbn   });
    return books.ToList();
  } 

	  public async Task<List<BookModel>> GetBooksByLanguage(string language)
  {
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();
    
    var query = "SELECT * FROM Books WHERE language ILIKE @Language";
    var books = await connection.QueryAsync<BookModel>(query, new { Language = language  });
    return books.ToList();
  } 

 public async Task<BookModel> CreateBook(BookModel newBook)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync();

          var query = @"
            INSERT INTO Books (title, author, publisher, publication_date, isbn, edition, language)
            VALUES (@Title, @Author, @Publisher, @PublicationDate, @ISBN, @Edition, @Language) 
            RETURNING *";
          
            var createdBook = await connection.QuerySingleOrDefaultAsync<BookModel>(query, new
            {
              title = newBook.Title,
              author = newBook.Author,
              publisher = newBook.Publisher,
              publicationDate = newBook.PublicationDate,
              isbn = newBook.ISBN,
              edition = newBook.Edition ?? string.Empty,  // Optional field
              language = newBook.Language
            });

            return createdBook;
        }

public async Task<BookModel> UpdateBook(int id, BookModel updatedBook)
{
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();
    
    var existingBook = await connection.QuerySingleOrDefaultAsync<BookModel>(
        "SELECT * FROM Books WHERE Id = @Id", new { Id = id });

    if (existingBook == null)
    {
        return null;
    }
    
    var query = @"
        UPDATE Books
        SET title = @Title, 
            author = @Author, 
            publisher = @Publisher, 
            publication_date = @PublicationDate, 
            isbn = @ISBN, 
            edition = @Edition, 
            language = @Language
        WHERE Id = @Id
        RETURNING *";
    
    var updatedBookRecord = await connection.QuerySingleOrDefaultAsync<BookModel>(query, new
    {
        Id = id,
        Title = updatedBook.Title,
        Author = updatedBook.Author,
        Publisher = updatedBook.Publisher,
        PublicationDate = updatedBook.PublicationDate,
        ISBN = updatedBook.ISBN,
        Edition = updatedBook.Edition ?? string.Empty, // Optional field
        Language = updatedBook.Language
    });

    return updatedBookRecord;
}
public async Task<bool> DeleteBook(int id)
{
    using var connection = new NpgsqlConnection(ConnectionString);
    await connection.OpenAsync();

    var query = "DELETE FROM Books WHERE id = @Id";

    var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

    return rowsAffected > 0; // If the book was deleted, return true
}



}  

