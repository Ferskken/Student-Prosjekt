// src/services/bookServices.ts
export const bookServices = {
  getAllBooks: async () => {
    console.log("getAllBooks Called");
    const response = await fetch("http://localhost:5189/book");
    if (response.ok) {
      return await response.json();
    }
    return [];
  },

  addBook: async (book) => {
    console.log("addBook Called", book);
    const response = await fetch("http://localhost:5189/book/create", {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(book), // Convert the book object to JSON
    });

    if (!response.ok) {
      throw new Error('Failed to add book');
    }

    return await response.json(); // Return the newly created book
  },


  getBookById: async (id: number) => {
    console.log("getBookById Called");
    const response = await fetch(`http://localhost:5189/book/search/id/${id}`);

    if (response.ok) {
      const data = await response.json(); // Parse the JSON
      console.log("Response Data:", data); // Log the full array

      if (Array.isArray(data) && data.length > 0) {
        // If data is an array, return the first element (the book)
        return data[0];
      } else {
        throw new Error('Book not found');
      }
    }

    throw new Error('Book not found');
  },


  getBookByTitle: async (title: string) => {
    console.log("getBookByTitle Called");
    const response = await fetch(`http://localhost:5189/book/search/title/${title}`);
    if (response.ok) {
      return await response.json();
    }
    throw new Error('Book not found');
  },

  getBookByISBN: async (isbn: string) => {
    console.log("getBookByISNB Called");
    const response = await fetch(`http://localhost:5189/book/search/isbn/${isbn}`);
    if (response.ok) {
      return await response.json();
    }
    throw new Error('Book not found');
  },

  getBookByPublisher: async (publisher: string) => {
    console.log("getBookByPublisher Called");
    const response = await fetch(`http://localhost:5189/book/search/publisher/${publisher}`);
    if (response.ok) {
      return await response.json();
    }
    throw new Error('Book not found');
  },

  getBookByAuthor: async (author: string) => {
    console.log("getBookByAuthor Called");
    const response = await fetch(`http://localhost:5189/book/search/author/${author}`);
    if (response.ok) {
      return await response.json();
    }
    throw new Error('Book not found');
  },
};
