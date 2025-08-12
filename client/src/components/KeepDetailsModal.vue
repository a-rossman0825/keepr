<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';


const activeKeep = computed(()=> AppState.activeKeep);
const account = computed(() => AppState.activeKeep.creator);


</script>


<template>
  <div v-if="activeKeep" class="container-fluid p-0">
    <div class="row row p-0">
      <div class="col-lg-6 p-0">
        <img :src="activeKeep.img" :alt="`A picture of ${activeKeep.name}, posted by ${activeKeep.creator.name}`" class="img-fluid keep-img" />
      </div>
      <div class="col-lg-6">
        <div class="d-flex flex-column h-100 flex-grow-1 justify-content-between">
          <div class="d-flex justify-content-center mt-3 fs-5 align-items-center">
            <i class="mdi mdi-eye pe-2"></i><span>{{ activeKeep.views }}</span>
            <i class="mdi mdi-key ms-4 pe-2"></i><span>{{ activeKeep.kept }}</span>
          </div>
          <div>
            <h1 class="text-center mb-3">Mindful</h1>
            <p class="px-5">Lorem ipsum dolor sit amet consectetur adipisicing elit. Delectus atque voluptatem culpa quae quo eius ratione ullam temporibus? Maiores, reprehenderit modi? Quos sint, fugit blanditiis porro consectetur cupiditate non cum.</p>
          </div>
          <div class="d-inline-flex align-items-center justify-content-between">
            <div class="d-flex ms-5">
              <div class="dropdown open d-flex">
                <button
                  class=" btn dropdown-toggle open-sans-font"
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
              <button class="ms-4">save</button>
            </div>
            <RouterLink :to="{ name: 'Account' }">
              <div class="d-inline-flex align-items-center me-4">
                <img :src="account?.picture" :alt="`${account?.name}'s profile picture`" class="img-fluid profile-picture"/>
                <h5 class="mt-2 ms-2">{{ account?.name }}</h5>
              </div>
            </RouterLink>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

  .keep-img {
    border-top-left-radius: 10px;
    border-bottom-left-radius: 10px;
    width: 600px;
    max-height: 80dvh;
    object-fit: cover;
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
  }

</style>