<template>
  <div class="book-list">
<!--    <h2>Book List</h2>-->
    <div>
      <input
        type="text"
        v-model="searchTerm"
        placeholder="Search books..."
        @input="searchBooks"
      />
    </div>
    <div class="scroll-container">
      <div class="grid-container">
<!--        <div class="grid-item" v-for="book in books" :key="book.id" @click="selectBook(book)">-->
          <div class="grid-item" v-for="book in filteredBooks" :key="book.id" @click="selectBook(book)">
            <div class="delete-icon" @click.stop="deleteBook(book.id)">
              <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24" height="24" viewBox="0 0 256 256" xml:space="preserve">
    <g style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill-rule: nonzero; opacity: 1;" transform="translate(1.4065934065934016 1.4065934065934016) scale(2.81 2.81)">
      <path d="M 76.777 2.881 H 57.333 V 2.412 C 57.333 1.08 56.253 0 54.921 0 H 35.079 c -1.332 0 -2.412 1.08 -2.412 2.412 v 0.469 H 13.223 c -1.332 0 -2.412 1.08 -2.412 2.412 v 9.526 c 0 1.332 1.08 2.412 2.412 2.412 h 63.554 c 1.332 0 2.412 -1.08 2.412 -2.412 V 5.293 C 79.189 3.961 78.109 2.881 76.777 2.881 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" transform="matrix(1 0 0 1 0 0)" stroke-linecap="round" />
      <path d="M 73.153 22.119 H 16.847 c -1.332 0 -2.412 1.08 -2.412 2.412 v 63.057 c 0 1.332 1.08 2.412 2.412 2.412 h 56.306 c 1.332 0 2.412 -1.08 2.412 -2.412 V 24.531 C 75.565 23.199 74.485 22.119 73.153 22.119 z M 33.543 81.32 c 0 1.332 -1.08 2.412 -2.412 2.412 h -2.245 c -1.332 0 -2.412 -1.08 -2.412 -2.412 V 30.799 c 0 -1.332 1.08 -2.412 2.412 -2.412 h 2.245 c 1.332 0 2.412 1.08 2.412 2.412 V 81.32 z M 48.535 81.32 c 0 1.332 -1.08 2.412 -2.412 2.412 h -2.245 c -1.332 0 -2.412 -1.08 -2.412 -2.412 V 30.799 c 0 -1.332 1.08 -2.412 2.412 -2.412 h 2.245 c 1.332 0 2.412 1.08 2.412 2.412 V 81.32 z M 63.526 81.32 c 0 1.332 -1.08 2.412 -2.412 2.412 h -2.245 c -1.332 0 -2.412 -1.08 -2.412 -2.412 V 30.799 c 0 -1.332 1.08 -2.412 2.412 -2.412 h 2.245 c 1.332 0 2.412 1.08 2.412 2.412 V 81.32 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" transform="matrix(1 0 0 1 0 0)" stroke-linecap="round" />
    </g>
  </svg>
            </div>

            <h3>{{ book.title }}</h3>
          <p><strong>ID:</strong> {{ book.id }}</p>
          <p><strong>Author:</strong> {{ book.author }}</p>
          <p><strong>Publisher:</strong> {{ book.publisher }}</p>
          <p><strong>Publication Date:</strong> {{ new Date(book.publicationDate).toLocaleDateString() }}</p>
          <p><strong>ISBN:</strong> {{ book.isbn }}</p>
          <p><strong>Edition:</strong> {{ book.edition }}</p>
          <p><strong>Language:</strong> {{ book.language }}</p>
        </div>
      </div>
    </div>
    <div v-if="loading">Loading books...</div>
    <div v-if="error" class="error">{{ error }}</div>

    <!-- Selected Book Information -->
    <div v-if="selectedBook" class="selected-book">
      <h3>Selected Book Details</h3>
      <div v-if="isEditing">
        <div>
        <label for="edit-title">Title:</label>
        <input id="edit-title" v-model="editBook.title" required />
      </div>

        <div>
        <label for="edit-author">Author:</label>
        <input id="edit-author" v-model="editBook.author" required />
        </div>
          <div>
        <label for="edit-publisher">Publisher:</label>
        <input id="edit-publisher" v-model="editBook.publisher" required />
          </div>
            <div>
        <label for="edit-publication-date">Publication Date:</label>
        <input id="edit-publication-date" type="datetime-local" v-model="editBook.publicationDate" required />
            </div>
              <div>
        <label for="edit-isbn">ISBN:</label>
        <input id="edit-isbn" v-model="editBook.isbn" required />
              </div>
        <div>
        <label for="edit-edition">Edition:</label>
        <input id="edit-edition" v-model="editBook.edition" />
        </div>
          <div>
        <label for="edit-language">Language:</label>
        <input id="edit-language" v-model="editBook.language" required />
          </div>
            <div>
        <button class="edit-book-update-button" @click="updateBook">Update</button>
        <button class="edit-book-cancel-button" @click="cancelEdit">Cancel</button>
            </div>
      </div>
      <div v-else>
        <p><strong>Title:</strong> {{ selectedBook.title }}</p>
        <p><strong>ID:</strong> {{ selectedBook.id }}</p>
        <p><strong>Author:</strong> {{ selectedBook.author }}</p>
        <p><strong>Publisher:</strong> {{ selectedBook.publisher }}</p>
        <p><strong>Publication Date:</strong> {{ new Date(selectedBook.publicationDate).toLocaleDateString() }}</p>
        <p><strong>ISBN:</strong> {{ selectedBook.isbn }}</p>
        <p><strong>Edition:</strong> {{ selectedBook.edition }}</p>
        <p><strong>Language:</strong> {{ selectedBook.language }}</p>
        <button class="selected-book-edit-button" @click="startEditing">Edit</button>
        <button class="selected-book-delete-button" @click="console.log(`${book.value.id}`)">Delete</button>
