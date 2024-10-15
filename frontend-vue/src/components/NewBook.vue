<template>
  <div>
    <h2>Add a New Book</h2>
    <form @submit.prevent="handleSubmit">
      <div>
        <label for="title">Title:</label>
        <input type="text" v-model="newBook.title" required />
      </div>
      <div>
        <label for="author">Author:</label>
        <input type="text" v-model="newBook.author" required />
      </div>
      <div>
        <label for="publisher">Publisher:</label>
        <input type="text" v-model="newBook.publisher" required />
      </div>
      <div>
        <label for="publicationDate">Publication Date:</label>
        <input type="date" v-model="newBook.publicationDate" required />
      </div>
      <div>
        <label for="isbn">ISBN:</label>
        <input type="text" v-model="newBook.isbn" required />
      </div>
      <div>
        <label for="edition">Edition:</label>
        <input type="text" v-model="newBook.edition" />
      </div>
      <div>
        <label for="language">Language:</label>
        <input type="text" v-model="newBook.language" required />
      </div>
      <button type="submit">Add Book</button>
    </form>
    <div v-if="error" class="error">{{ error }}</div> <!-- This line accesses error -->
  </div>

</template>

<script setup lang="ts">
import { ref, computed } from 'vue';  // Ensure computed is imported
import { useBooksStore } from '@/stores/books';
import { bookServices } from '@/services/bookServices';
import type { BookModel } from '@/models/bookModel';

// Reactive reference for the new book
const newBook = ref<BookModel>({
  id: 0,  // Assuming ID is managed by the backend
  title: '',
  author: '',
  publisher: '',
  publicationDate: new Date().toISOString().split('T')[0], // Default to today's date
  isbn: '',
  edition: '',
  language: '',
});


const error = ref<string | null>(null);

const store = useBooksStore();

const handleSubmit = async () => {

  if (!newBook.value.title || !newBook.value.author || !newBook.value.publisher || !newBook.value.publicationDate || !newBook.value.isbn || !newBook.value.language) {
    error.value = 'Please fill in all required fields.';
    return;
  }

  try {

    const addedBook = await bookServices.addBook(newBook.value);
    store.setBooks([...store.getBooks, addedBook]);


    newBook.value = {
      id: 0,
      title: '',
      author: '',
      publisher: '',
      publicationDate: new Date().toISOString().split('T')[0],
      isbn: '',
      edition: '',
      language: '',
    };

    error.value = null;
  } catch (err) {
    error.value = 'Failed to add the book. Please try again.';
  }
};
</script>

<style scoped>
.error {
  color: red;
  margin-top: 10px;
}
</style>
