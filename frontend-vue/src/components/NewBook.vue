<template>
  <div>
    <h2>Add a New Book</h2> <!-- Header for the book addition form -->
    <form @submit.prevent="handleSubmit"> <!-- Form to add a new book, prevent default form submission -->
      <div>
        <label for="title">Title:</label> <!-- Label for title input -->
        <input type="text" v-model="newBook.title" required /> <!-- Input for book title, required -->
      </div>
      <div>
        <label for="author">Author:</label> <!-- Label for author input -->
        <input type="text" v-model="newBook.author" required /> <!-- Input for book author, required -->
      </div>
      <div>
        <label for="publisher">Publisher:</label> <!-- Label for publisher input -->
        <input type="text" v-model="newBook.publisher" required /> <!-- Input for book publisher, required -->
      </div>
      <div>
        <label for="publicationDate">Publication Date:</label> <!-- Label for publication date input -->
        <input type="date" v-model="newBook.publicationDate" required /> <!-- Input for publication date, required -->
      </div>
      <div>
        <label for="isbn">ISBN:</label> <!-- Label for ISBN input -->
        <input type="text" v-model="newBook.isbn" required /> <!-- Input for book ISBN, required -->
      </div>
      <div>
        <label for="edition">Edition:</label> <!-- Label for edition input -->
        <input type="text" v-model="newBook.edition" /> <!-- Input for book edition, optional -->
      </div>
      <div>
        <label for="language">Language:</label> <!-- Label for language input -->
        <input type="text" v-model="newBook.language" required /> <!-- Input for book language, required -->
      </div>
      <button type="submit">Add Book</button> <!-- Button to submit the form -->
    </form>
    <div v-if="error" class="error">{{ error }}</div> <!-- Display error message if there's an error -->
    <button @click="addTestBook">Add Test Book</button> <!-- Button to add a test book for testing purposes -->
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'; // Importing Vue reactive references and computed properties
import { useBooksStore } from '@/stores/books'; // Importing the book store for state management
import { bookServices } from '@/services/bookServices'; // Importing book services for API calls
import type { BookModel } from '@/models/bookModel'; // Importing the BookModel type for TypeScript

// Reactive reference for the new book
const newBook = ref<BookModel>({
  id: 0,  // Placeholder ID for the new book; to be managed by the backend
  title: '', // Title of the book
  author: '', // Author of the book
  publisher: '', // Publisher of the book
  publicationDate: new Date().toISOString().split('T')[0], // Default to today's date
  isbn: '', // ISBN of the book
  edition: '', // Edition of the book
  language: '', // Language of the book
});

// Function to add a test book for demonstration purposes
const addTestBook = () => {
  newBook.value = {
    id: 0, // ID will be generated on the backend
    title: 'Test Book Title', // Sample title
    author: 'Test Author', // Sample author
    publisher: 'Test Publisher', // Sample publisher
    publicationDate: '2024-01-01', // Example publication date
    isbn: '123456789', // Example ISBN
    edition: '1st', // Example edition
    language: 'English', // Example language
  };
  handleSubmit(); // Submit the form with the test book data
}

// Reactive reference for storing error messages
const error = ref<string | null>(null);

// Accessing the book store
const store = useBooksStore();

// Function to handle form submission
const handleSubmit = async () => {
  // Check if required fields are filled
  if (!newBook.value.title || !newBook.value.author || !newBook.value.publisher || !newBook.value.publicationDate || !newBook.value.isbn || !newBook.value.language) {
    error.value = 'Please fill in all required fields.'; // Set error message if fields are missing
    return; // Exit the function
  }

  try {
    // Call the service to add the new book
    const addedBook = await bookServices.addBook(newBook.value);
    store.setBooks([...store.getBooks, addedBook]); // Update the store with the newly added book

    // Reset the form fields to their initial state
    newBook.value = {
      id: 0, // Reset ID for new entry
      title: '',
      author: '',
      publisher: '',
      publicationDate: new Date().toISOString().split('T')[0], // Reset to today's date
      isbn: '',
      edition: '',
      language: '',
    };
    error.value = null; // Clear any existing error messages
  } catch (err) {
    error.value = 'Failed to add the book. Please try again.'; // Set error message on failure
  }
};
</script>

<style scoped>
.error {
  color: red; /* Red color for error messages */
  margin-top: 10px; /* Space above error messages */
}
</style>
