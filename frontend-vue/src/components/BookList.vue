<template>
  <div class="book-list">
    <h2>Book List</h2>
    <div class="scroll-container">
      <div class="grid-container">
        <div class="grid-item" v-for="book in books" :key="book.id">
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
  </div>
</template>


<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useBooksStore } from '@/stores/books';
import { bookServices } from '@/services/bookServices';
import type { BookModel } from '@/models/bookModel';

const store = useBooksStore();
const loading = ref(true);
const error = ref<string | null>(null);

// Fetch books from the backend when the component is mounted
onMounted(async () => {
  try {
    loading.value = true; // Set loading to true before fetching
    const books: BookModel[] = await bookServices.getAllBooks();
    store.setBooks(books); // Store fetched books in Pinia store
  }  finally {
    loading.value = false; // Set loading to false after fetching
  }
});

// Computed property to get books from the store
const books = computed(() => store.getBooks);
</script>

<style scoped>
.book-list {
  margin: 20px;
}

.scroll-container {
  max-height: 1200px; /* Set the maximum height for the scrollable area */
  overflow-y: auto; /* Allow vertical scrolling */
  border: 1px solid #ccc; /* Optional: border for the scrollable area */
  border-radius: 5px; /* Optional: rounded corners for the scrollable area */
}

.grid-container {
  display: grid;
  grid-template-columns: repeat(5, minmax(200px, 1fr)); /* Adjust the min width as needed */
  gap: 20px; /* Space between grid items */
}

.grid-item {
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
  background-color: #f9f9f9; /* Optional: background color for the grid item */
}

.error {
  color: red;
  margin-top: 10px;
}
</style>

