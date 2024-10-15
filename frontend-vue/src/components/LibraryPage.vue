

<template>
  <div>
    <h1>Library</h1>
    <input v-model="newBookTitle" placeholder="New Book Title" />
    <button @click="addBook">Add Book</button>
    <ul>
      <li v-for="book in books" :key="book.id">{{ book.title }}</li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const books = ref([
  { id: 1, title: 'The Great Gatsby' },
  { id: 2, title: 'To Kill a Mockingbird' },
  { id: 3, title: '1984' },
]);
const newBookTitle = ref('');

const addBook = () => {
  if (newBookTitle.value) {
    const newBook = {
      id: books.value.length + 1,
      title: newBookTitle.value,
    };
    books.value.push(newBook);
    newBookTitle.value = ''; // Clear input field
  }
};
</script>

<style scoped>


</style>
