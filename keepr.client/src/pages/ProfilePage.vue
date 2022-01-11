<template>
  <div class="profile">
    <h1>Welcome to the profile page</h1>

    {{ vaults }}
    {{ keeps }}
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import { profilesService } from '../services/ProfilesService'
import Pop from '../utils/Pop'

export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getKeepsByCreatorId(route.params.id)
        await profilesService.getVaultsByCreatorId(route.params.id)

      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>