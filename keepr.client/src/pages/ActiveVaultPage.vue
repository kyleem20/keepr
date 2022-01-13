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
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
export default {
  name: 'ActiveVault',
  props: { vault: { type: Object } },
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(async () => {
      try {
        if (route.params.id) {
          await vaultsService.getById(route.params.id)
          await vaultKeepsService.getKeepsForVault(route.params.id)
        }
        if (route.params.id && route.params.isPrivate === true && route.params.creatorId != AppState.account || null) {
          router.push({ name: 'Home' })
        }
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.vaultKeeps),
      activeVault: computed(() => AppState.activeVault)
    }
  }
}
</script>

<style scoped lang="scss">
</style>
