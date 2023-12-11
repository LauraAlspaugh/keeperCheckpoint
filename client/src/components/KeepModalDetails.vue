<template>
    <div class="container-fluid">
        <section class="row">
            <div v-if="keep" class="d-flex">
                <div class="col-6 d-flex">
                    <img class="img-fluid image-card" :src="keep.img" :alt="keep.name">
                </div>
                <div class="col-6 p-2 ">
                    <div class=" keep-name mt-5 text-center">
                        <p>{{ keep.views }} <i class="mdi mdi-eye fs-5"></i></p>
                        <p>{{ keep.kept }}<i class="mdi mdi-alpha-k-circle-outline fs-5"></i> </p>
                        <p class="fs-2 text-center mb-3 ">{{ keep.name }}</p>
                        <!-- <i class="mdi mdi-delete text-center fs-3" title="delete this keep" role="button"
                            @click="destroyKeep()"> </i> -->
                    </div>
                    <p> {{ keep.description }}</p>
                    <!-- <button @click="createVaultKeep()" class="btn btn-secondary dropdown-toggle" data-toggle="dropdown"
                        v-for="vault in vaults" :key="vault.id">{{ vault.name }}</button> -->
                    <label for="vault" class="form-label">Vault</label>
                    <select v-model="editable.vault" class="form-select" name="" id="">

                        <option :value="vault" v-for="vault in vaults" :key="vault">
                            {{ vault.name }}
                        </option>
                    </select>
                    <button @click="createVaultKeep()" class="btn btn-outline-dark mt-2">Save</button>




                    <router-link :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
                        <div class="text-end mt-5">

                            <img class="rounded-circle img-fluid creator-image text-end" :title="keep.creator.name"
                                :src="keep.creator.picture" :alt="keep.creator.name">
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
export default {

    setup() {
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
            keepsService.clearAppState()
            // getKeepById()
        }, { immediate: true });
        return {
            editable,
            keep: computed(() => AppState.activeKeep),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
            vaultkeeps: computed(() => AppState.vaultKeeps),
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

                    const vaultId = vault.id
                    const keepId = activeKeep.id
                    await keepsService.createVaultKeep(vaultId, keepId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.image-card {
    width: 100%;
    height: 100%;
}

.keep-name {
    vertical-align: center;
    padding-top: 10px;
}

.creator-image {
    width: 40px;
    height: 40px;
}
</style>