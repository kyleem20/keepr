<template>
  <div
    class="vault p-2"
    :title="vault.name"
    data-bs-toggle="modal"
    data-bs-target="#vault-modal"
    @click="setActive"
  ></div>
</template>


<script>
import { Modal } from 'bootstrap'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
  props: {
    vault: {
      type: Object, required: true
    }
  },
  setup(props) {
    return {
      async setActive() {
        try {
          AppState.activeVault = props.vault
          await vaultsService.getById(props.vault.id)
        }
        catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById('vault-modal')).hide()
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>