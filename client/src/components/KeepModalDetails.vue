<template>
    <div class="container-fluid">
        <section class="row">
            <div v-if="keep" class="d-flex">
                <div class="col-6 d-flex">
                    <img class="img-fluid image-card" :src="keep.img" :alt="keep.name">
                </div>
                <div class="col-6 p-2 ">
                    <div class=" keep-name mt-5">
                        <p class="fs-2 text-center mb-3 ">{{ keep.name }}</p>
                    </div>
                    <p> {{ keep.description }}</p>
                    <router-link :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
                        <div class="text-end mt-5">

                            <img class="rounded-circle img-fluid creator-image text-end" :src="keep.creator.picture"
                                :alt="keep.creator.name">
                        </div>
                    </router-link>
                </div>
            </div>

        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            keeps: computed(() => AppState.keeps)
        }
    }
};
</script>


<style lang="scss" scoped>
.image-card {
    width: 100%;
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