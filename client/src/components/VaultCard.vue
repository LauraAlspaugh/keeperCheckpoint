<template>
    <div class="keep-card">
        <img class="img-fluid keep-image" title="image of vault" :src="vaultProp.img" :alt="vaultProp.name">
        <router-link :to="{ name: 'Vault', params: { vaultId: vaultProp.id } }">
            <p class="bottom-left text-light fs-3" :title="vaultProp.name">{{ vaultProp.name }}</p>
            <i v-if="vaultProp.isPrivate == true" class="mdi mdi-lock-outline text-white fs-3 bottom-mid-left"></i>
        </router-link>
        <!-- <img class="img-fluid rounded-circle bottom-right" :title="vaultProp.creator.name" :src="vaultProp.creator.picture"
            :alt="vaultProp.creator.name"> -->
        <i class="mdi mdi-close top-right fs-3" title="delete this vault" role="button" @click="destroyVault()"></i>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault.js';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
export default {
    props: { vaultProp: { type: Vault, required: true } },
    setup(props) {
        return {
            vaults: computed(() => AppState.vaults),
            async destroyVault() {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy this Vault? ');
                    if (!wantstoDestroy) {
                        return;
                    }
                    await vaultsService.destroyVault(props.vaultProp.id);
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            }
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

.top-right {
    position: absolute;
    top: 8px;
    right: 16px;
}

.bottom-mid-left {
    position: absolute;
    bottom: 20px;
    right: 20px;
    height: 40px;
    width: 40px;
}
</style>