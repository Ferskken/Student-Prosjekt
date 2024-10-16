export const bookServices = {
  getAllBooks: async () => {
    console.log('getAllBooks Called');
    const response = await fetch('http://localhost:5189/book');
    if (response.ok) {
      return await response.json();
    }
    return [];
  },

  addBook: async (book) => {
    console.log('addBook Called', book);
    const response = await fetch('http://localhost:5189/book/create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(book),
    });

    if (!response.ok) {
      throw new Error('Failed to add book');
    }

    return await response.json();
  },


  getBookById: async (id: number) => {
    console.log('getBookById Called');
    const response = await fetch(`http://localhost:5189/book/search/id/${id}`);

    if (response.ok) {
      const data = await response.json();
      console.log("Response Data:", data);

      if (Array.isArray(data) && data.length > 0) {

        return data[0];
      } else {
        throw new Error('Book not found');
      }
    }

    throw new Error('Book not found');
  },


  getBookByTitle: async (title: string) => {
    console.log('getBookByTitle Called');
    const response = await fetch(`http://localhost:5189/book/search/title/${title}`);
    if (response.ok) {
      const data = await response.json();
      console.log("Response Data:", data);

      if (Array.isArray(data) && data.length > 0) {

        return data;
      } else {
        throw new Error('Book not found');
      }
    }

    throw new Error('Book not found');
  },

  getBookByISBN: async (isbn: string) => {
    console.log('getBookByISNB Called');
    const response = await fetch(`http://localhost:5189/book/search/isbn/${isbn}`);
    if (response.ok) {
      const data = await response.json();
      console.log("Response Data:", data);

      if (Array.isArray(data) && data.length > 0) {

        return data;
      } else {
        throw new Error('Book not found');
      }
    }

    throw new Error('Book not found');
  },

  getBookByPublisher: async (publisher: string) => {
    console.log('getBookByPublisher Called');
    const response = await fetch(`http://localhost:5189/book/search/publisher/${publisher}`);
    if (response.ok) {
      const data = await response.json();
      console.log("Response Data:", data);

      if (Array.isArray(data) && data.length > 0) {

        return data;
      } else {
        throw new Error('Book not found');
      }
    }

    throw new Error('Book not found');
  },

  getBookByAuthor: async (author: string) => {
    console.log('getBookByAuthor Called');
    const response = await fetch(`http://localhost:5189/book/search/author/${author}`);
    if (response.ok) {
      const data = await response.json();
      console.log('Response Data:', data);

      if (Array.isArray(data) && data.length > 0) {

        return data;
      } else {
        throw new Error('Book not found');
      }
    }

    throw new Error('Book not found');
  },

  deleteBookbyId: async (id: number) => {
    console.log("deleteBookbyId Called");
    const response = await fetch(`http://localhost:5189/book/delete/${id}`,{method: 'DELETE'});
    if (response.ok) {
      console.log(`Book with ID ${id} deleted successfully`, response);
      return {
        success: true,
      };
    } else {
      const errorMessage = await response.text();
      console.error('Failed to delete nook with ID ${id}');
      throw new Error('Book not found');
    }

  },

  updateBook: async (book: BookModel) => {
    console.log("updateBook Called");
    const response = await fetch(`http://localhost:5189/book/update/${book.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(book),
    });

    if (response.ok) {
      const updatedBook = await response.json();
      console.log(`Book with ID ${book.id} updated successfully`, updatedBook);
      return updatedBook;
    } else {
      console.error(`Failed to update book with ID ${book.id}`);
      throw new Error('Update failed');
    }
  },



};
