// Book services module for interacting with the backend API related to book management
export const bookServices = {
  // Fetches all books from the API
  getAllBooks: async () => {
    console.log('getAllBooks Called'); // Log the function call for debugging
    const response = await fetch('http://localhost:5189/book'); // Fetch all books from the API

    if (response.ok) {
      return await response.json(); // If the response is OK, return the JSON data
    }
    return []; // Return an empty array if the response is not OK
  },

  // Adds a new book to the database
  addBook: async (book) => {
    console.log('addBook Called', book); // Log the function call and the book being added
    const response = await fetch('http://localhost:5189/book/create', {
      method: 'POST', // Set the request method to POST
      headers: {
        'Content-Type': 'application/json', // Indicate that the request body is JSON
      },
      body: JSON.stringify(book), // Convert the book object to JSON string
    });

    if (!response.ok) {
      throw new Error('Failed to add book'); // Throw an error if the response is not OK
    }

    return await response.json(); // Return the JSON response of the newly added book
  },

  // Fetches a book by its ID
  getBookById: async (id: number) => {
    console.log('getBookById Called'); // Log the function call
    const response = await fetch(`http://localhost:5189/book/search/id/${id}`); // Fetch the book by ID

    if (response.ok) {
      const data = await response.json(); // Parse the JSON response
      console.log("Response Data:", data); // Log the response data

      // If the response is an array and has results, return the first book
      if (Array.isArray(data) && data.length > 0) {
        return data[0];
      } else {
        throw new Error('Book not found'); // Throw an error if no book is found
      }
    }

    throw new Error('Book not found'); // Throw an error if the response is not OK
  },

  // Fetches books by their title
  getBookByTitle: async (title: string) => {
    console.log('getBookByTitle Called'); // Log the function call
    const response = await fetch(`http://localhost:5189/book/search/title/${title}`); // Fetch the book by title

    if (response.ok) {
      const data = await response.json(); // Parse the JSON response
      console.log("Response Data:", data); // Log the response data

      // If the response is an array and has results, return the results
      if (Array.isArray(data) && data.length > 0) {
        return data;
      } else {
        throw new Error('Book not found'); // Throw an error if no book is found
      }
    }

    throw new Error('Book not found'); // Throw an error if the response is not OK
  },

  // Fetches books by their ISBN
  getBookByISBN: async (isbn: string) => {
    console.log('getBookByISBN Called'); // Log the function call
    const response = await fetch(`http://localhost:5189/book/search/isbn/${isbn}`); // Fetch the book by ISBN

    if (response.ok) {
      const data = await response.json(); // Parse the JSON response
      console.log("Response Data:", data); // Log the response data

      // If the response is an array and has results, return the results
      if (Array.isArray(data) && data.length > 0) {
        return data;
      } else {
        throw new Error('Book not found'); // Throw an error if no book is found
      }
    }

    throw new Error('Book not found'); // Throw an error if the response is not OK
  },

  // Fetches books by their publisher
  getBookByPublisher: async (publisher: string) => {
    console.log('getBookByPublisher Called'); // Log the function call
    const response = await fetch(`http://localhost:5189/book/search/publisher/${publisher}`); // Fetch the book by publisher

    if (response.ok) {
      const data = await response.json(); // Parse the JSON response
      console.log("Response Data:", data); // Log the response data

      // If the response is an array and has results, return the results
      if (Array.isArray(data) && data.length > 0) {
        return data;
      } else {
        throw new Error('Book not found'); // Throw an error if no book is found
      }
    }

    throw new Error('Book not found'); // Throw an error if the response is not OK
  },

  // Fetches books by their author
  getBookByAuthor: async (author: string) => {
    console.log('getBookByAuthor Called'); // Log the function call
    const response = await fetch(`http://localhost:5189/book/search/author/${author}`); // Fetch the book by author

    if (response.ok) {
      const data = await response.json(); // Parse the JSON response
      console.log('Response Data:', data); // Log the response data

      // If the response is an array and has results, return the results
      if (Array.isArray(data) && data.length > 0) {
        return data;
      } else {
        throw new Error('Book not found'); // Throw an error if no book is found
      }
    }

    throw new Error('Book not found'); // Throw an error if the response is not OK
  },

  // Deletes a book by its ID
  deleteBookbyId: async (id: number) => {
    console.log("deleteBookbyId Called"); // Log the function call
    const response = await fetch(`http://localhost:5189/book/delete/${id}`, { method: 'DELETE' }); // Send a DELETE request to the API

    if (response.ok) {
      console.log(`Book with ID ${id} deleted successfully`, response); // Log successful deletion
      return {
        success: true, // Return success status
      };
    } else {
      const errorMessage = await response.text(); // Get error message from response
      console.error(`Failed to delete book with ID ${id}`); // Log failure to delete
      throw new Error('Book not found'); // Throw an error if the response is not OK
    }
  },

  // Updates an existing book
  updateBook: async (book: BookModel) => {
    console.log("updateBook Called"); // Log the function call
    const response = await fetch(`http://localhost:5189/book/update/${book.id}`, {
      method: 'PUT', // Set the request method to PUT for updates
      headers: {
        'Content-Type': 'application/json', // Indicate that the request body is JSON
      },
      body: JSON.stringify(book), // Convert the book object to JSON string
    });

    if (response.ok) {
      const updatedBook = await response.json(); // Parse the JSON response of the updated book
      console.log(`Book with ID ${book.id} updated successfully`, updatedBook); // Log successful update
      return updatedBook; // Return the updated book
    } else {
      console.error(`Failed to update book with ID ${book.id}`); // Log failure to update
      throw new Error('Update failed'); // Throw an error if the response is not OK
    }
  },
};
