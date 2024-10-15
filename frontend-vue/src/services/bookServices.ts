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

  // New method to add a book
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
};
