<script setup>
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';
import NavbarMobile from './components/NavbarMobile.vue';
import CreateKeepModal from './components/CreateKeepModal.vue';
import CreateModalWrapper from './components/CreateModalWrapper.vue';
import CreateChoiceModal from './components/CreateChoiceModal.vue';
import KeepDetailsModal from './components/KeepDetailsModal.vue';
import DetailsModalWrapper from './components/DetailsModalWrapper.vue';
import { computed, watch } from 'vue';
import { Pop } from './utils/Pop.js';
import { logger } from './utils/Logger.js';
import { vaultsService } from './services/VaultsService.js';
import { AppState } from './AppState.js';
import CreateVaultModal from './components/CreateVaultModal.vue';

const account = computed(()=> AppState.account);

watch(()=> account.value?.id, 
  async (id)=> {
    if (!id) return
    await getUserVaults()
    stop()
  }, { immediate: true })

  async function getUserVaults(){
  try {
    await vaultsService.getVaultsByProfileId(account.value?.id, true);
  }
  catch (error){
    Pop.error(error);
    logger.error('could not get user Vaults!', error);
  }
}
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

  <!-- NOTE CREATE KEEP MODAL -->
  <CreateModalWrapper modalId="createKeepModal" modalHeader="Add your keep">
    <CreateKeepModal />
  </CreateModalWrapper>

  <CreateModalWrapper modalId="createVaultModal" modalHeader="Add your vault">
    <CreateVaultModal />
  </CreateModalWrapper>

  <!-- NOTE CHOICE MODAL -->
  <CreateModalWrapper modalId="createChoiceModal" modalHeader="What would you like to create?">
    <CreateChoiceModal />
  </CreateModalWrapper>

  <!-- NOTE KEEP DETAILS MODAL -->
  <DetailsModalWrapper modalId="keepDetailsModal">
    <KeepDetailsModal />
  </DetailsModalWrapper>
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
