<template>
  <div class="book-list">
    <h2>Book List</h2>
    <ul>
      <li v-for="book in books" :key="book.id">
        <h3>{{ book.title }}</h3>
        <p><strong>ID:</strong> {{ book.id }}</p>
        <p><strong>Author:</strong> {{ book.author }}</p>
        <p><strong>Publisher:</strong> {{ book.publisher }}</p>
        <p><strong>Publication Date:</strong> {{ new Date(book.publicationDate).toLocaleDateString() }}</p>
        <p><strong>ISBN:</strong> {{ book.isbn }}</p>
        <p><strong>Edition:</strong> {{ book.edition }}</p>
        <p><strong>Language:</strong> {{ book.language }}</p>
      </li>
    </ul>
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
ul {
  list-style-type: none;
  padding: 0;
}
li {
  margin: 10px 0;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
}
.error {
  color: red;
  margin-top: 10px;
}
</style>
