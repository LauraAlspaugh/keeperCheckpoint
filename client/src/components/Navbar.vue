<template>
  <nav class="row navbar py-0 px-3">
    <!-- <router-link class=" col-4 navbar-brand d-flex " :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        Home
      </div>
    </router-link> -->
    <div class="col-4 d-flex justify-content-center justify-content-md-start order-2 order-md-1">
      <router-link :to="{ name: 'Home' }" class="d-none d-md-block btn selectable nav-color" title="go home?">
        Home
      </router-link>
      <div v-if="account.id" :class="{ dropdown: width >= 768, dropup: width < 768 }">
        <button type="button" role="button" class="btn dropdown-toggle mx-2 fs-5" data-bs-toggle="dropdown"
          aria-expanded="false">Create</button>

        <div class="dropdown-menu p-0 create" aria-labelledby="createDropdown">
          <div class="list-group create">
            <div class="dropdown-item list-group-item-action fw-bold selectable p-1" data-bs-toggle="modal"
              data-bs-target="#newKeepModal">
              New Keep
            </div>
            <hr class="m-0 px-2">
            <div class="dropdown-item list-group-item-action fw-bold selectable p-1" data-bs-toggle="modal"
              data-bs-target="#newVaultModal">
              New Vault
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="col-4 d-flex order-1 order-md-2">
      <router-link class="rounded mx-md-auto ps-2 pe-4  pt-2 keeper-logo" title="go home?" :to="{ name: 'Home' }">
        <p>the<br>keepr<br>co.</p>
      </router-link>
    </div>
    <!-- <button v-if="account.id" class="btn btn-outline-dark" data-bs-target="#newKeepModal" data-bs-toggle="modal"
      title="create a keep">Create Keep</button>
    <button v-if="account.id" class="btn btn-outline-dark m-2" data-bs-target="#newVaultModal" data-bs-toggle="modal"
      title="create a keep">Create Vault</button> -->
    <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse d-flex justify-content-end" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li> -->
    <!-- <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link> -->
    <!-- </li>
      </ul> -->
    <!-- LOGIN COMPONENT HERE -->

    <!-- <Login />
    </div>  -->
    <div class="col-4 d-flex justify-content-end order-3">
      <button class="btn text-light" @click="toggleTheme"><i class="mdi"
          :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button>
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

nav {
  height: 50px;
  background: #FEF6F0;
  box-shadow: 0px 3px 10px gray;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.nav-color {
  background-color: #E9D8D6;
  border-radius: 15px;
  font-family: 'Marko One', serif;
  font-weight: bold;
  color: #2D2D2D;
}

.keeper-logo {
  color: black;
  border: 1.5px solid black;
  line-height: 15px;
  font-family: 'Marko One', serif;
  font-weight: bold;
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
