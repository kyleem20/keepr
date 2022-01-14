<template>
  <div class="vault p-2">
    <div class="row h-50 bg-light elevation-2 rounded selectable">
      <img
        :src="vault.img"
        :alt="vault.name"
        height="150"
        width="100"
        class="p-0 object-fit-cover rounded-top"
        @click="setActive(vault.id)"
      />
      <div
        class="
          py-3
          d-flex
          justify-content-between
          align-content-center
          text-center
        "
      >
        <h5 class="m-0 col-10">
          <b>{{ vault.name }}</b>
        </h5>
      </div>
    </div>
  </div>
</template>

<script>
import { Modal } from 'bootstrap'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { computed } from '@vue/reactivity'
import { useRouter } from 'vue-router'

export default {
  props: {
    vault: {
      type: Object, required: true
    }
  },
  setup() {
    const router = useRouter()

    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      //   vault: computed(() => AppState.profileVaults),


      async setActive(vaultId) {
        try {
          await vaultsService.getById(vaultId)
          router.push({ name: 'ActiveVault', params: { id: vaultId } })
        }
        catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>