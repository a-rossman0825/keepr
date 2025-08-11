<script setup>
import { AppState } from '@/AppState.js';
import { Modal } from 'bootstrap';
import { computed } from 'vue';

// TODO make this array an appstate of keeps Images?
const photos = [
  {
    name: 'rhinos',
    imgUrl: 'https://images.unsplash.com/photo-1547970810-dc1eac37d174?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8cmhpbm98ZW58MHwyfDB8fHww',
  },
  {
    name: 'kitten',
    imgUrl: 'https://images.unsplash.com/photo-1694239400333-0051c92d420f?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8a2l0dGVufGVufDB8MnwwfHx8MA%3D%3D',
  },
  {
    name: 'flower',
    imgUrl: 'https://images.unsplash.com/photo-1444021465936-c6ca81d39b84?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8Zmxvd2Vyc3xlbnwwfDJ8MHx8fDA%3D',
  },
  {
    name: 'boat',
    imgUrl: 'https://images.unsplash.com/photo-1663550352112-9b62454ec577?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8dGhlJTIwb2NlYW58ZW58MHwyfDB8fHww',
  },
  {
    name: 'Taj Mahal',
    imgUrl: 'https://images.unsplash.com/photo-1690812161626-3a034b109a38?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDV8fGNvbmNlcnR8ZW58MHwyfDB8fHww',
  },
  {
    name: 'yoga guy',
    imgUrl: 'https://plus.unsplash.com/premium_photo-1726736480651-df15be52ba93?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjh8fGNhbG18ZW58MHwyfDB8fHww',
  },
];

// const chosenPhotoIndex = computed(() => AppState.choicePhotoIndex);

let chosenPhoto = computed( () => {
  if (AppState.choicePhotoIndex > photos.length-1) {
    AppState.choicePhotoIndex = 0;
  }
  return photos[AppState.choicePhotoIndex];
})

function openForm(form){
  if (form == 'keep') {
    Modal.getOrCreateInstance("#createChoiceModal").hide();
    Modal.getOrCreateInstance("#createKeepModal").show();
  } else {
    Modal.getOrCreateInstance("#createChoiceModal").hide();
    Modal.getOrCreateInstance("createVaultModal").show();
  }
}

</script>


<template>
  <div class="container-fluid">
    <div class="row text-center">
      <!-- SECTION Photo Upload Choice -->
      <div class="col-6">
        <img v-if="chosenPhoto" @click="openForm('keep')" :src="chosenPhoto?.imgUrl" :alt="chosenPhoto?.name" class="img-fluid photo rounded">
        <button @click="openForm('keep')" type="button" class="btn btn-secondary mt-2 open-sans-font">Add Keep</button>
      </div>
      <!-- !SECTION -->
      <!-- SECTION Album Upload Choice -->
      <div class="col-6">
        <img @click="openForm('vault')" src="https://images.unsplash.com/photo-1677658288237-2fff291a34f0?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8cGhvdG8lMjBhbGJ1bXxlbnwwfDJ8MHx8fDA%3D" alt="photo album" class="img-fluid photo rounded">
        <button @click="openForm('vault')" type="button" class="btn btn-secondary mt-2 open-sans-font">Add Vault</button>
      </div>
      <!-- !SECTION -->
    </div>
  </div>
</template>


<style lang="scss" scoped>
.photo {
  height: 210px;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
  box-shadow: 2px 3px 5px rgba(126, 125, 125, 0.7);

  &:hover {
    cursor: pointer;
  }
}

button {
  box-shadow: 1px 2px 3px var(--bs-primary);
  font-weight: 500;
  border: none;
  transition: all .25s ease-in-out;

  &:hover {
    box-shadow: 2px 3px 3px var(--bs-secondary);
    color: var(--bs-light);
    background-color: var(--bs-primary);
  }
}




</style>