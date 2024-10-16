<template>
  <div>
    <h2>Delete a Book</h2>
    <input v-model="bookId" type="number" placeholder="Enter book ID to delete" />
    <button @click="deleteBook">Delete Book</button>
    <div v-if="error" class="error">{{ error }}</div>
    <div v-if="success" class="success">{{ success }}</div>
  </div>

</template>

<script setup lang="ts">
import { ref } from 'vue';
import { bookServices } from '@/services/bookServices';

const bookId = ref<number | null>(null);
const error = ref<string | null>(null);
const success = ref<string | null>(null);

const deleteBook = async () => {
  if (bookId.value === null) {
    error.value = 'Please enter a valid book ID.';
    return;
  }

  try {
    success.value = null;
    error.value = null;


    const book = await bookServices.getBookById(bookId.value);

    if (book) {
      await bookServices.deleteBookbyId(bookId.value);
      success.value = `Book with ID ${bookId.value} deleted successfully.`;
    } else {
      error.value = `Book with ID ${bookId.value} not found.`;
    }
  } catch (err) {
    error.value = 'Failed to delete the book. Please try again.';
  }
};
</script>

<style scoped>
.error {
  color: red;
}
.success {
  color: green;
}
</style>
