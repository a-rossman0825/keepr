<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';


const activeKeep = computed(()=> AppState.activeKeep);

</script>


<template>
  <div v-if="activeKeep" class="container-fluid p-0">
    <div class="row row p-0">
      <div class="col-lg-6 p-0">
        <img :src="activeKeep.img" :alt="`A picture of ${activeKeep.name}, posted by ${activeKeep.creator.name}`" class="img-fluid" />
      </div>
      <div class="col-lg-6 mt-4 ps-5 description-box">
        <div class="row align-items-center justify-content-start d-flex">
          <h1 class="main-font text-black col-12">
            {{ activeKeep.name }}
          </h1>
          <p>{{ activeKeep.description }}</p>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

</style>