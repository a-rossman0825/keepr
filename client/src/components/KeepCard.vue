<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed } from 'vue';
import { RouterLink } from 'vue-router';



  const prop = defineProps({
    keep: { type: Keep, required: true },
  });

  const account = computed(()=> AppState.account)

  async function openKeepModal() {
    AppState.activeKeep = prop.keep;

    try {
      if (prop.keep.creatorId != account.value?.id){
      await keepsService.getKeepById(prop.keep.id);
      Modal.getOrCreateInstance('#keepDetailsModal').show();
      } else {
        Modal.getOrCreateInstance('#keepDetailsModal').show();
      }
    }
    catch (error){
      Pop.error(error);
      logger.log('Could not update keep view count', error);
    }
    
  }

</script>


<template>
  <!-- TODO NO DEAD URLS!!!! -->
  <div v-if="keep" class="keep-card-wrapper">
    <img @click="openKeepModal()" :src="keep.img" :alt="`${keep.creator.name}'s keep of ${keep.name}`" class="keep-img img-fluid"/>
    <div class="keep-content d-flex justify-content-between">
      <h5 class="fraunces-font keep-title mb-0 text-truncate">{{ keep.name }}</h5>
      <RouterLink :to="{ name: 'Profile',  params: {profileId: `${prop.keep.creatorId}` } }">
        <img
        :src="prop.keep.creator?.picture"
        :alt="prop.keep.creator?.name"
        :title="prop.keep.creator?.name"
        class="creator-profile-img d-none d-lg-inline-block me-2" />
      </RouterLink>
    </div>
  </div>
</template>


<style lang="scss" scoped>

.keep-card-wrapper {
  border-radius: 10px;
  overflow: hidden;

  &::after {
    content: "";
    position: absolute;
    inset: 0;
    background: linear-gradient(
      to bottom,
      rgba(0,0,0,0.03) 45%,
      rgba(0,0,0,.2) 65%,
      rgba(0,0,0,.5) 100%,
    );
    z-index: 1;
    pointer-events: none;
    border-radius: 10px;

  }
}

.keep-img {
  display: block;
  width: 100%;
  border-radius: 10px;
  z-index: 0;

  &:hover {
    cursor: pointer;
  }
}

.keep-content {
  position: absolute;
  left: 10px;
  right: 10px;
  bottom: 8px;
  z-index: 2;
  justify-content: space-between;
  align-items: center;
}

.creator-profile-img {
  width: 40px;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
  box-shadow: 0 1px 2px rgba(0,0,0,.35);
  transition: 
  box-shadow .4s ease-in-out,
  width .2s ease-in-out;

  &:hover {
    cursor: pointer;
    width: 41px;
    box-shadow: 0px 0px 20px whitesmoke;
  }
}

h5 {
  margin-inline-start: 10px;
  font-size: 150%;
  font-weight: 100 !important;
  letter-spacing: .1ch;
  color: var(--bs-light);
  z-index: 2;
  text-shadow: 1px 1px 3px rgba(124, 121, 121, 0.67);
}



</style>