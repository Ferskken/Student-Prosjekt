using System;
using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace
{
    public class BookModel
    {
	/// <summary>
    /// The unique identifier for the book.
    /// </summary>
	[Key] 
	public int Id { get; set; } 
	
	/// <summary>
    /// The title of the book.
    /// </summary>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// The author of the book.
    /// </summary>
    [Required]
    public string Author { get; set; }

    /// <summary>
    /// The publisher of the book.
    /// </summary>
    [Required]
    public string Publisher { get; set; }

    /// <summary>
    /// The date when the book was published.
    /// </summary>
    [Required]
    public DateTime PublicationDate { get; set; }

    /// <summary>
    /// The ISBN number of the book.
    /// </summary>
    [Required]
    public string ISBN { get; set; }

    /// <summary>
    /// The language the book is written in.
    /// </summary>
    [Required]
    public string Language { get; set; }


     /*   public BookModel(int id, string title, string author, string publisher, DateTime publicationDate, string isbn, string edition, string language)
        {
            this.Id = id;
            this.Title = title;
            this.Author = author;
            this.Publisher = publisher;
            this.PublicationDate = publicationDate;
            this.ISBN = isbn;
            this.Edition = edition;
            this.Language = language;
        } */
    }
}