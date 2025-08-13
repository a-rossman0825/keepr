<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import { AuthService } from '@/services/AuthService.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, watch } from 'vue';
import { useRoute } from 'vue-router';


const route = useRoute();

// onMounted(()=> {
//   getProfileById();
//   getKeepsByProfileId();
// });

const profile = computed(()=> AppState.activeProfile);
const account = computed(() => AppState.account);
const keeps = computed(() => AppState.keeps);
const vaults = computed(() => AppState.vaults);


//NOTE if profileId change >> run functions, also run onMount (immediate: true)
watch(() => route.params.profileId, ()=> {
  getProfileById();
  getKeepsByProfileId();
  getVaultsByProfileId();
}, {immediate:true});

async function getProfileById(){
  const profileId = route.params.profileId;
  try {
    await accountService.getProfileById(profileId);
  }
  catch (error){
    Pop.error(error);
    logger.log('Could not get profile!', error);
  }
}

async function getKeepsByProfileId(){
  const profileId = route.params.profileId;
  try {
    await keepsService.getKeepsByProfileId(profileId);
  }
  catch (error){
    Pop.error(error);
  }
}

async function deleteKeep(keepId){
  
  const confirmed = await Pop.confirm('Are you sure you want to permanently delete this keep?');

  if (!confirmed) return;

  try {
    keepsService.deleteKeep(keepId);
  }
  catch (error){
    Pop.error(error);
  }
}

async function getVaultsByProfileId(){
  const profileId = route.params.profileId;
  try {
    await vaultsService.getVaultsByProfileId(profileId);
  }
  catch (error){
    Pop.error(error);
  }
}



function logout() {
  AuthService.logout()
}

function jumpLink(locationId) {
  const target = document.getElementById(locationId);
  target.scrollIntoView({
    behavior: 'smooth',
    block: 'start'
  });
}

</script>


<template>
  <div v-if="profile" class="container">
    <div class="row justify-content-center display-1 px-5">
      <div class="col-12 p-0 p-md-4 col-md-9 col-lg-8">
        <div class="position-relative">
          <img :src="profile.coverImg" :alt="`${profile.name}'s cover image`" class="img-fluid cover">
          <img :src="profile.picture" :alt="`${profile.name}'s profile picture`" class="img-fluid profile-picture" />
        </div>
        <div v-if="profile.id == account?.id" class="d-flex justify-content-end">
          <h1 class="mdi mdi-dots-horizontal fs-2 me-3 mt-0" title="Edit Profile"></h1>
        </div>
        <div class="row text-center hero-wrapper">
          <h1 class="hero-text open-sans-font">{{ profile.name }}</h1>
          <div class="justify-content-center gap-2 d-inline-flex ms-2">
            
            <h2 @click="jumpLink('vault-jump-link')" class="fs-5 fw-light jump-link">{{ vaults.length }} Vault{{ vaults.length == 1 ? "" : "s" }}</h2>
            <h2 class="fs-5 fw-light">|</h2>
            <h2 id="vault-jump-link" @click="jumpLink('keep-jump-link')" class="fs-5 fw-light jump-link">{{ keeps.length }} Keep{{ keeps.length == 1 ? "" : "s" }}</h2>
          </div>
        </div>
      </div>
    </div>
    <div class="mt-5 row justify-content-start display-3 px-5">
      <h3 class="hero-text">Vaults</h3>
    </div>
    <div class="row px-5">
      <div v-for="vault in vaults" :key="'profile-vaults-id-' + vault.id" class="col-6 col-md-4 col-lg-3">
        <VaultCard :vault class="vault-card" />
      </div>
      <div id="keep-jump-link" class="mt-5 pt-5"></div>
    </div>
    <div class="mt-5 row justify-content-start display-3 px-5">
      <h4 class="hero-text">Keeps</h4>
    </div>
    <div class="row justify-content-start px-5">
      <div class="container">
        <div class="masonry-wrapper">
          <div v-for="keep in keeps" :key="'profile-keeps-id-' + keep.id" class="keep-card position-relative">
            <KeepCard :keep />
            <i v-if="keep.creatorId == account.id" @click="deleteKeep(keep?.id)" role="button" class="mdi mdi-close-circle" title="Delete This Keep"></i>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-else class="text-center">
    <h1 class="display-1 text-primary">LOADING.... <i class="mdi mdi-image mdi-spin display-1 text-secondary"></i></h1>
  </div>
  <button @click="logout()" class="mt-5">logout</button>
</template>


<style lang="scss" scoped>

.cover {
  height: 280px;
  width: 900px;
  object-fit: cover;
  box-shadow: 0px 2px 6px rgba(94, 92, 92, 0.678);
}

.profile-picture {
  border-radius: 50%;
  border: 1px solid var(--bs-light);
  box-shadow: 3px 6px 8px rgba(94, 92, 92, 0.678);
  width: 100px;
  aspect-ratio: 1/1;
  object-fit: cover;
  position: absolute;
  bottom: -20%;
  left: 50%;
  transform: translateX(-50%);
}

.hero-wrapper {
  margin-top: 75px;
}

.hero-text {
  font-weight: 580;
  font-size: 1.2ch;
}

@media (min-width: 576px){
  .cover {

    border-radius: 10px;
  }
}

.mdi-dots-horizontal {
  opacity: .65;
  transition: all 0.3s ease;

  &:hover {
    opacity: 1;
  }
}


.masonry-wrapper {
  column-gap: 1.5rem;
  column-count: 2;
}

.mdi-close-circle {
  color: var(--bs-danger);
  position: absolute;
  top: -10px;
  right: -7px;
  z-index: 10;
  transition: all .3s ease-in-out;
  &:hover {
    color: rgb(250, 0, 0);
  }
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

    .mdi-close-circle{
      opacity: .6;
    }
  }
  .mdi-close-circle {
    opacity: 0;
  }
}

.vault-card {
  break-inside: avoid;
  border-radius: 10px;
  box-shadow: 4px 4px 7px rgba(115, 115, 115, 0.537);

  transition: all .3s ease-in-out;

  &:hover {
    box-shadow: 5px 5px 9px rgba(46, 45, 45, 0.537);
    transform: translateY(-3px);
    filter: contrast(130%);

    .mdi-close-circle{
      opacity: .6;
    }
  }
  .mdi-close-circle {
    opacity: 0;
  }
}

.jump-link {

  transition: all .1s ease-in;

  &:hover {
    opacity: 0.8;
    color: var(--bs-primary);
    cursor: pointer;
    font-weight: 400 !important;
  }
}

</style>