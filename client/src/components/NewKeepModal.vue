<template>
    <div class="modal fade" id="newKeepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center text-light" id="exampleModalLabel">Create Your Keep</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" id="name"
                                aria-describedby="emailHelp" maxlength="255" required placeholder="Name of Keep...">

                        </div>
                        <div class="mb-3">
                            <label for="imgUrl" class="form-label">Image Url</label>
                            <input v-model="editable.img" type="url" class="form-control" id="imgUrl"
                                aria-describedby="emailHelp" maxlength="1000" required placeholder="Image Url...">

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
                            <textarea v-model="editable.description" text-break type="text" class="form-control"
                                id="description" rows="5" maxlength="1000" required placeholder="Description..."></textarea>
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
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import { Modal } from 'bootstrap';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            keeps: computed(() => AppState.keeps),
            async createKeep() {
                try {
                    const keepData = editable.value
                    const keep = await keepsService.createKeep(keepData)
                    Pop.success('Keep created!')
                    Modal.getOrCreateInstance('#newKeepModal').hide()
                    editable.value = {}
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
.modal-header {
    background-color: #877A8F;

}

.modal-footer {
    background-color: #877A8F;
}
</style>