<script setup lang="ts">
   import { RouterLink, RouterView } from 'vue-router'
   import HelloWorld from './components/HelloWorld.vue'
   import  {bookServices} from '@/services/bookServices'
   import {useBooksStore} from '@/stores/books'
   //import NewBook from '@/components/NewBook.vue'

  const onGetAllBooksClicked =  async ()=> {
     const books = await bookServices.getAllBooks()
    console.log(books)
     const store = useBooksStore()
    console.log(store.books)
     store.setBooks(books)
  }

</script>

<template>
  <header>


    <img
      alt="Vue logo"
      class="logo"
      src="@/assets/logo.svg"
      width="125"
      height="125"
    />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />
      <button @click="onGetAllBooksClicked()">Book Services>
        GetAllBooks
      </button>




      <nav>
        <RouterLink to="/">Home</RouterLink>
        <RouterLink to="bookList">List all Books</RouterLink>

      </nav>
    </div>
  </header>

  <RouterView />

</template>

<style scoped>

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
