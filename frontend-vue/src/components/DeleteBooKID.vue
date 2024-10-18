<template>
  <div>
    <h2>Delete a Book</h2> <!-- Header for the delete book section -->
    <input v-model="bookId" type="number" placeholder="Enter book ID to delete" /> <!-- Input for entering the book ID to delete -->
    <button @click="deleteBook">Delete Book</button> <!-- Button to trigger the delete action -->
    <div v-if="error" class="error">{{ error }}</div> <!-- Display error message if there is an error -->
    <div v-if="success" class="success">{{ success }}</div> <!-- Display success message if the book is deleted successfully -->
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'; // Importing reactive references from Vue
import { bookServices } from '@/services/bookServices'; // Importing book services for API interactions

// Reactive reference for the book ID to be deleted, initialized to null
const bookId = ref<number | null>(null);

// Reactive reference for error messages
const error = ref<string | null>(null);

// Reactive reference for success messages
const success = ref<string | null>(null);

// Function to delete a book based on the entered ID
const deleteBook = async () => {
  // Check if book ID is provided
  if (bookId.value === null) {
    error.value = 'Please enter a valid book ID.'; // Set error message if no ID is entered
    return; // Exit the function if ID is invalid
  }

  try {
    success.value = null; // Reset success message
    error.value = null; // Reset error message

    // Attempt to retrieve the book by its ID
    const book = await bookServices.getBookById(bookId.value);

    if (book) {
      // If the book is found, attempt to delete it
      await bookServices.deleteBookbyId(bookId.value);
      success.value = `Book with ID ${bookId.value} deleted successfully.`; // Set success message upon successful deletion
    } else {
      // If the book is not found, set an appropriate error message
      error.value = `Book with ID ${bookId.value} not found.`;
    }
  } catch (err) {
    // Catch any errors during the deletion process
    error.value = 'Failed to delete the book. Please try again.'; // Set error message on failure
  }
};
</script>

<style scoped>
.error {
  color: red; /* Style for error messages, red color */
}
.success {
  color: green; /* Style for success messages, green color */
}
</style>
