<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';


const account = computed(()=> AppState.account);
</script>

<template>
  <nav class="navbar navbar-expand-md border-bottom sticky-top">
    <div class="container gap-2">
      <div class="d-flex align-items-center gap-2">
        <button class="home-btn bg-success text-dark fs-5 open-sans-font">
          Home
        </button>
        <div class="dropdown">
          <button
            class="dropdown-btn dropdown-toggle open-sans-font bg-light fs-6 px-3 py-1"
            type="button"
            id="createMenu"
            data-bs-toggle="dropdown"
            aria-expanded="false">
            Create
          </button>

          <ul class="dropdown-menu shadow-sm" aria-labelledby="createMenu">
            <li><a class="dropdown-item py-1 px-3" href="#">Action</a></li>
            <li><hr class="dropdown-divider my-1"></li>
            <li><a class="dropdown-item py-1 px-3" href="#">Another action</a></li>
          </ul>
        </div>
      </div>
      <div>
        <RouterLink :to="{ name: 'Home' }" class="text-light">
          <img class="keepr-logo" alt="logo" src="../assets/img/keepr_logo.png" height="45" />
        </RouterLink>
      </div>
      <div>
        <img :src="account?.picture" :alt="`${account?.name}'s profile picture`" class="img-fluid profile-picture"/>
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
  border-radius: 10px;
  padding-inline: 15px;
  padding-block: 5px;
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
  font-size: .95rem;
  text-align: start;
  border-radius: 10px;

  &:hover {
    background-color: var(--bs-secondary);
  }
}

.dropdown-divider {
  border-top: 1px solid #000;
  margin: .25rem 15px;
  width: 80%;

}

.keepr-logo {
  height: 60px;
  padding-right: 160px;
}



.dropdown-btn {
  border: none;
  font-weight: 600;
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
