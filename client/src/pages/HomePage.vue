<script setup >
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const keeps = computed(()=> AppState.keeps);

const account = computed(()=> AppState.account);

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

</script>

<template>
  <div class="container">
    <div class="masonry-wrapper">
      <div v-for="keep in keeps" :key="'keeps-feed-id-' + keep.id" class="keep-card position-relative">
        <KeepCard :keep/>
        <i v-if="keep?.creatorId == account?.id" @click="deleteKeep(keep?.id)" role="button" class="mdi mdi-close-circle" title="Delete This Keep"></i>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">

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
</style>
