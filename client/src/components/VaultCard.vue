<script setup>
import { AppState } from '@/AppState.js';
import { Vault } from '@/models/Vault.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';


const props = defineProps({
  vault: { type: Vault, required: true },
});

const account = computed(()=> AppState.account);

const isPrivateUpdating = ref(false);

async function toggleIsPrivate(){
  if (isPrivateUpdating.value) return;
  isPrivateUpdating.value = true;

  try {
    await vaultsService.updateVault(props.vault.id, { isPrivate: !props.vault.isPrivate });
    isPrivateUpdating.value = false;
  }
  catch (error){
    Pop.error(error);
    logger.error('Could not toggle isPrivate on vault!', error);
    isPrivateUpdating.value = false;
  }
}

</script>


<template>
  
  <div class="vault-card-wrapper position-relative" :title="`${props.vault.name} by ${props.vault.creator.name}`">
      <RouterLink :to="{ name: 'Vault Details Page', params: { vaultId: `${props.vault.id}` } }">
        <img :src="props.vault.img" :alt="`${props.vault.creator.name}'s vault cover image`" class="img-fluid vault-img">
        <div class="vault-content d-flex align-items-end justify-content-between py-3 position-absolute cursor-pointer">
          <h4 class="open-sans-font vault-title mb-0 text-truncate fs-4 text-uppercase">{{ props.vault.name }}</h4>
          <div v-if="props.vault.creatorId == account?.id">
            <button class="btn">
              <i @click.stop.prevent="toggleIsPrivate()" class="mdi text-light fs-1 z-3" :class="[vault.isPrivate ? 'mdi-lock' : 'mdi-lock-open', { 'opacity-50 pointer-events-none': isPrivateUpdating }]" :title="vault.isPrivate ? 'Unlock Vault' : 'Lock Vault'"></i>
            </button>
          </div>
        </div>
      </RouterLink>
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

  h4 {
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