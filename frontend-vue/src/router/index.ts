import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'newBook',
      component: () => import('@/components/NewBook.vue'),
    },
    {
      path: '/bookList',
      name: 'bookList',
      component: () => import('../views/BookList.vue'),
    },
    {
      path: '/bookSearch',
      name: 'bookSearch',
      component: () => import('../components/BookSearchId.vue'),
    },
    {
      path: '/bookDelete',
      name: 'bookDelete',
      component: () => import('../components/DeleteBooKID.vue'),
    },

  ],
})

export default router
