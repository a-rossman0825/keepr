<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { AuthService } from '@/services/AuthService.js';

const account = computed(() => AppState.account)

function login() {
  AuthService.loginWithRedirect()
}
function logout() {
  AuthService.logout()
}

</script>

<template>
  <div class="about text-center">
    <div v-if="account">
      <h1>Welcome {{ account.name }}</h1>
      <img class="rounded" :src="account.picture" alt="" />
      <p>{{ account.email }}</p>
      <button v-if="account" @click="logout()">logout</button>
      <button v-else @click="login()">login</button>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}
</style>
