<template>
    <div class="keep-card">
        <img class="img-fluid keep-image" title="image of keep" :src="keepProp.img" :alt="keepProp.name">
        <p @click="setActiveKeep(keepProp)" data-bs-toggle="modal" data-bs-target="#keepModal" type="button"
            class="bottom-left text-light keep-name fs-3" text-break :title="keepProp.name">{{ keepProp.name }}</p>
        <img class="img-fluid rounded-circle bottom-right" :title="keepProp.creator.name" :src="keepProp.creator.picture"
            :alt="keepProp.creator.name">
        <i v-if="keepProp.creator.id == account.id" class="mdi mdi-close top-right fs-3" title="delete this keep"
            role="button" @click="destroyKeep()"></i>
    </div>
    <KeepModal />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';
import KeepModal from './KeepModal.vue';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
export default {
    props: { keepProp: { type: Keep, required: true } },
    setup(props) {
        return {
            keeps: computed(() => AppState.keeps),
            account: computed(() => AppState.account),
            setActiveKeep(keepProp) {
                keepProp.views++
                keepsService.setActiveKeep(keepProp)
                // const photoId = photoProp.id

            },
            async destroyKeep() {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy this Keep? ');
                    if (!wantstoDestroy) {
                        return;
                    }
                    await keepsService.destroyKeep(props.keepProp.id);
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            }
            // async destroyVaultKeep() {
            //     try {
            //         const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy this VaultKeep? ');
            //         if (!wantstoDestroy) {
            //             return;
            //         }

            //         const vaultKeepId = props.keepProp.vaultKeepId
            //         // AppState.activeKeep.kept--
            //         logger.log('am i reaching this point?', vaultKeepId)
            //         await keepsService.destroyVaultKeep(vaultKeepId)
            //     } catch (error) {
            //         logger.error(error)
            //         Pop.error(error)

            //     }
            // }
        };
    },
    components: { KeepModal }
};
</script>


<style lang="scss" scoped>
.keep-image {
    box-shadow: 0px 3px 10px gray;
    border-radius: 7px;
    width: 100%;
}

.keep-name {
    font-family: 'Pinyon Script', cursive;
}

.keep-card {
    position: relative;
}

.bottom-left {
    position: absolute;
    bottom: 4px;
    left: 16px;
    text-shadow: 2px 2px 4px #000000;
    border-radius: 5px;
}

.bottom-right {
    position: absolute;
    bottom: 8px;
    right: 16px;
    height: 40px;
    width: 40px;
}

.top-right {
    position: absolute;
    top: 8px;
    right: 16px;
    background-color: white;
    border-radius: 1rem;
    line-height: 2rem;
}

.top-mid-right {
    position: absolute;
    top: 8px;
    right: 50px;
}
</style>