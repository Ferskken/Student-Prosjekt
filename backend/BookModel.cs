using System;
using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace
{
    public class BookModel
    {
        [Key] 
        public int Id { get; set; } 

        [Required] 
        public string Title { get; set; } 

        [Required] 
        public string Author { get; set; } 

        [Required] 
        public string Publisher { get; set; } 

        [Required] 
        public DateTime PublicationDate { get; set; } 

        [Required] 
        public string ISBN { get; set; } 

        public string Edition { get; set; } 

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