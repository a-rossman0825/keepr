<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref, watch } from 'vue';


const keepData = ref({
  name: '',
  img: '',
  description: '',
})

async function createKeep(){
  try {
    await keepsService.createKeep(keepData.value);
    clearForm();
    Pop.success("Keep Created!");
    Modal.getOrCreateInstance("#createKeepModal").hide();
  }
  catch (error){
    Pop.error(error);
    logger.error('Could not create Keep', error);
  }
}

watch(()=> keepData.value.img, changeImg);


function changeImg(){
  AppState.imgHasChanged = keepData.value.img.includes('http');
}


function clearForm(){
  keepData.value = {
    name: '',
    img: '',
    description: '',
  }
}

</script>


<template>
  <div class="container-fluid">
    <form @submit.prevent="createKeep()" class="row">
      <div v-if="keepData.img" class="mb-5 col-12 col-lg-6">
        <small class="m-3">Image Preview</small>
        <img :src="keepData.img" alt="Your Image" class="img-fluid photo-preview" />
      </div>
      <div @change="changeImg()" :class="keepData.img ? 'col-12 col-md-6' : 'col-12'">
        <div class="form-floating mb-3">
          <input
            type="text"
            class="form-control fs-5"
            name="title"
            id="title"
            aria-describedby="helpId"
            placeholder="Title..."
            v-model="keepData.name"
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
            v-model="keepData.img"
            minlength="1"
            maxlength="500"
            required
          />
          <label for="imgUrl" class="form-label">Image URL...</label>
        </div>
        <div class="form-floating mb-3">
          <textarea
            type="text"
            class="form-control fs-5 mt-5 mb-3"
            name="description"
            id="description"
            aria-describedby="helpId"
            placeholder="Keep Description..."
            v-model="keepData.description"
            minlength="1"
            maxlength="500"
            required
          />
          <label for="description" class="form-label">Keep Description...</label>
        </div>
        <div class="d-flex justify-content-end">
          <button type="submit" class="btn open-sans-font">Create</button>
        </div>
      </div>
    </form>
  </div>
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