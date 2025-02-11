<template>
  <div class="book-list">
<!--  Container for the search bar  -->
    <div class="search-bar">
<!--   input field for book searches-  v-model Binds the input value to the 'searchTerm' data property in the Vue instance  @input Event listener that triggers the 'searchBooks' method whenever the input value changes -->
      <input
        type="text"
        v-model="searchTerm"
        placeholder="Search books..."
        @input="searchBooks"
      />
    </div>
    <div class="scroll-container">
      <div class="grid-container">
          <div class="grid-item" v-for="book in filteredBooks" :key="book.id" @click="selectBook(book)">
            <IconTrashcan :onDelete="() => deleteBook(book.id)" />

            <!-- Display the title of the book -->
            <h3 class = book-title>{{ book.title }}</h3>

            <!-- Display the ID of the book -->
            <p><strong>ID:</strong> {{ book.id }}</p>

            <!-- Display the author of the book -->
            <p><strong>Author:</strong> {{ book.author }}</p>

            <!-- Display the publisher of the book -->
            <p><strong>Publisher:</strong> {{ book.publisher }}</p>

            <!-- Display the publication date of the book, formatted to a localized date string -->
            <p><strong>Publication Date:</strong> {{ new Date(book.publicationDate).toLocaleDateString() }}</p>

            <!-- Display the ISBN of the book -->
            <p><strong>ISBN:</strong> {{ book.isbn }}</p>

            <!-- Display the edition of the book -->
            <p><strong>Edition:</strong> {{ book.edition }}</p>

            <!-- Display the language the book is written in -->
            <p><strong>Language:</strong> {{ book.language }}</p>

          </div> <!-- Closing tag for the book details container -->

      </div>
    </div>

    <!-- Message displayed when no books are found -->
    <div v-if="filteredBooks.length === 0" class="no-books-message">
      <h3>No books found!</h3> <!-- Heading indicating no books available -->
    </div>

    <!-- Loading message shown while books are being fetched -->
    <div v-if="loading">Loading books...</div>

    <!-- Error message displayed when there's an issue fetching books -->
    <div v-if="error" class="error">{{ error }}</div>

    <!-- Display for the selected book's details -->
    <div v-if="selectedBook" class="selected-book">
      <h3>Selected Book Details</h3> <!-- Heading for selected book details -->

      <!-- If the user is editing the book details -->
      <div v-if="isEditing">
        <div>
          <label for="edit-title">Title:</label> <!-- Label for title input -->
          <input id="edit-title" v-model="editBook.title" required /> <!-- Input for book title with two-way binding -->
        </div>

        <div>
          <label for="edit-author">Author:</label> <!-- Label for author input -->
          <input id="edit-author" v-model="editBook.author" required /> <!-- Input for book author -->
        </div>

        <div>
          <label for="edit-publisher">Publisher:</label> <!-- Label for publisher input -->
          <input id="edit-publisher" v-model="editBook.publisher" required /> <!-- Input for book publisher -->
        </div>

        <div>
          <label for="edit-publication-date">Publication Date:</label> <!-- Label for publication date input -->
          <input id="edit-publication-date" type="datetime-local" v-model="editBook.publicationDate" required /> <!-- Input for publication date -->
        </div>

        <div>
          <label for="edit-isbn">ISBN:</label> <!-- Label for ISBN input -->
          <input id="edit-isbn" v-model="editBook.isbn" required /> <!-- Input for book ISBN -->
        </div>

        <div>
          <label for="edit-edition">Edition:</label> <!-- Label for edition input -->
          <input id="edit-edition" v-model="editBook.edition" /> <!-- Input for book edition -->
        </div>

        <div>
          <label for="edit-language">Language:</label> <!-- Label for language input -->
          <input id="edit-language" v-model="editBook.language" required /> <!-- Input for book language -->
        </div>

        <!-- Buttons for updating or canceling the edit -->
        <div>
          <button class="edit-book-update-button" @click="updateBook">Update</button> <!-- Button to update book -->
          <button class="edit-book-cancel-button" @click="cancelEdit">Cancel</button> <!-- Button to cancel editing -->
        </div>
      </div>

      <!-- Displaying selected book details when not in editing mode -->
      <div v-else>
        <p class="book-title"><strong></strong> {{ selectedBook.title }}</p> <!-- Display book title -->
        <p><strong>ID:</strong> {{ selectedBook.id }}</p> <!-- Display book ID -->
        <p><strong>Author:</strong> {{ selectedBook.author }}</p> <!-- Display book author -->
        <p><strong>Publisher:</strong> {{ selectedBook.publisher }}</p> <!-- Display book publisher -->
        <p><strong>Publication Date:</strong> {{ new Date(selectedBook.publicationDate).toLocaleDateString() }}</p> <!-- Format and display publication date -->
        <p><strong>ISBN:</strong> {{ selectedBook.isbn }}</p> <!-- Display book ISBN -->
        <p><strong>Edition:</strong> {{ selectedBook.edition }}</p> <!-- Display book edition -->
        <p><strong>Language:</strong> {{ selectedBook.language }}</p> <!-- Display book language -->

        <!-- Buttons for editing or deleting the selected book -->
        <button class="selected-book-edit-button" @click.stop ="startEditing">Edit</button> <!-- Button to start editing book details -->
        <button class="selected-book-delete-button" @click.stop ="deleteBook(selectedBook.id)">Delete</button> <!-- Button to delete the selected book -->
      </div>
    </div>

  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted, computed } from 'vue';
