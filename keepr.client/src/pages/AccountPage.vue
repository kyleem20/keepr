<template>
  <div class="about row p-3 mb-5">
    <div class="col-md-2 p-0">
      <img
        class="rounded text-center ms-5 mb-0 w-100 image-fit-contain"
        :src="account.picture"
        :alt="account.name"
        height="100"
      />
    </div>
    <div class="col-md-8">
      <h2>{{ account.name }}</h2>
      <h5 class="m-0">Vaults:</h5>
      <h5 class="m-0">Keeps:</h5>
    </div>
  </div>
  <div class="row mt-5 pt-5 p-2">
    <h2>
      Vaults
      <i
        class="mdi mdi-plus selectable"
        aria-label="create vault"
        @click="createKeep"
      ></i>
    </h2>
  </div>
  <div class="row p-2">
    <!-- <Vault /> -->
  </div>
  <div class="row p-2">
    <h2>
      Keeps
      <i
        class="mdi mdi-plus selectable"
        aria-label="create keep"
        @click="createKeep"
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
      keep: computed(() => AppState.keeps),
      accountKeep: computed(() => AppState.keeps)

    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
