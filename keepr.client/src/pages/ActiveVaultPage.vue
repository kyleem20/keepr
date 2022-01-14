<template>
  <div class="row m-3 p-2">
    <h1 class="d-flex justify-content-between">
      {{ activeVault.name }}
      <button
        v-if="activeVault.creatorId === account.id"
        type="button"
        class="btn btn-outline-secondary"
        @click="deleteVault(activeVault.id)"
      >
        Delete Vault
      </button>
    </h1>

    <h4 class="m-3 p-3">Keeps: {{ keeps.length }}</h4>
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
import { keepsService } from '../services/KeepsService'
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
          await vaultKeepsService.getKeepsForVault(AppState.activeVault.id)
        }
        if (route.params.isPrivate === true && route.params.creatorId != AppState.account) {
          router.push({ name: 'Home' })
        }
      } catch (error) {
        router.push({ name: 'Home' })
      }
    })
    return {
      keeps: computed(() => AppState.vaultKeeps),
      activeVault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
      async deleteVault(vId) {
        try {
          await vaultsService.remove(vId)
          router.push({ name: 'Account' })
        }
        catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
</style>
