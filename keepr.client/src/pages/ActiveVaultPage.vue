<template>
  <div class="row mt-5 pt-5 p-2">
    <h2>
      {{ vault.name }}
      <i
        class="mdi mdi-plus selectable"
        aria-label="create vault"
        title="New Vault"
        data-bs-toggle="modal"
        data-bs-target="#create-vaults-modal"
      ></i>
    </h2>
  </div>
  <div class="row p-2 m-1">
    <div class="col-md-2 col-6 p-2" v-for="k in keep" :key="k.id">
      <AccountKeep :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
export default {
  name: 'Account',
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
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      vault: computed(() => AppState.activeVault),
      keep: computed(() => AppState.keeps),
      accountKeep: computed(() => AppState.keeps),


    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
