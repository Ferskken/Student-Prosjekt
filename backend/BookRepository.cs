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
    
    var query = "SELECT * FROM Books WHERE Title ILIKE @Title";
    var books = await connection.QueryAsync<BookModel>(query, new { Title = "%" + title + "%" });
    return books.AsList();
  } 
 
}  
