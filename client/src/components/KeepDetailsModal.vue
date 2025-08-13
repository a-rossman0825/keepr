<script setup>
import { AppState } from '@/AppState.js';
import { Modal } from 'bootstrap';
import { computed } from 'vue';


const activeKeep = computed(()=> AppState.activeKeep);
const creator = computed(() => AppState.activeKeep.creator);

function closeModal(){
  Modal.getOrCreateInstance("#keepDetailsModal").hide();
}

</script>


<template>
  <div v-if="activeKeep" class="container-fluid">
    <div class="row">
      <div class="col-lg-6 p-0">
        <img :src="activeKeep.img" :alt="`A picture of ${activeKeep.name}, posted by ${creator.name}`" class="img-fluid keep-img" />
      </div>
      <div class="col-lg-6">
        <div class="d-flex flex-column h-100 flex-grow-1 justify-content-between">
          <div class="d-flex justify-content-center mt-4 mb-3 mb-lg-0 fs-5 align-items-center">
            <i class="mdi mdi-eye-outline pe-2"></i><span>{{ activeKeep.views }}</span>
            <i class="ms-4 me-2 fraunces-font keep-count-icon">k</i><span>{{ activeKeep.kept }}</span>
          </div>
          <div>
            <h1 class="text-center mb-3 fraunces-font fw-bold px-4">{{ activeKeep.name }}</h1>
            <p class="px-5 mb-0 pb-0 keep-desc">{{ activeKeep.description }}</p>
          </div>
          <div class="d-inline-flex align-items-center justify-content-between mb-3 mt-5 mt-lg-0">
            <div class="d-flex ms-3 ms-xl-5">
              <div class="dropdown open d-flex">
                <button
                  class=" btn dropdown-toggle open-sans-font fw-bold"
                  type="button"
                  id="categoriesBtn"
                  data-bs-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  PLANTS
                </button>
                <div class="dropdown-menu" aria-labelledby="triggerId">
                  <button class="dropdown-item" href="#">Action</button>
                  <button class="dropdown-item disabled" href="#">
                    Disabled action
                  </button>
                </div>
              </div>
              <button class="ms-3 ms-xl-4 save-btn mt-1 open-sans-font">save</button>
            </div>
            <RouterLink @click="closeModal()" :to="{ name: 'Profile', params: { profileId: `${activeKeep.creatorId}`} }">
              <div class="d-inline-flex align-items-center me-4">
                <img :src="creator?.picture" :alt="`${creator?.name}'s profile picture`" class="img-fluid profile-picture"/>
                <h5 class="mt-2 ms-2 fraunces-font profile-name">{{ creator?.name }}</h5>
              </div>
            </RouterLink>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

a {
  color: black;
}

  .keep-img {
    border-top-left-radius: 6px;
    width: 600px;
    max-height: 80dvh;
    object-fit: cover;
  }

  .keep-desc {
    text-indent: 2ch;
  }

  @media (max-width: 991px){
    .keep-img {
      border-top-right-radius: 6px;
    }
  }

  @media (min-width: 992px){
    .keep-img {
      border-top-left-radius: 6px;
      border-bottom-left-radius: 6px;
    }
  }

  .dropdown-toggle {
    font-weight: 500;
    letter-spacing: .3ch;
    border-bottom: 1px solid lightgray;
    border-radius: 0;
    padding-inline: 0;
  }

  .profile-picture {
    width: 40px;
    aspect-ratio: 1/1;
    border-radius: 50%;
    box-shadow: 0px 3px 3px rgba(112, 109, 109, 0.7);
  }

  .profile-name {
    text-shadow: 0px 3px 3px rgba(112, 109, 109, 0.7);
  }

  .save-btn {
    height: 35px;
    border: none;
    background-color: var(--bs-primary);
    color: var(--bs-light);
    border-radius: 10px;
    padding-inline: 12px;
    box-shadow: 0px 4px 4px var(--bs-secondary);
    font-weight: 500;
  }

  .keep-count-icon {
    border: 2px solid rgb(118, 116, 116);
    padding-inline: 6px;
    padding-top: 2px;
    border-radius: 8px;
    font-size: 18px;
    color: rgb(118, 116, 116);
    line-height: 23px;
  }

</style>