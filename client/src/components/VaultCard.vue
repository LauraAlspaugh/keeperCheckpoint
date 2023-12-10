<template>
    <router-link :to="{ name: 'Vault', params: { vaultId: vaultProp.id } }">
        <div class="keep-card">
            <img class="img-fluid keep-image" title="image of vault" :src="vaultProp.img" :alt="vaultProp.name">
            <p class="bottom-left text-light fs-3" :title="vaultProp.name">{{ vaultProp.name }}</p>
            <!-- <img class="img-fluid rounded-circle bottom-right" :title="vaultProp.creator.name" :src="vaultProp.creator.picture"
            :alt="vaultProp.creator.name"> -->
        </div>
    </router-link>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault.js';
export default {
    props: { vaultProp: { type: Vault, required: true } },
    setup() {
        return {
            vaults: computed(() => AppState.vaults)
        }
    }
};
</script>


<style lang="scss" scoped>
.keep-image {
    box-shadow: 0px 3px 10px gray;
    border-radius: 7px;
}

.keep-card {
    position: relative;
}

.bottom-left {
    position: absolute;
    bottom: 4px;
    left: 16px;
    backdrop-filter: blur(15px);
    border-radius: 5px;
}

.bottom-right {
    position: absolute;
    bottom: 8px;
    right: 16px;
    height: 40px;
    width: 40px;
}
</style>