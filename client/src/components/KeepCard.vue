<template>
    <div @click="setActiveKeep(keepProp)" data-bs-toggle="modal" data-bs-target="#keepModal" type="button"
        class="keep-card">
        <img class="img-fluid keep-image" title="image of keep" :src="keepProp.img" :alt="keepProp.name">
        <p class="bottom-left text-light fs-3" :title="keepProp.name">{{ keepProp.name }}</p>
        <img class="img-fluid rounded-circle bottom-right" :title="keepProp.creator.name" :src="keepProp.creator.picture"
            :alt="keepProp.creator.name">
    </div>
    <KeepModal />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';
import KeepModal from './KeepModal.vue';
import { keepsService } from '../services/KeepsService.js';
export default {
    props: { keepProp: { type: Keep, required: true } },
    setup(props) {
        return {
            keeps: computed(() => AppState.keeps),
            setActiveKeep(keepProp) {
                keepsService.setActiveKeep(keepProp)
                // const photoId = photoProp.id

            },
        };
    },
    components: { KeepModal }
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
}

.bottom-right {
    position: absolute;
    bottom: 8px;
    right: 16px;
    height: 40px;
    width: 40px;
}
</style>