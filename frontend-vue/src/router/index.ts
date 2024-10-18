// Importing necessary functions from the vue-router library
import { createRouter, createWebHistory } from 'vue-router';

// Creating a router instance
const router = createRouter({
  // Using HTML5 history mode for cleaner URLs
  history: createWebHistory(import.meta.env.BASE_URL),

  // Defining the application's routes
  routes: [
    {
      // Route for the root path, renders the NewBook component
      path: '/',
      name: 'newBook', // Name for the route, can be used for navigation
      component: () => import('@/components/NewBook.vue'), // Lazy-loading the component
    },
    {
      // Route for the book list page
      path: '/bookList',
      name: 'bookList', // Name for the route
      component: () => import('../views/BookList.vue'), // Lazy-loading the component
    },
    {
      // Route for searching for a book
      path: '/bookSearch',
      name: 'bookSearch', // Name for the route
      component: () => import('../components/BookSearchId.vue'), // Lazy-loading the component
    },
    {
      // Route for deleting a book by ID
      path: '/bookDelete',
      name: 'bookDelete', // Name for the route
      component: () => import('../components/DeleteBooKID.vue'), // Lazy-loading the component
    },
  ],
});

// Exporting the router instance for use in the main application
export default router;
