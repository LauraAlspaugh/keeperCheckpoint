<template>
  <nav class="navbar navbar-expand-sm navbar-dark nav-color px-3 ">
    <router-link class="navbar-brand d-flex " :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <i class="mdi mdi-arrow-down fs-1 photo-title "><span class="plan-title fs-1 p-0" title="back to homepage">Keepr
            Co.</span></i>
      </div>
    </router-link>
    <button v-if="account.id" class="btn btn-outline-dark" data-bs-target="#newKeepModal" data-bs-toggle="modal"
      title="create a keep">Create Keep</button>
    <button v-if="account.id" class="btn btn-outline-dark m-2" data-bs-target="#newVaultModal" data-bs-toggle="modal"
      title="create a keep">Create Vault</button>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <!-- <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link> -->
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <div>
        <button class="btn text-light" @click="toggleTheme"><i class="mdi"
            :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button>
      </div>
      <Login />
    </div>
  </nav>
  <NewKeepModal />
  <NewVaultModal />
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import NewKeepModal from './NewKeepModal.vue';
import NewVaultModal from './NewVaultModal.vue';
import { AppState } from '../AppState.js';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      account: computed(() => AppState.account),
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login, NewKeepModal, NewVaultModal }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.nav-color {
  background-color: #E9D8D6;
  height: 90px;
  /* background-color: #FEF6F0; */
}

.photo-title {

  font-family: 'Pinyon Script', cursive;
  color: #2D2D2D;
}

@media screen and (min-width: 768px) {
  nav {
    height: 94px;
  }

  button {
    display: block;
  }
}
</style>
