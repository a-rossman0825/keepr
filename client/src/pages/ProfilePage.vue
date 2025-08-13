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
import { computed, ref, watch } from 'vue';
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
    logger.error('Could not update profile!', error);
  }
}

let editableAccountData = ref({
  name: '',
  coverImg: '',
  picture: '',
});

let isEditing = ref(false);

async function editProfile(){
  try {
    const editData = {
    name: editableAccountData.value.name ? editableAccountData.value.name : account.value.name,
    coverImg: editableAccountData.value.coverImg ? editableAccountData.value.coverImg : account.value.coverImg,
    picture: editableAccountData.value.picture ? editableAccountData.value.picture : account.value.picture
    }
    await accountService.editProfile(editData);
    editableAccountData.value = {
      name: '',
      coverImg: '',
      picture: '',
    };
    isEditing.value = false;
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

function toggleEdit(bool){
  isEditing.value = bool;

  if (nameEdit.value == true) nameEdit.value = false;
  if (pfpEdit.value == true) pfpEdit.value = false;
  if (coverImgEdit.value == true) coverImgEdit.value = false;
}

let nameEdit = ref(false);
function toggleNameEdit(){
nameEdit.value = !nameEdit.value;
const input = document.getElementById('name-input');
setTimeout(() => {
  input.focus();
}, 30);
}

let pfpEdit = ref(false);
function togglePfpEdit(){
  pfpEdit.value = !pfpEdit.value;
  const input = document.getElementById('pfp-input');
  setTimeout(() => {
    input.focus();
  }, 30);
}

let coverImgEdit = ref(false);
function toggleCIEdit(){
  coverImgEdit.value = !coverImgEdit.value;
  const input = document.getElementById('ci-input');
  setTimeout(() => {
    input.focus();
  }, 30);
}

</script>


<template>
  <div v-if="profile" class="container">
    <div class="row justify-content-center display-1 px-5">
      <div v-if="!isEditing" class="p-0 p-md-4 col-md-9 col-lg-8">
        <div class="position-relative" :class="profile.id == account?.id ? '' : 'mb-5'">
          <img :src="profile.coverImg" :alt="profile.name ? `${profile.name}'s cover image` : `Add a coverImg!`" class="img-fluid cover">
          <img :src="profile.picture" :alt="profile.name ? `${profile.name}'s profile picture` : `Add a Profile Picture!`" class="img-fluid profile-picture" />
        </div>
        <div v-if="profile.id == account?.id" class="d-flex justify-content-end">
          <h1 class="dropdown-btn mdi mdi-dots-horizontal fs-2 me-3 mt-0" title="Edit Profile"
          data-bs-toggle="dropdown"
          aria-expanded="false"></h1>
          
          <div class="dropdown-menu border-black">
            <li @click="toggleEdit(true)" class="dropdown-item border-bottom border-black">edit profile</li>
            <li @click="logout()" class="dropdown-item">logout</li>
          </div>
        </div>
        <div class="row text-center hero-wrapper">
          <h1 class="hero-text open-sans-font" :class="profile.id == account?.id ? '' : 'mt-4'">{{ profile.name }}</h1>
          <div class="justify-content-center gap-2 d-inline-flex ms-2">
            <h2 @click="jumpLink('vault-jump-link')" class="fs-5 fw-light jump-link">{{ vaults.length }} Vault{{ vaults.length == 1 ? "" : "s" }}</h2>
            <h2 class="fs-5 fw-light">|</h2>
            <h2 id="vault-jump-link" @click="jumpLink('keep-jump-link')" class="fs-5 fw-light jump-link">{{ keeps.length }} Keep{{ keeps.length == 1 ? "" : "s" }}</h2>
          </div>
        </div>
      </div>
      <div v-else class="p-0 p-md-4 col-md-9 col-lg-8">
        <form @submit.prevent="editProfile()">
          <div class="position-relative">
            <img :src="editableAccountData.coverImg && editableAccountData.coverImg.includes('http') || account.coverImg ? editableAccountData.coverImg || account.coverImg : 'https://c4.wallpaperflare.com/wallpaper/940/744/14/star-wars-bliss-atat-1920x1080-video-games-star-wars-hd-art-wallpaper-preview.jpg'" alt="Add a coverImg!" class="img-fluid cover">
            <i @click="toggleCIEdit()" class="mdi mdi-image-plus fs-2 text-secondary position-absolute coverImg-icon" title="Upload a cover image" :class="coverImgEdit ? 'd-none' : ''"><span class=" fs-5 edit-txt"> edit cover Image</span></i>
            <input v-model="editableAccountData.coverImg" id="ci-input" class="position-absolute form-control w-25 coverImg-input"  placeholder="Cover Image Url..." :class="coverImgEdit ? '' : 'd-none'" />
            <img @click="togglePfpEdit()" role="button" :src="editableAccountData.picture && editableAccountData.picture.includes('http') || account.picture ? editableAccountData.picture || account.picture : 'https://static.wixstatic.com/media/3134dd_ff2e967f1ebc44cd8edcb7dcc77f0610~mv2.png/v1/fill/w_980,h_653,al_c,q_90,usm_0.66_1.00_0.01,enc_avif,quality_auto/HEYA%20Banners%20(3).png'" alt="Add a Profile Picture!" class="img-fluid " title="upload a profile picture" :class="editProfile ? 'edit-pfp' : 'profile-picture'" />
            <input v-model="editableAccountData.picture" id="pfp-input" class="form-control mt-2 w-25 position-absolute pfp-input" placeholder="Picture Url..." :class="pfpEdit ? '' : 'd-none'">
          </div>
          <div class="d-flex justify-content-end">
            <button type="submit" class="btn mdi mdi-check fs-2 me-0 pe-0 mt-0 pt-0" title="Accept Profile Changes"></button>
            <button @click="toggleEdit(false)" type="button" class="btn mdi mdi-close fs-2 me-3 mt-0 pt-0" title="Stop Editing Profile"></button>
          </div>
          <div class="justify-content-center text-center position-relative">
            <div @click="toggleNameEdit()" role="button" class="hero-text open-sans-font text-secondary" :title="editProfile ? 'edit your username' : ''" :class="editProfile ? 'edit-hero-text' : 'hero-text'">{{ editableAccountData.name ? editableAccountData.name : account.name }}</div>
            <input v-model="editableAccountData.name" id="name-input" name="name" class="form-control mb-3 w-50 d-inline mt-0" placeholder="enter your new username..." aria-label="enter your new username" :class="nameEdit ? '' : 'd-none' "/>
          </div>
          <div class="justify-content-center gap-2 d-flex ms-2 text-center">
            <h2 @click="jumpLink('vault-jump-link')" class="fs-5 fw-light jump-link">{{ vaults.length }} Vault{{ vaults.length == 1 ? "" : "s" }}</h2>
            <h2 class="fs-5 fw-light">|</h2>
            <h2 id="vault-jump-link" @click="jumpLink('keep-jump-link')" class="fs-5 fw-light jump-link">{{ keeps.length }} Keep{{ keeps.length == 1 ? "" : "s" }}</h2>
          </div>
        </form>
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
          <div v-for="keep in keeps" :key="'profile-keeps-id-' + keep.id" class="keep-card position-relative" :title="`${keep.name} by ${keep.creator.name}`">
            <KeepCard :keep />
            <i v-if="keep.creatorId == account?.id" @click="deleteKeep(keep?.id)" role="button" class="mdi mdi-close-circle" title="Delete This Keep"></i>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-else class="text-center">
    <h1 class="display-1 text-primary">LOADING.... <i class="mdi mdi-image mdi-spin display-1 text-secondary"></i></h1>
  </div>
</template>


<style lang="scss" scoped>

.cover {
  height: 280px;
  width: 900px;
  object-fit: cover;
  box-shadow: 0px 2px 6px rgba(94, 92, 92, 0.678);
  border-radius: 10px;
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

.edit-pfp {
  border: 4px solid var(--bs-secondary);
  box-shadow: 3px 6px 8px rgba(94, 92, 92, 0.678);
  border-radius: 50%;
  width: 100px;
  aspect-ratio: 1/1;
  object-fit: cover;
  position: absolute;
  bottom: -20%;
  left: 50%;
  transform: translateX(-50%);
  transition: all .2s ease-in-out;

  &:hover {

    bottom: -19%;
    box-shadow: 3px 6px 5px var(--bs-warning);
  }
}

.hero-wrapper {
  margin-top: 20px;

  
}

.hero-text {
  font-weight: 580;
  font-size: 40px;
}

.edit-hero-text {
  text-shadow: 3px 6px 8px rgba(120, 120, 118, 0.678);
  transition: all .2s ease-in-out;

  &:hover {
  transform: translateY(-4px);
  text-shadow: 3px 6px 5px var(--bs-warning);
  }
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
    cursor: pointer;
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


.masonry-wrapper {
  column-gap: 1.5rem;
  column-count: 2;
}

.mdi-close {
  color: rgba(221, 67, 67, 0.554);
  transition: all .25s ease-in-out;
  text-shadow: 2px 0px 2px rgb(88, 88, 88, 0.5);

  &:hover {
    color: red;
    cursor: pointer;
  }
}

.mdi-check {
  color: rgba(0, 128, 0, 0.364);
  text-shadow: 2px 0px 2px rgb(88, 88, 88, 0.5);
  transition: all .25s ease-in-out;

  &:hover {
    color: green;
    cursor: pointer;
  }
}

.coverImg-icon {
  top: -18px;
  left: -10px;
  text-shadow: 2px 0 1px rgb(187, 169, 169);
  transition: all .2s ease-in-out;

  &:hover {
    top: -20px;
    left: -12px;
    text-shadow: 4px 0 3px var(--bs-warning);
    cursor: pointer;

    .edit-txt {
      opacity: .8;
      text-shadow: 3px 2px 3px rgb(76, 75, 75);
    }
  }
}

.pfp-input {
  right: 130px;
}

.coverImg-input {
  top: 5px;
  left: 5px;
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

.edit-txt {
  color: white;
  text-shadow: 1px 0px 1px rgb(88, 86, 86);
  opacity: 0;
  transition: all .2s ease-in-out;
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