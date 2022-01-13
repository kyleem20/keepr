<template>
  <div class="vault p-2" v-if="vault.creatorId === account.id">
    <div class="row h-50 bg-light elevation-2 rounded selectable">
      <img
        :src="vault.img"
        :alt="vault.name"
        class="p-0 object-fit-cover w-100 rounded-top"
        @click="setActive"
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
        <i class="mdi mdi-lock" v-if="vault.isPrivate == true"></i>
      </div>
    </div>
  </div>
</template>

// TODO Router push to active vault on click
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
  setup(props) {
    const router = useRouter()

    return {
      account: computed(() => AppState.account),

      async setActive() {
        try {
          AppState.activeVault = props.vault
          await vaultsService.getById(props.vault.id)
          router.push({ name: 'ActiveVault', params: { id: props.vault.id } })
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