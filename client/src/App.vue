<script setup>
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';
import NavbarMobile from './components/NavbarMobile.vue';
import CreateKeepModal from './components/CreateKeepModal.vue';
import CreateModalWrapper from './components/CreateModalWrapper.vue';
import CreateChoiceModal from './components/CreateChoiceModal.vue';

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

  <!-- NOTE KEEP MODAL -->
  <CreateModalWrapper modalId="createKeepModal" modalHeader="Add your keep">
    <CreateKeepModal />
  </CreateModalWrapper>

  <!-- NOTE CHOICE MODAL -->
  <CreateModalWrapper modalId="createChoiceModal" modalHeader="What would you like to create?">
    <CreateChoiceModal />
  </CreateModalWrapper>
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
