<template>
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel"></h5>
                    <button type="button" class="btn-close text-end" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body ">
                    <KeepModalDetails />
                </div>
                <!-- <div class="modal-footer">

                </div> -->
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import KeepModalDetails from './KeepModalDetails.vue';
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep)
        };
    },
    components: { KeepModalDetails }
};
</script>


<style lang="scss" scoped></style>