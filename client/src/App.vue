<script setup>
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';
import NavbarMobile from './components/NavbarMobile.vue';

</script>

<template>
  <header class="sticky-top bg-light">
    <Navbar />
  </header>
  <main class="mb-5 mt-3">
    <RouterView/>
  </main>
  <footer class="mt-5">
    <NavbarMobile class="bg-light" />
  </footer>
</template>

<style lang="scss">


@media (max-width: 769px){
  header {
    display: none;
  }
}

@media (min-width: 769px){
  footer {
    display: none;
  }
}

</style>