import { useBooksStore } from '@/stores/books'; // Importing the book store
import { bookServices } from '@/services/bookServices'; // Importing book services for API calls
import type { BookModel } from '@/models/bookModel'; // Importing BookModel type
import Fuse from 'fuse.js'; // Importing Fuse.js for search functionality
import IconTrashcan from '@/components/icons/IconTrashcan.vue'

// Refs for handling component state
const store = useBooksStore(); // Access the Vuex store for books
const loading = ref(true); // Tracks loading state
const error = ref<string | null>(null); // Tracks error messages
const selectedBook = ref<BookModel | null>(null); // Holds the currently selected book
const isEditing = ref(false); // Indicates if the user is in editing mode
const editBook = ref<BookModel | null>(null); // Holds the book being edited
const searchTerm = ref(''); // Holds the search term input by the user
const filteredBooks = ref([]); // Stores the filtered list of books based on search
const exitingEditing = ref(false); // Tracks if the user is exiting editing mode


// Fetches all books when the component is mounted
onMounted(async () => {
  try {
    loading.value = true; // Start loading
    const books: BookModel[] = await bookServices.getAllBooks(); // Fetch all books
    store.setBooks(books); // Store books in Vuex store
  } finally {
    loading.value = false; // Stop loading when books are fetched
  }
});

// Computed property to get all books from the store
const allBooks = computed(() => store.getBooks);
// Computed property for accessing the list of books from the store
const books = computed(() => store.getBooks);

// Start editing the selected book
const startEditing = () => {
  if (selectedBook.value) {
    editBook.value = { ...selectedBook.value }; // Clone the selected book for editing
    isEditing.value = true; // Set editing mode to true
  }
};

// Cancel editing mode
const cancelEdit = () => {

  if (selectedBook.value) {
   // console.log(editBook.value)
    editBook.value = { ...selectedBook.value }; // Revert changes by cloning selected book again
   // console.log(selectedBook.value)
  }
  isEditing.value = false; // Exit editing mode
  exitingEditing.value = true; // Set flag for exiting editing mode
};

// Update book details
const updateBook = async () => {
  if (!editBook.value) return; // Do nothing if there's no book being edited

  try {
    await bookServices.updateBook(editBook.value); // Call the update service
    console.log(`Book with ID ${editBook.value.id} updated successfully`);

    // Update the book in the store after successful update
    store.setBooks(store.getBooks.map(book => (book.id === editBook.value.id ? editBook.value : book)));
    selectedBook.value = editBook.value; // Update the selected book to the edited one
    isEditing.value = false; // Exit editing mode
  } catch (err) {
    console.error('Error details:', err);
    error.value = 'Failed to update the book. Please try again.';
  }
};


// Set filtered books when component is mounted
onMounted(() => {
  filteredBooks.value = books.value;  // Initially show all books
});


// Search through books using Fuse.js based on the search term
const searchBooks = () => {

  if (!searchTerm.value.trim()) {

    filteredBooks.value = allBooks.value; // If search term is empty, show all books
    return;
  }

  // fuse.ja search options
  const options = {
    keys: ['title', 'author', 'publisher', 'isbn', 'language','id','publicationDate'],
    threshold: 0.2, //fuzzy matching threshold
  };

  const fuse = new Fuse(allBooks.value, options); // Initialize Fuse with all books and options

  const result = fuse.search(searchTerm.value); // Perform the search

  filteredBooks.value = result.map(item => item.item); // Update filtered books with search results
};

// Select a book from the list
const selectBook = (book: BookModel) => {
  selectedBook.value = book; // Set selected book
  editBook.value = { ...book }; // Clone book for potential editing
  isEditing.value = false; // Ensure editing mode is off
};

// Delete a book by its ID
const deleteBook = async (bookId: number) => {
  try {
    await bookServices.deleteBookbyId(bookId); // Call the service to delete the book
    store.setBooks(store.getBooks.filter(book => book.id !== bookId)); // Remove the book from the store
    selectedBook.value = null; // Clear selected book after deletion
  } catch (err) {
    error.value = 'Failed to delete the book. Please try again.'; // Set error message on failure
  }
};


