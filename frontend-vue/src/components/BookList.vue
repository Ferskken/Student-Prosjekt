<template>
  <div class="book-list">
    <h2>Book List</h2>
    <div class="scroll-container">
      <div class="grid-container">
        <div class="grid-item" v-for="book in books" :key="book.id" @click="selectBook(book)">
          <div class="delete-icon" @click.stop="deleteBook(book.id)">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24">
              <path
                d="M11.447 8.894a1 1 0 1 0-.894-1.789l.894 1.789zm-2.894-.789a1 1 0 1 0 .894 1.789l-.894-1.789zm0 1.789a1 1 0 1 0 .894-1.789l-.894 1.789zM7.447 7.106a1 1 0 1 0-.894 1.789l.894-1.789zM10 9a1 1 0 1 0-2 0h2zm-2 2.5a1 1 0 1 0 2 0H8zm9.447-5.606a1 1 0 1 0-.894-1.789l.894 1.789zm-2.894-.789a1 1 0 1 0 .894 1.789l-.894-1.789zm2 .789a1 1 0 1 0 .894-1.789l-.894 1.789zm-1.106-2.789a1 1 0 1 0-.894 1.789l.894-1.789zM18 5a1 1 0 1 0-2 0h2zm-2 2.5a1 1 0 1 0 2 0h-2zm-5.447-4.606a1 1 0 1 0 .894-1.789l-.894 1.789zM9 1l.447-.894a1 1 0 0 0-.894 0L9 1zm-2.447.106a1 1 0 1 0 .894 1.789l-.894-1.789zm-6 3a1 1 0 1 0 .894 1.789L.553 4.106zm2.894.789a1 1 0 1 0-.894-1.789l.894 1.789zm-2-.789a1 1 0 1 0-.894 1.789l.894-1.789zm1.106 2.789a1 1 0 1 0 .894-1.789l-.894 1.789zM2 5a1 1 0 1 0-2 0h2zM0 7.5a1 1 0 1 0 2 0H0zm8.553 12.394a1 1 0 1 0 .894-1.789l-.894 1.789zm-1.106-2.789a1 1 0 1 0-.894 1.789l.894-1.789zm1.106 1a1 1 0 1 0 .894 1.789l-.894-1.789zm2.894.789a1 1 0 1 0-.894-1.789l.894 1.789zM8 19a1 1 0 1 0 2 0H8zm2-2.5a1 1 0 1 0-2 0h2zm-7.447.394a1 1 0 1 0 .894-1.789l-.894 1.789zM1 15H0a1 1 0 0 0 .553.894L1 15zm1-2.5a1 1 0 1 0-2 0h2zm12.553 2.606a1 1 0 1 0 .894 1.789l-.894-1.789zM17 15l.447.894A1 1 0 0 0 18 15h-1zm1-2.5a1 1 0 1 0-2 0h2zm-7.447-5.394l-2 1 .894 1.789 2-1-.894-1.789zm-1.106 1l-2-1-.894 1.789 2 1 .894-1.789zM8 9v2.5h2V9H8zm8.553-4.894l-2 1 .894 1.789 2-1-.894-1.789zm.894 0l-2-1-.894 1.789 2 1 .894-1.789zM16 5v2.5h2V5h-2zm-4.553-3.894l-2-1-.894 1.789 2 1 .894-1.789zm-2.894-1l-2 1 .894 1.789 2-1L8.553.106zM1.447 5.894l2-1-.894-1.789-2 1 .894 1.789zm-.894 0l2 1 .894-1.789-2-1-.894 1.789zM0 5v2.5h2V5H0zm9.447 13.106l-2-1-.894 1.789 2 1 .894-1.789zm0 1.789l2-1-.894-1.789-2 1 .894 1.789zM10 19v-2.5H8V19h2zm-6.553-3.894l-2-1-.894 1.789 2 1 .894-1.789zM2 15v-2.5H0V15h2zm13.447 1.894l2-1-.894-1.789-2 1 .894 1.789zM18 15v-2.5h-2V15h2z"
              />           </svg>
          </div>
          <h3>{{ book.title }}</h3>
          <p><strong>ID:</strong> {{ book.id }}</p>
          <p><strong>Author:</strong> {{ book.author }}</p>
          <p><strong>Publisher:</strong> {{ book.publisher }}</p>
          <p><strong>Publication Date:</strong> {{ new Date(book.publicationDate).toLocaleDateString() }}</p>
          <p><strong>ISBN:</strong> {{ book.isbn }}</p>
          <p><strong>Edition:</strong> {{ book.edition }}</p>
          <p><strong>Language:</strong> {{ book.language }}</p>
        </div>
      </div>
    </div>
    <div v-if="loading">Loading books...</div>
    <div v-if="error" class="error">{{ error }}</div>

    <!-- Selected Book Information -->
    <div v-if="selectedBook" class="selected-book">
      <h3>Selected Book Details</h3>
      <p><strong>Title:</strong> {{ selectedBook.title }}</p>
      <p><strong>ID:</strong> {{ selectedBook.id }}</p>
      <p><strong>Author:</strong> {{ selectedBook.author }}</p>
      <p><strong>Publisher:</strong> {{ selectedBook.publisher }}</p>
      <p><strong>Publication Date:</strong> {{ new Date(selectedBook.publicationDate).toLocaleDateString() }}</p>
      <p><strong>ISBN:</strong> {{ selectedBook.isbn }}</p>
      <p><strong>Edition:</strong> {{ selectedBook.edition }}</p>
      <p><strong>Language:</strong> {{ selectedBook.language }}</p>
    </div>
  </div>
</template>





<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useBooksStore } from '@/stores/books';
import { bookServices } from '@/services/bookServices';
import type { BookModel } from '@/models/bookModel';

const store = useBooksStore();
const loading = ref(true);
const error = ref<string | null>(null);
const selectedBook = ref<BookModel | null>(null); // Reactive reference for the selected book

onMounted(async () => {
  try {
    loading.value = true; // Set loading to true before fetching
    const books: BookModel[] = await bookServices.getAllBooks();
    store.setBooks(books); // Store fetched books in Pinia store
  } finally {
    loading.value = false; // Set loading to false after fetching
  }
});

const books = computed(() => store.getBooks);

const selectBook = (book: BookModel) => {
  selectedBook.value = book;
};

const deleteBook = async (bookId: number) => {
  console.log(`Attempting to delete book with ID: ${bookId}`);

  try {
    await bookServices.deleteBookbyId(bookId);
    console.log(`Book with ID ${bookId} deleted successfully`);

    // Update the state to remove the deleted book
    store.setBooks(store.getBooks.filter(book => book.id !== bookId));
    selectedBook.value = null; // Reset the selected book
  } catch (err) {
    console.error('Error details:', err);
    error.value = 'Failed to delete the book. Please try again.';
  }
};

</script>


<style scoped>
.book-list {
  margin: 20px;
}
.scroll-container {
  max-height: 900px;
  overflow-y: auto;
  border: 1px solid #ccc;
  border-radius: 5px;
}
.grid-container {
  display: grid;
  grid-template-columns: repeat(5, minmax(200px, 1fr)); /* Responsive grid */
  gap: 10px; /* Space between grid items */
}
.grid-item {
  position: relative; /* Positioning context for the delete icon */
  border: 1px solid #ddd;
  border-radius: 5px;
  padding: 10px;
}
.delete-icon {
  position: absolute;
  top: 10px;
  right: 10px;
  cursor: pointer;
}
.error {
  color: red;
  margin-top: 10px;
}
.selected-book {
  margin-top: 20px;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
}
</style>

