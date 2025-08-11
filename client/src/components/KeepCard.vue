<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import { Modal } from 'bootstrap';



  const prop = defineProps({
    keep: { type: Keep, required: true },
  });

  function openKeepModal() {
    AppState.activeKeep = prop.keep;
    Modal.getOrCreateInstance('#keepDetailsModal').show();
  }

</script>


<template>
  <!-- TODO NO DEAD URLS!!!! -->
  <div class="keep-card">
    <img @click="openKeepModal()" :src="keep.img" :alt="`${keep.creator.name}'s keep of ${keep.name}`" class="keep-img img-fluid"/>
    <div class="keep-content d-flex align-items-between">
      <h5 class="fraunces-font keep-title mb-0 text-truncate">{{ keep.name }}</h5>
      <img
        :src="prop.keep.creator?.picture"
        :alt="prop.keep.creator?.name"
        :title="prop.keep.creator?.name"
        class="creator-profile-img d-none d-lg-inline-block me-2" />
    </div>
  </div>
</template>


<style lang="scss" scoped>

.keep-card {
  border-radius: 10px;
  overflow: hidden;

  &::after {
    content: "";
    position: absolute;
    inset: 0;
    background: linear-gradient(
      to bottom,
      rgba(0,0,0,.1) 70%,
      rgba(0,0,0,.15) 75%,
      rgba(0,0,0,.85) 100%,
    );
    z-index: 1;
    pointer-events: none;
    border-radius: 10px;
  }
}

.keep-img {
  display: block;
  width: 100%;
  height: auto;
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
}

h5 {
  font-size: 2.5ch;
  color: var(--bs-light);
  z-index: 2;
  text-shadow: 1px 1px 3px rgba(124, 121, 121, 0.67);
}



</style>