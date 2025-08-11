<script setup >
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const keeps = computed(()=> AppState.keeps);

onMounted(()=> {
  getKeeps();
});

async function getKeeps(){
  try {
    await keepsService.getKeeps();
  }
  catch (error){
    Pop.error(error);
    logger.error('Could not get Keeps', error);
  }
}

</script>

<template>
  <div class="container">
    <div class="masonry-wrapper">
      <div v-for="keep in keeps" :key="'keeps-feed-id-' + keep.id" class="keep-card">
      <KeepCard :keep />
    </div>
    </div>
  </div>
</template>

<style scoped lang="scss">

.masonry-wrapper {
  column-gap: 1.5rem;
  column-count: 2;
}

@media (min-width: 768px) {
  .masonry-wrapper {
    column-count: 4;
  }
  .keep-card {
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
  box-shadow: 1px 2px 4px rgba(156, 156, 156, 0.537);
}


</style>
