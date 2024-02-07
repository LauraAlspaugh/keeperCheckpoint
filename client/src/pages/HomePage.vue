<template>
  <div class="container">
    <section class="row mt-3 mb-3">

      <div class="masonry">
        <div v-for="keep in keeps" :key="keep.id" class=" col-12 p-1  w-100 mb-2 div-height">
          <KeepCard :keepProp="keep" />
        </div>
      </div>

    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';

export default {
  setup() {
    onMounted(() => {
      getKeeps();
    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.masonry {
  columns: 300px;
  column-gap: 1rem;
}
</style>
