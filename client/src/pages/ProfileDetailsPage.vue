<template>
    <div class="container-fluid">
        <section v-if="profile" class="row">
            <div v-if="profile" class="col-12 p-4">
                <img class="img-fluid profile-image" :src="profile.picture" alt="">
            </div>
            <div class="col-12 text-center">
                <img class="img-fluid rounded-circle profile-img" :src="profile.picture" alt="">
                <p class=" fs-1 mt-5 text-center ">{{ profile.name }}</p>
                <p class="fs-5">
                    Vaults: {{ vaults.length }} || Keeps: {{ keeps.length }}
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
            <div v-for="keep in keeps" :key="keep.id" class="col-3">
                <KeepCard :keepProp="keep" />
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { profileService } from '../services/ProfileService.js';
import { vaultsService } from '../services/VaultsService.js';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import KeepCard from '../components/KeepCard.vue';
import VaultCard from '../components/VaultCard.vue';
export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            getProfile();
            getKeepsByProfileId();
            getVaultsByProfileId()
        });
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
</style>