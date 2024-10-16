
<template>
  <div class="book-search">
    <h2>Search for a Book</h2>
    <div>
      <label>Search by:</label>
      <select v-model="searchType">
        <option value="id">ID</option>
        <option value="title">Title</option>
        <option value="isbn">ISBN</option>
        <option value="publisher">Publisher</option>
        <option value="author">Author</option>
      </select>
      <input v-model="query" type="text" placeholder="Enter search value" />
      <button @click="searchBook">Search</button>
    </div>

    <div v-if="loading">Searching...</div>
    <div v-if="error" class="error">{{ error }}</div>

    <div v-if="book">
      <h3>{{ book.title }}</h3>
      <p><strong>Author:</strong> {{ book.author }}</p>
      <p><strong>Publisher:</strong> {{ book.publisher }}</p>
      <p><strong>Publication Date:</strong> {{ new Date(book.publicationDate).toLocaleDateString() }}</p>
      <p><strong>ISBN:</strong> {{ book.isbn }}</p>
      <p><strong>Edition:</strong> {{ book.edition }}</p>
      <p><strong>Language:</strong> {{ book.language }}</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { bookServices } from '@/services/bookServices';
import type { BookModel } from '@/models/bookModel';

const searchType = ref('id'); // Default search type is ID
const query = ref('');
const book = ref<BookModel | null>(null);
const loading = ref(false);
const error = ref<string | null>(null);

const searchBook = async () => {
  loading.value = true;
  error.value = null;
  book.value = null;

  try {
    let result;
    if (searchType.value === 'id') {
      result = await bookServices.getBookById(Number(query.value));
    } else if (searchType.value === 'title') {
      result = await bookServices.getBookByTitle(query.value);
    } else if (searchType.value === 'isbn') {
      result = await bookServices.getBookByISBN(query.value);
    } else if (searchType.value === 'publisher') {
      result = await bookServices.getBookByPublisher(query.value);
    } else if (searchType.value === 'author') {
      result = await bookServices.getBookByAuthor(query.value);
    }

    if (Array.isArray(result) && result.length > 1) {
      // Show only the first result but indicate that there are more
      book.value = result[0];
      error.value = 'Showing the first result out of multiple matches.';
    } else {
      book.value = Array.isArray(result) ? result[0] : result;
    }
  } catch (err) {
    error.value = 'Book not found.';
  } finally {
    loading.value = false;
  }
};


</script>

<style scoped>
.book-search {
  margin: 20px;
}
input {
  margin: 10px;
}
.error {
  color: red;
}
</style>


<style scoped>

</style>
