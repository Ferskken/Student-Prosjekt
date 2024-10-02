CREATE TABLE books (
    id  SERIAL PRIMARY KEY,
    title TEXT,
    author TEXT,
    publisher TEXT,
    publication_date DATE,
    isbn TEXT,
    edition TEXT,
    language TEXT
                   );
    
    INSERT INTO books (title,author,publisher,publication_date,isbn,edition,language)
    VALUES 
    
        ('The Lord of the Rings','J.R.R. Tolkien','Houghton Mifflin','1954-07-29','9780261103689','2021','English'),
        ('Ringenes Herre','J.R.R. Tolkien','Houghton Mifflin','1954-07-29','9780261103689','2021','Norsk'),
        ('The Lord of the Rings: The Two Towers','J.R.R. Tolkien','Houghton Mifflin','1954-11-11','9780261103610','2021','English'),
        ('Ringenes Herre: To tårn','J.R.R. Tolkien','Houghton Mifflin','1954-11-11','9780261103610','2022','Norsk'),
        ('The Lord of the Rings: Return of the King','J.R.R. Tolkien','Houghton Mifflin','1955-10-20','9780261103666','2022','English'),
        ('Ringenes Herre: Atter en konge','J.R.R. Tolkien','Houghton Mifflin','1955-10-20','9780261103666','2021','Norsk'),
        ('The Bible','King James','HHarper & Row','1611-05-16','9780261103666','1995','English'),
        ('1984', 'George Orwell', 'Secker & Warburg', '1949-06-08', '9780451524935', '2009', 'English'),
        ('Nineteen Eighty-Four', 'George Orwell', 'Gyldendal', '1949-06-08', '9780451524935', '2010', 'Norsk'),
        ('Brave New World', 'Aldous Huxley', 'Chatto & Windus', '1932-09-01', '9780060850524', '2006', 'English'),
        ('Verden i 2020', 'Aldous Huxley', 'Gyldendal Norsk Forlag', '1932-09-01', '9780060850524', '2010', 'Norsk'),
        ('Pride and Prejudice', 'Jane Austen', 'T. Egerton', '1813-01-28', '9780141040349', '2009', 'English'),
        ('Stolthet og fordom', 'Jane Austen', 'Gyldendal', '1813-01-28', '9780141040349', '2010', 'Norsk'),
        ('Moby Dick', 'Herman Melville', 'Harper & Brothers', '1851-10-18', '9780142437247', '2002', 'English'),
        ('Moby Dick: Hvalen', 'Herman Melville', 'Aschehoug', '1851-10-18', '9780142437247', '2003', 'Norsk'),
        ('War and Peace', 'Leo Tolstoy', 'The Russian Messenger', '1869-01-09', '9780199232765', '2007', 'English'),
        ('Krig og fred', 'Leo Tolstoy', 'Gyldendal Norsk Forlag', '1869-01-09', '9780199232765', '2011', 'Norsk'),
        ('Crime and Punishment', 'Fyodor Dostoevsky', 'The Russian Messenger', '1866-01-01', '9780143058144', '2002', 'English'),
        ('Forbrytelse og straff', 'Fyodor Dostoevsky', 'Gyldendal', '1866-01-01', '9780143058144', '2003', 'Norsk'),
        ('The Great Gatsby', 'F. Scott Fitzgerald', 'Charles Scribner Sons', '1925-04-10', '9780743273565', '2004', 'English'),
        ('Den store Gatsby', 'F. Scott Fitzgerald', 'Gyldendal', '1925-04-10', '9780743273565', '2005', 'Norsk'),
        ('To Kill a Mockingbird', 'Harper Lee', 'J.B. Lippincott & Co.', '1960-07-11', '9780060935467', '2006', 'English');
