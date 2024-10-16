<script setup lang="ts">
   import { RouterLink, RouterView } from 'vue-router'
   import  {bookServices} from '@/services/bookServices'
   import {useBooksStore} from '@/stores/books'

  const onGetAllBooksClicked =  async ()=> {
     const books = await bookServices.getAllBooks()
    console.log(books)
     const store = useBooksStore()
    console.log(store.books)
     store.setBooks(books)
  }

</script>

<template>
<!--  <body>-->
  <header class="header">
    <!-- Logo Image -->
    <img
      alt="Vue logo"
      class="logo"
      src="@/assets/logo.svg"
      width="125"
      height="125"
    />

    <!-- Navigation & Button -->
    <div class="nav-wrapper">
      <nav>
        <RouterLink to="/" class="nav-button">Add Book</RouterLink>
        <RouterLink to="bookList" class="nav-button">List all Books</RouterLink>
        <RouterLink to="bookSearch" class="nav-button">Book Search</RouterLink>
        <RouterLink to="bookDelete" class="nav-button">Delete Book</RouterLink>
      </nav>

      <!-- Button -->
      <div class="get-books-btn">
        <button @click="onGetAllBooksClicked()">Get All Books</button>
      </div>
    </div>
    <RouterView/>
  </header>
<!--  </body>-->



</template>

<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

.header {
  display: flex;
  align-items: flex-start;
  justify-content: flex-start;
  width: 100%; /* Full width */
  padding: 20px;
  //background-color: #f5f5f5;
  position: fixed; /* Fixes the header to the top of the page */
  top: 0;
  left: 0;
  z-index: 1000; /* Make sure it's on top */
}

.logo {
  margin-right: 20px; /* Space between logo and nav */
}

.nav-wrapper {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  justify-content: flex-start;
}

nav {
  margin-bottom: 20px; /* Space between nav and button */
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
  display: inline-block;
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

.get-books-btn button {
  padding: 10px 20px;
  font-size: 16px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.get-books-btn button:hover {
  background-color: #0056b3;
}
</style>





<!--<script setup lang="ts">-->
<!--// import { RouterLink, RouterView } from 'vue-router'-->
<!--// import HelloWorld from './components/HelloWorld.vue'-->

<!--</script>-->

<!--<template>-->
<!--  <header>-->
<!--    <img-->
<!--      alt="Vue logo"-->
<!--      class="logo"-->
<!--      src="@/assets/logo.svg"-->
<!--      width="125"-->
<!--      height="125"-->
<!--    />-->

<!--    <div class="wrapper">-->
<!--      &lt;!&ndash;      <HelloWorld msg="You did it!" />&ndash;&gt;-->


<!--      <nav>-->
<!--        &lt;!&ndash;        <RouterLink to="/">Home</RouterLink>&ndash;&gt;-->
<!--        &lt;!&ndash;        <RouterLink to="/about">About</RouterLink>&ndash;&gt;-->
<!--      </nav>-->
<!--    </div>-->
<!--  </header>-->

<!--  &lt;!&ndash;  <RouterView />&ndash;&gt;-->
<!--</template>-->


<!--
header {
    line-height: 1.5;
    max-height: 100vh;
  }

  .logo {
    display: block;
    margin: 0 auto 2rem;
  }

  nav {
    width: 100%;
    font-size: 12px;
    text-align: center;
    margin-top: 2rem;
  }

  nav a.router-link-exact-active {
    color: var(--color-text);
  }

  nav a.router-link-exact-active:hover {
    background-color: transparent;
  }

  nav a {
    display: inline-block;
    padding: 0 1rem;
    border-left: 1px solid var(--color-border);
  }

  nav a:first-of-type {
    border: 0;
  }

  @media (min-width: 1024px) {
    header {
      display: flex;
      place-items: center;
      padding-right: calc(var(--section-gap) / 2);
    }

    .logo {
      margin: 0 2rem 0 0;
    }

    header .wrapper {
      display: flex;
      place-items: flex-start;
      flex-wrap: wrap;
    }

    nav {
      text-align: left;
      margin-left: -1rem;
      font-size: 1rem;

      padding: 1rem 0;
      margin-top: 1rem;
    }
  }
  -->
