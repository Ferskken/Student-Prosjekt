
import { defineStore } from 'pinia'
import type { BookModel } from '@/models/bookModel'

export const useBooksStore = defineStore('books', {
  state: () => ({
    books: [],
  }),
  getters: {
    // automatically infers the return type as a number
    getBooks(state) {
      return state.books
    },
    // the return type **must** be explicitly set

  },
  actions:{
    setBooks(books:BookModel[]){
      this.books.length=0
      this.books.push(...books)
    },
  }
})

//ref()s become state properties
// computed()s become getters
// function()s become actions
