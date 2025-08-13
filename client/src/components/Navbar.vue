<script setup>
import { AppState } from '@/AppState.js';
import { AuthService } from '@/services/AuthService.js';
import { computed } from 'vue';


const account = computed(()=> AppState.account);


function login() {
  AuthService.loginWithRedirect()
}

</script>

<template>
  <nav class="navbar navbar-expand-md border-bottom sticky-top">
    <div class="container gap-2">
      <div class="d-flex align-items-center gap-2">
        <RouterLink :to="{name: 'Home'}">
          <button class="home-btn bg-success fs-5 open-sans-font">
            Home
          </button>
        </RouterLink>
        <div v-if="account" class="dropdown">
          <button
            class="dropdown-btn dropdown-toggle open-sans-font bg-light fs-5 px-3 py-1"
            type="button"
            id="createMenu"
            data-bs-toggle="dropdown"
            aria-expanded="false">
            Create
          </button>

          <ul class="dropdown-menu shadow-sm" aria-labelledby="createMenu">
            <li><a class="dropdown-item py-1 px-3 open-sans-font" role="button" data-bs-toggle="modal" data-bs-target="#createKeepModal">new keep</a></li>
            <li><hr class="dropdown-divider my-1"></li>
            <li><a class="dropdown-item py-1 px-3 open-sans-font" role="button">new vault</a></li>
          </ul>
        </div>
      </div>
      <div>
        <RouterLink :to="{ name: 'Home' }" class="text-light">
          <img class="keepr-logo" alt="logo" src="../assets/img/keepr_logo.png" height="45" :class="account ? '' : 'keepr-logo-LO'" />
        </RouterLink>
      </div>
      <div v-if="account?.picture">
        <RouterLink :to="{ name: 'Profile', params: {profileId: `${account.id}`} }">
          <img :src="account?.picture" :alt="`${account?.name}'s profile picture`" class="img-fluid profile-picture"/>
        </RouterLink>
      </div>
      <div v-else>
        <i role="button" @click="login()" class="mdi mdi-login fs-1 text-primary"></i>
      </div>
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.home-btn {
  font-weight: 500;
  border: none;
  border-radius: 15px;
  padding-inline: 15px;
  padding-block: 3px;
  transition: all .2s ease-in-out;

  &:hover {
    color: var(--bs-primary);
    font-weight: 400;
    padding-inline: 15.3px;

  }
}

.keepr-logo-LO {
  margin-inline-start: 110px;
}

.dropdown-menu {
  padding: .25rem 0;
  border-radius: 10px;
  border: 1px solid #000;
  background-color: var(--bs-secondary);
}

.dropdown-item {
  padding: .25rem .75rem;
  line-height: 1.25;
  font-size: 1.2rem;
  text-align: start;
  border-radius: 10px;
  font-weight: 600;
  transition: all .2s ease-in-out;

  &:hover {
    background-color: var(--bs-secondary);
    color: var(--bs-primary);
    font-weight: 500;
  }
}

.dropdown-divider {
  border-top: 1px solid #000;
  margin: .25rem 15px;
  width: 80%;

}

.keepr-logo {
  height: 60px;
  padding-right: 150px;
}



.dropdown-btn {
  border: none;
  font-weight: 600;

  transition: all .2s ease-in-out;

  &:hover {
    font-weight: 500;
    opacity: 50%;
    padding-right: 17px !important;
  }
}

.profile-picture {
  height: 60px;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
  box-shadow: 0 4px 2px rgba(174, 173, 173, 0.45);
}


.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.navbar {
  box-shadow: 1px 0px 5px rgba(151, 145, 145, 0.556);
}
</style>
