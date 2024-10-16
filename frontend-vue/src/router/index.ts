import { createRouter, createWebHistory } from 'vue-router'
// import HomeView from '../views/HomeView.vue'

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
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/BookList.vue'),
    },
    {
      path: '/bookSearch',
      name: 'bookSearch',
      component: () => import('../components/BookSearch.vue'),
    },
    {
      path: '/bookDelete',
      name: 'bookDelete',
      component: () => import('../components/DeleteBooKID.vue'),
    },

  ],
})

export default router
