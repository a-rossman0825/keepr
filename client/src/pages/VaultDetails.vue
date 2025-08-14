<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, onUnmounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();

onMounted(()=> {
  getVaultById();
  getKeeps();
})

onUnmounted(()=> {
  AppState.activeVault = null;
  AppState.keeps = [];
})

const vault = computed(()=> AppState.activeVault);
const keeps = computed(()=> AppState.keeps);
const account = computed(()=> AppState.account);


async function getVaultById(){
  try {
    await vaultsService.getVaultById(route.params.vaultId);
  }
  catch (error){
    Pop.error(error);
    router.push('/');
  }
}
async function getKeeps(){
  try {
    await vaultsService.getKeepsForVault(route.params.vaultId);
  } catch (error){
    Pop.error(error);
    logger.error('could not get vaultKeeps!', error);
  }
}

async function deleteVault(){
  const confirmed = await Pop.confirm('Are You Sure You Want To Delete This Vault?');
  if (!confirmed) return;
  try {
    await vaultsService.DeleteVault(vault.value.id);
    router.push(`/profile/${account.value.id}`);
  }
  catch (error){
    Pop.error(error);
    logger.error('Could not delete vault');
  }
}

const isPrivateUpdating = ref(false);

async function toggleIsPrivate(){
  if (isPrivateUpdating.value) return;
  isPrivateUpdating.value = true;

  try {
    await vaultsService.updateVault(vault.value.id, { isPrivate: !vault.value.isPrivate });
    isPrivateUpdating.value = false;
  }
  catch (error){
    Pop.error(error);
    logger.error('Could not toggle isPrivate on vault!', error);
    isPrivateUpdating.value = false;
  }
}

</script>


<template>
  <div v-if="vault" class="container-fluid">
    <div class="d-flex justify-content-center">  
      <div class="row text-center justify-content-center mt-3 ">
        <div class="align-items-center d-flex text-center justify-content-center bg-cover" :style="`background-image: url('${ vault.img }')`">
          <div class="row text-light hero-text fraunces-font">
            <h1 class="">{{ vault.name.toUpperCase() }}</h1>
            <h2>by {{ vault.creator.name.toLowerCase() }}</h2>
          </div>
        </div>
        <div v-if="vault.creatorId == account?.id" class="btn-wrapper d-flex justify-content-end">
          <h1 class="dropdown-btn mdi mdi-dots-horizontal fs-2 me-3 mt-0" title="Edit Profile"
          data-bs-toggle="dropdown"
          aria-expanded="false"></h1>
          <div class="dropdown-menu border-black">
            <li @click="deleteVault()" class="dropdown-item">Delete Vault</li>
          </div>
        </div>
        <div class="d-flex justify-content-center mt-3">
          <h3 class="fs-5 bg-secondary py-2 px-2 keeps-count">{{ keeps.length }} Keep{{ keeps.length == 1 ? '' : 's' }}</h3>
          <div v-if="vault.creatorId == account.id">
            <i @click="toggleIsPrivate()" class="mdi locked-btn fs-4 ms-1" :class="vault.isPrivate ? 'mdi-lock' : 'mdi-lock-open-outline'"></i>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-start px-5">
      <div class="container px-5 mt-3">
        <div class="masonry-wrapper">
          <div v-for="keep in keeps" :key="'vault-keeps-id-' + keep?.id" class="keep-card position-relative" :title="`${keep.name} by ${keep.creator.name}`">
            <KeepCard :keep />
          </div>
        </div>
      </div>
    </div>
    
  </div>
</template>


<style lang="scss" scoped>


.bg-cover {
  height: 200px;
  width: 600px;
  
  border-radius: 10px;

}

.locked-btn {
  opacity: 40%;
  transition: all .2s ease-in-out;
  &:hover {
    opacity: 100%;
    cursor: pointer;
  }
}

.hero-text {
  text-shadow: 4px 6px 5px black;
  letter-spacing: .4rem;
  width:260px;
}

.keeps-count {
  border-radius: 12px;
}

.btn-wrapper {
  width: 650px;
}

.masonry-wrapper {
  column-gap: 1.5rem;
  column-count: 2;
}

@media (min-width: 992px){
  .masonry-wrapper{
    column-count: 4;
  }
  .keep-card {
    break-inside: avoid;
    display: inline-block;
    margin-bottom: .8rem;
  }
}

@media (min-width: 768px) and (max-width: 992px) {
  .masonry-wrapper {
    column-count: 3;
  }
  .keep-card {
    break-inside: avoid;
    display: inline-block;
    margin-bottom: .8rem;
  }
}

@media (max-width: 768px){
  .keep-card {
    margin-bottom: 1.5rem;
  }
}

@media (min-width: 575px) and (max-width: 768px){
  .masonry-wrapper{
    column-count: 2;
  }
}

.keep-card {
  break-inside: avoid;
  border-radius: 10px;
  box-shadow: 4px 4px 7px rgba(115, 115, 115, 0.537);

  transition: all .3s ease-in-out;

  &:hover {
    box-shadow: 5px 5px 9px rgba(46, 45, 45, 0.537);
    transform: translateY(-3px);
    filter: contrast(130%);
  }
}

.dropdown-menu {
  padding: .1rem 0;
  --bs-dropdown-min-width: 0;
  background-color: var(--bs-secondary);

  &:hover {
    background-color: var(--bs-secondary);
    color: var(--bs-primary);
  }
}

.dropdown-item {
  transition: all .2s ease-in-out;

  &:hover {
    border-radius: 2px;
    background-color: var(--bs-secondary);
    color: var(--bs-primary);
    cursor: pointer;
  }
}

.dropdown-btn {
  border: none;
}

.mdi-dots-horizontal {
  opacity: .65;
  transition: all 0.3s ease;

  &:hover {
    opacity: 1;
    cursor: pointer;
  }
}

</style>