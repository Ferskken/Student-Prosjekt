<template>
  <div class="book-search">
    <h2>Search for a Book</h2> <!-- Header for the book search section -->
    <div>
      <label>Search by:</label>
      <!-- Dropdown for selecting search type (ID, Title, ISBN, Publisher, Author) -->
      <select v-model="searchType">
        <option value="id">ID</option>
        <option value="title">Title</option>
        <option value="isbn">ISBN</option>
        <option value="publisher">Publisher</option>
        <option value="author">Author</option>
      </select>
      <!-- Input field for entering the search value -->
      <input v-model="query" type="text" placeholder="Enter search value" />
      <!-- Button to initiate the search -->
      <button @click="searchBook">Search</button>
    </div>

    <!-- Loading message displayed while searching -->
    <div v-if="loading">Searching...</div>
    <!-- Display error message if an error occurs -->
    <div v-if="error" class="error">{{ error }}</div>

    <div v-if="book"> <!-- Conditionally render book details if a book is found -->
      <h3>{{ book.title }}</h3> <!-- Book title -->
      <p><strong>Author:</strong> {{ book.author }}</p> <!-- Book author -->
      <p><strong>Publisher:</strong> {{ book.publisher }}</p> <!-- Book publisher -->
      <p><strong>Publication Date:</strong> {{ new Date(book.publicationDate).toLocaleDateString() }}</p> <!-- Formatted publication date -->
      <p><strong>ISBN:</strong> {{ book.isbn }}</p> <!-- Book ISBN -->
      <p><strong>Edition:</strong> {{ book.edition }}</p> <!-- Book edition -->
      <p><strong>Language:</strong> {{ book.language }}</p> <!-- Book language -->
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'; // Importing reactive references from Vue
import { bookServices } from '@/services/bookServices'; // Importing book services for API interactions
import type { BookModel } from '@/models/bookModel'; // Importing the BookModel type

// Reactive reference for the selected search type (default is 'id')
const searchType = ref('id');
// Reactive reference for the search query input
const query = ref('');
// Reactive reference for the searched book details, initialized to null
const book = ref<BookModel | null>(null);
// Reactive reference to manage loading state
const loading = ref(false);
// Reactive reference for error messages
const error = ref<string | null>(null);

// Function to search for a book based on the selected type and query
const searchBook = async () => {
  loading.value = true; // Set loading state to true
  error.value = null; // Reset error message
  book.value = null; // Reset book details

  try {
    let result; // Variable to hold the search result
    // Determine the search type and call the appropriate service function
    if (searchType.value === 'id') {
      result = await bookServices.getBookById(Number(query.value)); // Convert query to number for ID
    } else if (searchType.value === 'title') {
      result = await bookServices.getBookByTitle(query.value);
    } else if (searchType.value === 'isbn') {
      result = await bookServices.getBookByISBN(query.value);
    } else if (searchType.value === 'publisher') {
      result = await bookServices.getBookByPublisher(query.value);
    } else if (searchType.value === 'author') {
      result = await bookServices.getBookByAuthor(query.value);
    }

    // Check if the result is an array and has multiple items
    if (Array.isArray(result) && result.length > 1) {
      // Show only the first result but indicate that there are more
      book.value = result[0]; // Set the first book as the current book
      error.value = 'Showing the first result out of multiple matches.'; // Inform user of multiple matches
    } else {
      // If the result is not an array, set the book directly
      book.value = Array.isArray(result) ? result[0] : result;
    }
  } catch (err) {
    // Catch any errors that occur during the search
    error.value = 'Book not found.'; // Set error message on failure
  } finally {
    loading.value = false; // Reset loading state regardless of success or failure
  }
};
</script>

<style scoped>
.book-search {
  margin: 20px; /* Add margin around the book search section */
}
input {
  margin: 10px; /* Add margin around input fields */
}
.error {
  color: red; /* Style for error messages, red color */
}
</style>

<style scoped>

</style>
