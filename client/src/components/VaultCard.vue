<script setup>
import { AppState } from '@/AppState.js';
import { Vault } from '@/models/Vault.js';
import { computed } from 'vue';


const prop = defineProps({
  vault: { type: Vault, required: true },
});

const account = computed(()=> AppState.account);

async function toggleIsPrivate(){
  const vaultId = prop.vault.id;

}

</script>


<template>
    <div class="vault-card-wrapper position-relative" :title="`${prop.vault.name} by ${prop.vault.creator.name}`">
      <img :src="prop.vault.img" :alt="`${prop.vault.creator.name}'s vault cover image`" class="img-fluid vault-img">
      <div class="vault-content d-flex align-items-end justify-content-between py-3 position-absolute cursor-pointer">
        <h5 class="open-sans-font vault-title mb-0 text-truncate fs-4 text-uppercase">{{ prop.vault.name }}</h5>
        <div v-if="prop.vault.creatorId == account?.id">
          <i v-if="prop.vault.isPrivate" class="mdi mdi-shield-lock text-light fs-1" title="unlock vault"></i>
          <i v-else class="mdi mdi-shield-lock-open text-light fs-1" title="lock vault"></i>
        </div>
      </div>
    </div>
</template>


<style lang="scss" scoped>

  a {
    text-decoration: none;
  }

  i {
    opacity: .6;
    transition: all .3s ease-in-out;
    &:hover {
      opacity: 1;
      text-shadow: 0px 0px 20px rgb(236, 236, 225);
    }
  }
  
  .vault-card-wrapper {
    border-radius: 10px;
    overflow: hidden;
    margin-bottom: 10px;


    &::after {
      content: "";
      position: absolute;
      inset: 0;
      background: linear-gradient(
        to bottom,
          rgba(0,0,0,0.03) 45%,
          rgba(0,0,0,0.2) 65%,
          rgba(0,0,0,0.5) 100%,
      );
      z-index: 1;
      pointer-events: none;
    }
  }

  .vault-img {
    display: block;
    width: 100%;
    object-fit: cover;
    border-radius: 10px;
    z-index: 0;

    &:hover {
    cursor: pointer;
    }
  }

  .vault-content {
    position: absolute;
    left: 10px;
    right: 10px;
    bottom: 0px;
    z-index: 2;
    justify-content: space-between;
    align-items: center;
  }

  h5 {
    margin-inline-start: 10px;
    font-weight: 400 !important;
    letter-spacing: .4ch;
    color: var(--bs-light);
    z-index: 2;
    text-shadow: 1px 1px 3px rgba(124, 121, 121, 0.67);
  }

  .mdi-shield-lock-open {
    opacity: 40%;
  }

  .mdi-shield-lock {
    opacity: 85%;
  }

  .cursor-pointer {
    
    &:hover {
      cursor: pointer;
    }
  }

</style>