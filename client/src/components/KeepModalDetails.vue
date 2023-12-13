<template>
    <div class="container-fluid p-0">
        <section class="row">
            <div v-if="keep" class="d-flex">
                <div class="col-md-6 p-0">
                    <img class="img-fluid image-card" :src="keep.img" :alt="keep.name" title="image of keep">
                </div>
                <div class=" col-md-6 p-2 ">
                    <div class="d-flex justify-content-end w-100">

                        <button type="button" class="btn-close d-flex justify-content-end" data-bs-dismiss="modal"
                            aria-label="Close"></button>
                    </div>
                    <div class=" keep-name mt-5 text-center ">
                        <div class="d-flex justify-content-center  ">

                            <p class="p-2">{{ keep.views }} <i class="mdi mdi-eye fs-5" title="view count"></i></p>
                            <p class="p-2">{{ keep.kept }}<i class="mdi mdi-alpha-k-circle-outline fs-5"
                                    title="times this keep has been saved"></i> </p>
                        </div>
                        <p class="fs-2 text-center mb-3 keep-name" :title="keep.name">{{ keep.name }}</p>
                        <!-- <i class="mdi mdi-delete text-center fs-3" title="delete this keep" role="button"
                            @click="destroyKeep()"> </i> -->
                    </div>
                    <p class="p-4"> {{ keep.description }}</p>
                    <!-- <button @click="createVaultKeep()" class="btn btn-secondary dropdown-toggle" data-toggle="dropdown"
                        v-for="vault in vaults" :key="vault.id">{{ vault.name }}</button> -->
                    <!-- <label for="vault" class="form-label">Vault</label> -->
                    <select @click="getMyVaults()" v-model="editable.vaultId" class="form-select  p-2" name="vault"
                        id="vault">

                        <option :value="vault.id" v-for="vault in vaults" :key="vault">{{ vault.name }}</option>


                    </select>
                    <button @click="createVaultKeep()" class="btn btn-outline-dark m-2 "
                        title="Save this to your vault">Save</button>
                    <button v-if="$route.name == 'Vault'" @click="destroyVaultKeep()" class="btn btn-outline-dark mt-2 m-2"
                        title="remove this vault keep">Remove</button>





                    <router-link :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
                        <div class="text-end mt-5">

                            <img class="rounded-circle img-fluid creator-image text-end" :title="keep.creator.name"
                                :src="keep.creator.picture" :alt="keep.creator.name" data-bs-dismiss="modal">
                        </div>
                    </router-link>
                </div>
            </div>

        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect, watch, ref } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { VaultKeep } from '../models/VaultKeep.js';
import { useRoute } from 'vue-router';
import { vaultsService } from '../services/VaultsService.js';
import { accountService } from '../services/AccountService.js';


export default {
    // props: { vaultKeepProp: { type: VaultKeep, required: true } },
    setup() {
        const route = useRoute()
        const editable = ref({})
        const watchableKeepId = computed(() => AppState.keeps.id)
        // onMounted(() => {
        //     getKeepById()
        // }),
        // async function getKeepById() {
        //     try {
        //         const keepId = AppState.keeps.id
        //         keep.views++
        //         await keepsService.getKeepById(keepId)
        //     } catch (error) {
        //         logger.error(error)
        //         Pop.error(error)

        //     }
        // }
        watch(watchableKeepId, () => {
            // keepsService.clearAppState()
            // getKeepById()
        }, { immediate: true });

        return {
            editable,
            profile: computed(() => AppState.profile),
            keep: computed(() => AppState.activeKeep),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.myVaults),
            vault: computed(() => AppState.activeVault),
            vaultkeeps: computed(() => AppState.vaultKeeps),
            vaultKeep: computed(() => AppState.activeVaultKeep),

            async destroyKeep() {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy this Keep? ');
                    if (!wantstoDestroy) {
                        return;
                    }
                    await keepsService(props.keepProp.id);
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            },
            async createVaultKeep() {
                try {
                    logger.log(editable.value)
                    editable.value.keepId = AppState.activeKeep.id
                    logger.log('about to head to the service', editable.value)
                    const vaultKeepData = editable.value
                    AppState.activeKeep.kept++
                    await keepsService.createVaultKeep(vaultKeepData)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            // async getVaultsByProfileId() {
            //     try {
            //         const profileId = AppState.activeProfile
            //         await vaultsService.getVaults(profileId)
            //     } catch (error) {
            //         logger.error(error)
            //         Pop.error(error)

            //     }
            // },
            async destroyVaultKeep() {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy this VaultKeep? ');
                    if (!wantstoDestroy) {
                        return;
                    }
                    logger.log(AppState.activeKeep)
                    const vaultKeepId = AppState.activeKeep.vaultKeepId
                    // AppState.activeKeep.kept--
                    logger.log('am i reaching this point?', vaultKeepId)
                    await keepsService.destroyVaultKeep(vaultKeepId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            // async getMyVaults() {
            //     try {
            //         await accountService.getMyVaults()
            //     } catch (error) {
            //         logger.error(error)
            //         Pop.error(error)

            //     }
            // }

        }
    }
};
</script>


<style lang="scss" scoped>
.image-card {
    width: 100%;
    height: 100%;
    border-radius: 7px;
}

.keep-name {
    vertical-align: center;
    padding-top: 10px;
}

.creator-image {
    width: 40px;
    height: 40px;
}

.keep-name {
    font-family: 'Pinyon Script', cursive;
}
</style>