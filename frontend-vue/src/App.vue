<script setup lang="ts">
  import { RouterLink, RouterView } from 'vue-router'
  //import BookList from '@/components/BookList.vue'
  // import  {bookServices} from '@/services/bookServices'
  // import {useBooksStore} from '@/stores/books'

  // const onGetAllBooksClicked =  async ()=> {
  //    const books = await bookServices.getAllBooks()
  //   console.log(books)
  //    const store = useBooksStore()
  //   console.log(store.books)
  //    store.setBooks(books)
  // }
</script>

<template>
  <header class="header">
    <img
      alt="Vue logo"
      class="logo"
      src="@/assets/logo.svg"
      width="125"
      height="125"
    />

    <div class="nav-wrapper">
      <nav>
        <RouterLink to="/" class="nav-button">Add Book</RouterLink>
        <RouterLink to="bookList" class="nav-button">List all Books</RouterLink>
        <RouterLink to="bookSearch" class="nav-button">Book Search</RouterLink>
        <RouterLink to="bookDelete" class="nav-button">Delete Book</RouterLink>
      </nav>
    </div>
  </header>

  <!-- Ensure the main content is pushed below the header -->
  <div class="main-content">
    <RouterView />
    <!-- You can uncomment and use the component like this -->
    <!-- <book-list /> -->
  </div>
</template>


<style>
.header {
  display: flex;
  align-items: center;
  //justify-content: space-between; /* Distributes space between logo and nav */
  width: 100%;
  padding: 20px;
  background-color: #7daea6;
  position: fixed;
  top: 0;
  left: 0;
  //z-index: 1000;
  //box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.logo {
  margin-right: 20px;
}

.nav-wrapper {
  display: flex;
  justify-content: flex-end;
  align-items: center;
}

nav {
  display: flex;
}

.nav-button {
  padding: 10px 20px;
  margin-right: 10px;
  font-size: 16px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  text-decoration: none;
  cursor: pointer;
}

nav a {
  margin-right: 10px;
  text-decoration: none;
  color: #333;
  font-weight: bold;
}

nav a:hover {
  color: #007bff;
}

/* Style to ensure the content is below the fixed header */
.main-content {
  margin-top: 180px; /* Adjust based on your header height */
  position: absolute;
  top: 3rem;
  left: 1rem;
  padding: 20px;
  width: 95%;
}
body {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  background-color: #7daea6;
}

* {
  box-sizing: border-box;
}

</style>
