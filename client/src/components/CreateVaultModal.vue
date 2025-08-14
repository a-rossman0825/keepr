<script setup>
import { AppState } from '@/AppState.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref, watch } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const vaultData = ref({
  name: '',
  img: '',
  isPrivate: false,
  description: 'No description'
});

async function createVault(){
  try {
    await vaultsService.createVault(vaultData.value, route.params.profileId);
    clearForm();
    Pop.success("Vault Created!");
    Modal.getOrCreateInstance("#createVaultModal").hide();
  }
  catch (error){
    Pop.error(error);
    logger.error('Could not create Vault', error);
  }
};

watch(()=> vaultData.value.img, changeImg);


function changeImg(){
  AppState.imgHasChanged = vaultData.value.img.includes('http');
};


function clearForm(){
  vaultData.value = {
    name: '',
    img: '',
    description: 'No description',
    isPrivate: false,
  }
};
</script>


<template>
<section class="container-fluid">
  <form @submit.prevent="createVault()" class="row">
      <div v-if="vaultData.img" class="mb-5 col-12 col-lg-6">
        <small class="m-3">Image Preview</small>
        <img :src="vaultData.img" alt="Your Image" class="img-fluid photo-preview" />
      </div>
      <div @change="changeImg()" :class="vaultData.img ? 'col-12 col-md-6' : 'col-12'">
        <div class="form-floating mb-3">
          <input
            type="text"
            class="form-control fs-5"
            name="title"
            id="title"
            aria-describedby="helpId"
            placeholder="Title..."
            v-model="vaultData.name"
            minlength="1"
            maxlength="50"
            required
          />
          <label for="title" class="form-label">Title...</label>
        </div>
        <div class="form-floating mb-5 pb-5">
          <input
            type="url"
            class="form-control fs-5"
            name="imgUrl"
            id="imgUrl"
            aria-describedby="helpId"
            placeholder="Image URL..."
            v-model="vaultData.img"
            minlength="1"
            maxlength="500"
            required
          />
          <label for="imgUrl" class="form-label">Image URL...</label>
        </div>
        <div class="d-block-flex text-end pb-0">
          <small>Private Vaults can only be seen by you</small>
          <div class="form-check justify-content-end d-flex pb-0">
            <!-- FIXME you need v-model -->
            <input class="form-check-input pb-0" type="checkbox" value="true" id="isPrivate">
            <label class="form-check-label ms-2 pb-0" for="isPrivate">
              Make Vault Private?
            </label>
          </div>
        </div>
        <div class="d-flex justify-content-end">
          <button type="submit" class="btn open-sans-font">Create</button>
        </div>
      </div>
    </form>
</section>
</template>


<style lang="scss" scoped>
  .form-control {
  border: none;
  border-bottom: 2px solid lightgray;
  border-radius: 2%;
  background-color: var(--bs-body-bg) !important;

  &:focus {
    background-color: var(--bs-body-bg) !important;
    border-color: none !important;
    box-shadow: none !important;
  }
}

label {
  font-size: .95rem !important;
  font-weight: 600;
}

input {
  padding-bottom: 0;
  margin-bottom: 0;
  background-color: var(--bs-light);
}

button {
  margin-top: 30px;
  background-color: black;
  color: var(--bs-light);
  padding-block: 2px;
}

.photo-preview {
  object-fit: cover;
  height: 275px;
  width: 80%;
  border-radius: 10px;
}
</style>