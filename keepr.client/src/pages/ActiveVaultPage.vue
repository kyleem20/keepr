<template>
  <div class="row m-3 p-2">
    <h1 class="d-flex justify-content-between">
      {{ activeVault.name }}
      <button type="button" class="btn btn-outline-secondary">
        Delete Vault
      </button>
    </h1>

    <h4 class="m-3 p-3">Keeps:</h4>
  </div>
  <div class="row p-3">
    <div class="col-md-3 col-6 p-3" v-for="k in keeps" :key="k.id">
      <VaultKeeps :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  props: { vault: { type: Object } },
  setup() {
    onMounted(async () => {
      try {
        await vaultsService.getByCreatorId(AppState.activeVault.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      activeVault: computed(() => AppState.activeVault)
    }
  }
}
</script>

<style scoped lang="scss">
</style>
