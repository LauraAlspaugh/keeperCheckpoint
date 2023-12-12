<template>
    <div class="container-fluid">
        <section v-if="profile" class="row">
            <div v-if="profile" class="col-12 p-4">
                <img class="img-fluid profile-image" :src="profile.coverImg" alt="">
            </div>
            <div class="col-12 text-center">
                <img class="img-fluid rounded-circle profile-img" :src="profile.picture" alt="">
                <p class=" fs-1 mt-5 text-center ">{{ profile.name }}</p>
                <p class="fs-5">
                    {{ vaults.length }} Vaults || {{ keeps.length }} Keeps
                </p>

            </div>
        </section>
        <section class="row">
            <div>
                <p class="fs-1 keep-title">Vaults</p>
            </div>
            <div v-for="vault in vaults" :key="vault.id" class="col-3 p-3">
                <VaultCard :vaultProp="vault" />
            </div>
        </section>
        <section class="row">
            <div>
                <p class="fs-1 keep-title">Keeps</p>
            </div>
            <div class="masonry">

                <div v-for="keep in keeps" :key="keep.id" class="col-12 p-3 w-100 mb-2 div-height ">
                    <KeepCard :keepProp="keep" />
                </div>
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import { profileService } from '../services/ProfileService.js';
import { vaultsService } from '../services/VaultsService.js';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import KeepCard from '../components/KeepCard.vue';
import VaultCard from '../components/VaultCard.vue';
import { keepsService } from '../services/KeepsService.js';
export default {
    setup() {
        const route = useRoute();
        const watchableProfileId = computed(() => route.params.profileId);
        // onMounted(() => {
        //     getProfile();
        //     getKeepsByProfileId();
        //     getVaultsByProfileId()
        // });
        async function getProfile() {
            try {
                const profileId = route.params.profileId;
                await profileService.getProfile(profileId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error);
            }
        }
        async function getKeepsByProfileId() {
            try {
                const profileId = route.params.profileId;
                await profileService.getKeepsByProfileId(profileId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error);
            }
        }
        async function getVaultsByProfileId() {
            try {
                const profileId = route.params.profileId
                await vaultsService.getVaults(profileId)
            } catch (error) {
                logger.error(error)
                Pop.error(error)

            }
        }
        watch(watchableProfileId, () => {
            keepsService.clearAppState()
            getProfile()
            getVaultsByProfileId()
            getKeepsByProfileId()
        }, { immediate: true });
        return {
            keeps: computed(() => AppState.keeps),
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            vaults: computed(() => AppState.vaults)
        };
    },
    components: { KeepCard, VaultCard }
};
</script>


<style lang="scss" scoped>
.profile-image {
    width: 100%;
    height: 300px;
    object-fit: cover;
    position: center;
    border-radius: 7px;
}

.profile-img {
    width: 100px;
    height: 100px;
    border: 2px solid gray;
}

.keep-title {
    font-family: 'Pinyon Script', cursive;
}

.masonry {
    columns: 400px;
}
</style>