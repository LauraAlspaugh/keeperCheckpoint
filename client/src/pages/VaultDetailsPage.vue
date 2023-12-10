<template>
    <div class="container-fluid">
        <section v-if="vault" class="row">
            <div class="col-12 p-4 vault-card">
                <img class="img-fluid vault-image" :src="vault.img" alt="">
                <p class="centered ">{{ vault.name }}</p>
                <p class="below">By {{ vault.creator.name }}</p>
                <p>{{ keeps.length }} keeps</p>
            </div>
        </section>
        <section v-if="vault" class="row">
            <div v-for="keep in keeps" :key="keep.id" class="col-3">

                <KeepCard :keepProp="keep" />

            </div>
        </section>
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { vaultsService } from '../services/VaultsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import KeepCard from '../components/KeepCard.vue';
export default {
    setup() {
        const route = useRoute()
        onMounted(() => {
            getVaultById()
            getKeepsByVaultId()
        })
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId
                await vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error);
            }
        }
        async function getKeepsByVaultId() {
            try {
                const vaultId = route.params.vaultId
                await vaultsService.getKeepsByVaultId(vaultId)
            } catch (error) {
                logger.error(error)
                Pop.error(error)

            }
        }
        return {
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps)
        }
    },
    components: { KeepCard }
};
</script>


<style lang="scss" scoped>
.vault-image {
    width: 100%;
    height: 300px;
    object-fit: cover;
    position: center;
    border-radius: 7px;
}

.vault-card {
    position: relative;
}

.centered {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    color: white;
    font-size: 70px;
    backdrop-filter: blur(15px);
    border-radius: 7px;
}

.below {
    position: absolute;
    top: 80%;
    left: 50%;
    transform: translate(-50%, -50%);
    color: white;
    font-size: 40px;
    backdrop-filter: blur(10px);
    border-radius: 7px;
}
</style>