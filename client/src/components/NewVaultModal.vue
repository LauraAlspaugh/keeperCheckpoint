<template>
    <div class="modal fade" id="newVaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center text-light" id="exampleModalLabel">Create Your Vault</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault()">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input v-model="editable2.name" type="text" class="form-control" id="name"
                                aria-describedby="emailHelp" maxlength="255" required>

                        </div>
                        <div class="mb-3">
                            <label for="imgUrl" class="form-label">Image Url</label>
                            <input v-model="editable2.img" type="url" class="form-control" id="imgUrl"
                                aria-describedby="emailHelp" maxlength="1000" required>

                        </div>
                        <!-- <div class="mb-3">
                            <label for="category" class="form-label">Category</label>
                            <select v-model="editable.category" type="text" class="form-select" id="category"
                                aria-describedby="emailHelp">
                                <option :value="category" v-for="category in categories" :key="category">
                                    {{ category }}
                                </option>
                            </select>
                        </div> -->
                        <div class="mb-3">
                            <label for="description" class="form-label">Description</label>
                            <textarea v-model="editable2.description" type="text" class="form-control" id="description"
                                rows="5" maxlength="255"></textarea>
                        </div>
                        <div class="mb-3">
                            <label class="me-2" for="isPrivate">Private</label>
                            <input v-model="editable2.isPrivate" type="checkbox" class="form-check-input" id="isPrivate">
                        </div>

                        <button type="submit" class="btn btn-outline-dark">Submit</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button> -->
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { Modal } from 'bootstrap';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from '../utils/Logger.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';
export default {
    setup() {
        const editable2 = ref({})
        return {
            editable2,
            vaults: computed(() => AppState.vaults),
            async createVault() {
                try {
                    const vaultData = editable2.value
                    const vault = await vaultsService.createVault(vaultData)
                    Pop.success('Vault created!')
                    Modal.getOrCreateInstance('#newVaultModal').hide()
                    editable2.value = {}
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>