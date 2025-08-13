<script setup>
import { AppState } from '@/AppState.js';
import { AuthService } from '@/services/AuthService.js';
import { computed } from 'vue';


const account = computed(()=> AppState.account);

function login() {
  AuthService.loginWithRedirect()
}

function increaseImgI(){
  AppState.choicePhotoIndex++;
}


</script>


<template>
  <section class="container-fluid fixed-bottom">
    <div class="footer-wrapper align-items-center justify-content-between d-flex px-2">
      <div class="">
        <RouterLink :to="{ name: 'Home'}">
          <img src="../assets/img/keepr_logo.png" alt="keeper logo" class="img-fluid w-100">
        </RouterLink>
      </div>
      <div v-if="account">
        <button @click="increaseImgI()"
          role="button"
          data-bs-toggle="modal"
          data-bs-target="#createChoiceModal" 
          class="create-btn bg-primary text-light btn fs-4 open-sans-font">
          Create
        </button>
      </div>
      <div v-else>
        <button
          @click="login()"
          role="button"
          class="create-btn bg-primary text-light btn fs-4 open-sans-font">Login to Create</button>
      </div>
      <div v-if="account">
        <!-- TODO NO ROUTER LINK FOR ACCOUNT -->
        <RouterLink :to="{name: 'Profile', params: { profileId: `${account.id}`}}">
          <img  :src="account.picture" :alt="`${account.name}'s profile picture`" class="profile-picture">
        </RouterLink>
      </div>
      <div v-else>
        <i @click="login()"
            role="button"
            class="mdi mdi-login display-4 me-2 text-primary"></i>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>

.footer-wrapper {
  height: 90px;
}

.create-btn {
font-weight: 600;
padding: 15px;
border-radius: 12px;
box-shadow: 0 4px 2px rgba(174, 173, 173, 0.45);
}

.profile-picture {
  width: 70px;
  aspect-ratio: 1/1;
  border-radius: 50%;
  box-shadow: 0 4px 2px rgba(174, 173, 173, 0.45);
}

</style>