<template>
  <div class="about">
    <BookList />
  </div>
</template>

<style>
  @media (min-width: 1024px) {
    .about {
      min-height: 100vh;
      display: flex;
      align-items: center;
    }
  }
</style>
<script setup lang="ts">
import BookList from '@/components/BookList.vue'
</script>