<!--        deleteBook(book.id)-->
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useBooksStore } from '@/stores/books';
import { bookServices } from '@/services/bookServices';
import type { BookModel } from '@/models/bookModel';
import Fuse from 'fuse.js';

const store = useBooksStore();
const loading = ref(true);
const error = ref<string | null>(null);
const selectedBook = ref<BookModel | null>(null);
const isEditing = ref(false);
const editBook = ref<BookModel | null>(null); // For editing book

onMounted(async () => {
  try {
    loading.value = true;
    const books: BookModel[] = await bookServices.getAllBooks();
    store.setBooks(books);
  } finally {
    loading.value = false;
  }
});

const allBooks = computed(() => store.getBooks);

const selectBook = (book: BookModel) => {
  selectedBook.value = book;
  editBook.value = { ...book }; // Copy the selected book for editing
  isEditing.value = false; // Reset editing state
};



const deleteBook = async (bookId: number) => {
  console.log(`Attempting to delete book with ID: ${bookId}`);
  try {
    await bookServices.deleteBookbyId(bookId);
    console.log(`Book with ID ${bookId} deleted successfully`);
    store.setBooks(store.getBooks.filter(book => book.id !== bookId));
    selectedBook.value = null;
  } catch (err) {
    console.error('Error details:', err);
    error.value = 'Failed to delete the book. Please try again.';
  }
};

// Start editing mode
const startEditing = () => {
  editBook.value = { ...selectedBook.value,publicationDate: selectedBook.value.publicationDate };
  console.log(selectedBook.value.publicationDate)
  console.log(selectedBook.value)
  isEditing.value = true;
};

// Cancel editing mode
const cancelEdit = () => {
  isEditing.value = false; // Exit editing mode
  if (selectedBook.value) {
    editBook.value = { ...selectedBook.value };

  }
};

// Update book details
const updateBook = async () => {
  if (!editBook.value) return;

  try {
    await bookServices.updateBook(editBook.value); // Call the update service
    console.log(`Book with ID ${editBook.value.id} updated successfully`);

    // Update store and reset selected book
    store.setBooks(store.getBooks.map(book => (book.id === editBook.value.id ? editBook.value : book)));
    selectedBook.value = editBook.value; // Update the selected book to the edited one
    isEditing.value = false; // Exit editing mode
  } catch (err) {
    console.error('Error details:', err);
    error.value = 'Failed to update the book. Please try again.';
  }
};

const searchTerm = ref('');
const filteredBooks = ref([]);

const books = computed(() => store.getBooks);

onMounted(() => {
  filteredBooks.value = books.value;
});

const searchBooks = () => {

  if (!searchTerm.value.trim()) {

    filteredBooks.value = allBooks.value;
    return;
  }

  const options = {
    keys: ['title', 'author', 'publisher', 'isbn', 'language','id','publicationDate'],
    threshold: 0.2,
  };

  const fuse = new Fuse(allBooks.value, options);

  const result = fuse.search(searchTerm.value);

  filteredBooks.value = result.map(item => item.item);
};




</script>

<style scoped>
.book-list {
  margin: 10px;
}
.scroll-container {
  max-height: 65rem;
  overflow-y: auto;
  border-radius: 5px;
}
.grid-container {
  display: grid;
  grid-template-columns: repeat(7, minmax(250px, 1fr)); /* Makes the grid responsive */
  gap: 10px;
  padding: 10px;
  max-width: 100%; /* Prevents overflow */
  box-sizing: border-box;
}

.grid-item {
  cursor: pointer;
  background: white;
  position: relative;
  border-radius: 5px;
  padding: 10px;
}
.delete-icon {
  position: absolute;
  top: 10px;
  right: -1px;
  cursor: pointer;
}
.delete-icon:hover svg path {
  fill: red;
}

.error {
  color: red;
  margin-top: 10px;
}
.selected-book {
  height: 16.5rem;
  width: 20rem;
  position: absolute;
  top: -14.5rem;
  left: 60rem;
  background: white;
  margin-top: 20px;
  padding: 10px;
  border-radius: 5px;
}
.selected-book-edit-button{
  position: absolute;
  bottom: 0.5rem;
  right: 1rem;
}
.selected-book-delete-button{
  position: absolute;
  bottom: 0.5rem;
  right: 4rem;
}
.edit-book-update-button{
  position: absolute;
  bottom: 0.5rem;
  right: 1rem;
}
.edit-book-cancel-button{
  position: absolute;
  bottom: 0.5rem;
  right: 5rem;
}

</style>