// Handle clicks outside the book list to deselect a book
const handleClickOutside = (event: MouseEvent) => {
  const bookListElement = document.querySelector('.book-list'); // Select the book list element
  if (bookListElement && !bookListElement.contains(event.target as Node)) {
    // If clicking outside the book list and not editing, deselect the book
    if (!exitingEditing.value) {
      selectedBook.value = null; // Deselect the book
      exitingEditing.value = false;
    }
  }
  exitingEditing.value = false; // Reset exiting editing flag
};

// Add event listener for clicks when the component is mounted
onMounted(() => {
  document.addEventListener('click', handleClickOutside);
});

// Remove the event listener when the component is unmounted
onUnmounted(() => {
  document.removeEventListener('click', handleClickOutside);
});
</script>



<style scoped>
/* Styling for the book list container */
.book-list {
  margin: 10px; /* Add margin around the book list */
}

/* Container for scrolling, limiting height */
.scroll-container {
  max-height: 65rem; /* Maximum height for scrolling */
  overflow-y: auto; /* Enable vertical scrolling */
  border-radius: 5px; /* Rounded corners */
}

/* Grid layout for displaying books */
.grid-container {
  display: grid; /* Enable grid layout */
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); /* Responsive grid columns */
  gap: 10px; /* Space between grid items */
  padding: 10px; /* Padding inside the grid */
  max-width: 100%; /* Allow grid to take full width */
  box-sizing: border-box; /* Include padding in width calculations */
}

/* Individual grid item styling */
.grid-item {
  cursor: pointer; /* Change cursor to pointer on hover */
  background: white; /* White background for grid items */
  position: relative; /* Allow absolute positioning of child elements */
  border-radius: 5px; /* Rounded corners for items */
  padding: 10px; /* Padding inside grid items */
  transition: box-shadow 0.3s ease; /* Smooth transition for the shadow */
  border: 2px solid transparent; /* Transparent border to prevent layout shift */
}
/* Hover effect for grid items */
.grid-item:hover {
  box-shadow: 0 0 10px 2px black; /* Glowing effect */
}

/* Styling for delete icon within grid items */
.delete-icon {
  position: absolute; /* Position icon relative to parent */
  top: 10px; /* Position from the top */
  right: -1px; /* Position from the right */
  cursor: pointer; /* Change cursor to pointer on hover */
}

/* Error message styling */
.error {
  color: red; /* Red text for errors */
  margin-top: 10px; /* Space above error message */
}

/* Styling for the selected book details */
.selected-book {
  height: 16.5rem; /* Fixed height for selected book display */
  width: 20rem; /* Fixed width for selected book display */
  position: absolute; /* Absolute positioning for placement */
  top: -14.5rem; /* Position above the scroll container */
  left: 60rem; /* Position to the right of the scroll container */
  background: white; /* White background for selected book */
  margin-top: 20px; /* Space above selected book */
  padding: 10px; /* Padding inside selected book display */
  border-radius: 5px; /* Rounded corners */
}

/* Positioning for the edit button in selected book */
.selected-book-edit-button {
  position: absolute; /* Absolute positioning within selected book */
  bottom: 0.5rem; /* Space from the bottom */
  right: 1rem; /* Space from the right */
}

/* Positioning for the delete button in selected book */
.selected-book-delete-button {
  position: absolute; /* Absolute positioning within selected book */
  bottom: 0.5rem; /* Space from the bottom */
  right: 4rem; /* Space from the right */
}

/* Positioning for update button in edit book */
.edit-book-update-button {
  position: absolute; /* Absolute positioning within edit book */
  bottom: 0.5rem; /* Space from the bottom */
  right: 1rem; /* Space from the right */
}

/* Positioning for cancel button in edit book */
.edit-book-cancel-button {
  position: absolute; /* Absolute positioning within edit book */
  bottom: 0.5rem; /* Space from the bottom */
  right: 5rem; /* Space from the right */
}

/* Styling for the search bar container */
.search-bar {
  margin-top: 3rem; /* Space above search bar */
  margin-bottom: 1em; /* Space below search bar */
}

/* Styling for the input field in search bar */
.search-bar input {
  width: 100%; /* Full width input */
  padding: 0.7rem; /* Padding inside input */
  border: 0.1rem solid #ccc; /* Light gray border */
  border-radius: 0.5rem; /* Rounded corners for input */
  font-size: 1.3rem; /* Larger font size for input text */
  transition: border-color 0.3s; /* Smooth transition for border color */
}

/* Focused state for the search input */
.search-bar input:focus {
  border-color: #7daea6; /* Change border color on focus */
  outline: none; /* Remove default outline */
}

/* Placeholder text styling in search input */
.search-bar input::placeholder {
  color: #aaa; /* Light gray color for placeholder text */
}

/* Message displayed when there are no books available */
.no-books-message {
  text-align: center; /* Center-align the message */
  margin-top: 10rem; /* Space above the message */
  color: #555; /* Gray color for the message text */
  font-size: 6rem; /* Large font size for emphasis */
}
.book-title{
  font-size: 1.5rem;
  font-weight: bold;
}
.book-title:hover{
  text-decoration: underline;
}
</style>

