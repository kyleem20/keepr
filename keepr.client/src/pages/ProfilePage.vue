<template>
  <div class="about row p-3 mb-5">
    <div class="col-md-2 p-0">
      <img
        class="rounded text-center ms-5 mb-0 image-fit-contain"
        :src="profile.picture"
        :alt="profile.name"
        height="100"
        width="100"
      />
    </div>
    <div class="col-md-8">
      <h2>{{ profile.name }}</h2>
      <h5 class="m-0">Vaults: {{ vault.length }}</h5>
      <h5 class="m-0">Keeps: {{ keep.length }}</h5>
    </div>
  </div>
  <div class="row mt-5 pt-5 p-2 ms-1">
    <h1>
      Vaults
      <i
        class="mdi mdi-plus selectable"
        aria-label="create vault"
        data-bs-toggle="modal"
        data-bs-target="#create-vaults-modal"
      ></i>
    </h1>
  </div>
  <div class="row p-2 m-1">
    <div class="col-md-2 col-6 p-2" v-for="v in vault" :key="v.id">
      <Vault :vault="v" />
    </div>
  </div>
  <div class="row p-2 m-1">
    <h1>
      Keeps
      <i
        class="mdi mdi-plus selectable"
        aria-label="create keep"
        data-bs-toggle="modal"
        data-bs-target="#create-keeps-modal"
      ></i>
    </h1>
  </div>
  <div class="row p-2 m-1">
    <div class="col-md-2 col-6 p-2" v-for="k in keep" :key="k.id">
      <AccountKeep :keep="k" />
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'

export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getKeepsByCreatorId(route.params.id)
        await profilesService.getVaultsByCreatorId(route.params.id)

        await profilesService.getProfileById(route.params.id)

      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps),
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keep: computed(() => AppState.keeps),
      vault: computed(() => AppState.vaults),
      accountKeep: computed(() => AppState.keeps),
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
