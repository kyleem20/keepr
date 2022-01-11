<template>
  <div
    class="
      container-fluid
      home
      flex-grow-1
      d-flex
      flex-column
      align-items-center
      justify-content-center
    "
  >
    <div class="row home-card p-5 bg-white rounded elevation-3">
      <div class="col-3" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
</style>
