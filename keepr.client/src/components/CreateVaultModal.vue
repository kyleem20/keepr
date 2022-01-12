<template>
  <CreateModal id="create-vaults-modal">
    <template #modal-title>
      <div class="bg-secondary">
        <h4>Create a Vault</h4>
      </div>
    </template>
    <template #modal-body>
      <div class="component">
        <form @submit.prevent="createVault">
          <div class="modal-body">
            <div class="row">
              <div class="col m-2">
                Title...
                <input
                  type="text"
                  aria-label="title"
                  placeholder="Title..."
                  class="form-control"
                  v-model="editable.name"
                  required
                />
              </div>
            </div>
            <div class="row">
              <div class="col m-2">
                Image URL...
                <input
                  type="text"
                  aria-label="image"
                  placeholder="URL..."
                  class="form-control"
                  v-model="editable.img"
                  required
                />
              </div>
            </div>
            <div class="row">
              <div class="col m-2">
                <input
                  class="form-check-input"
                  type="checkbox"
                  value=""
                  id="defaultCheck1"
                  aria-label="Vault is Private"
                  v-model="editable.isPrivate"
                />
                Private?
                <p class="text-grey f-10">
                  Private Vaults can only be seen by you
                </p>
              </div>
            </div>
            <!-- <div class="row">
                  <div class="col m-2">
                    Description...
                    <textarea
                      type="text"
                      aria-label="Vault Description"
                      placeholder="Vault Description..."
                      class="form-control"
                      v-model="editable.description"
                      required
                    />
                  </div>
                </div> -->

            <div class="dropdown mx-4 my-2">
              <!-- <button
                class="btn btn-secondary dropdown-toggle"
                type="button"
                id="dropdownMenuButton1"
                data-bs-toggle="dropdown"
                aria-expanded="false"
                required
              >
                {{ vault }}
              </button>
              <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li v-for="vault in vaults" :key="vault.id">
                  <div
                    class="dropdown-item selectable"
                    @click="vault = activeVault"
                  >
                    {{ vault.name }}
                  </div>
                </li>
              </ul> -->
            </div>
          </div>

          <div class="modal-footer">
            <button
              type="submit"
              class="btn btn-outline-secondary"
              data-bs-dismiss="modal"
            >
              Create
            </button>
          </div>
        </form>
      </div>
    </template>
  </CreateModal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { watchEffect } from '@vue/runtime-core'
export default {
  props: { vault: { type: Object } },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.vault }
    })
    return {
      editable,
      vault: computed(() => AppState.profileVaults),

      async createVault() {
        try {
          await vaultsService.create(editable.value)
          const newVault = AppState.activeVault

        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }

      // async createGameNight() {

      //     try {
      //       const game = gameQueue.value
      //       await gameNightService.createGameNight(state.editable)
      //       const gameNight = AppState.activeGameNight
      //       await playersService.attendGameNight(gameNight.pin)

      //       const found = AppState.myGames.find(g => g.name === game)
      //       let gameObject = { gameId: found.id, gameNightId: AppState.activeGameNight.id }
      //       await gameQueuesService.addToGameQueue(gameObject)
      //       router.push({
      //         name: "GameNightDetails",
      //         params: { id: AppState.activeGameNight.id }
      //       })
      //       state.editable = {}

      //     } catch (error) {
      //       logger.error(error)
      //       Pop.toast(error.message, 'error')
      //     }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>