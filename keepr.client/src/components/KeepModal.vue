<template>
  <Modal id="keeps-modal" size="modal-xl">
    <template #modal-body>
      <div class="container-fluid p-0 m-0">
        <div class="row m-0 p-0">
          <div class="col-md-6 m-0 p-0">
            <img
              height="500"
              class="w-100 image-fit-contain"
              :src="keep.img"
              alt=""
            />
          </div>
          <div class="col-md-6">
            <div class="row">
              <div class="text-end">
                <button
                  type="button"
                  class="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div
                class="
                  text-center
                  d-flex
                  flex-column
                  justify-content-center
                  align-items-between
                "
              >
                <div class="col-12 pt-2">
                  <div class="row">
                    <div class="col-4 mdi mdi-eye text-secondary">
                      {{ keep.views }}
                    </div>
                    <div class="col-4 mdi mdi-pin text-secondary">
                      {{ keep.keeps }}
                    </div>
                    <div class="col-4 mdi mdi-share text-secondary">
                      {{ keep.shares }}
                    </div>
                  </div>
                </div>
                <div class="info col-12">
                  <h2 class="pt-5 mb-2">{{ keep.name }}</h2>
                  <p class="pt-5 ps-3 text-grey text-start darken-30">
                    {{ keep.description }}
                  </p>
                </div>
                <div
                  class="col-12 d-flex justify-content-between align-items-end"
                >
                  <div class="row m-0 p-0 d-flex flex-row">
                    <div
                      class="col-md-5 py-3"
                      v-if="keep.vaultKeepId && keep.creatorId === account.id"
                    >
                      <button
                        class="p-0 m-0 btn btn-outline btn-secondary selectable"
                        @click="deleteVaultKeep(keep.vaultKeepId)"
                      >
                        Remove From Vault
                      </button>
                    </div>
                    <div class="dropdown col-md-5 p-3">
                      <button
                        class="btn btn-secondary dropdown-toggle"
                        type="button"
                        id="dropdownMenuButton1"
                        data-bs-toggle="dropdown"
                        aria-expanded="false"
                        required
                      >
                        {{ myVaults }}
                      </button>
                      <ul
                        class="dropdown-menu"
                        aria-labelledby="dropdownMenuButton1"
                      >
                        <li
                          v-for="v in vault"
                          :key="v.id"
                          @click="createVaultKeep"
                        >
                          <div class="dropdown-item selectable">
                            <div
                              @click="
                                myVaults = v.name;
                                selectedVault = v.id;
                              "
                            >
                              {{ v.name }}
                            </div>
                          </div>
                        </li>
                      </ul>
                    </div>
                    <h5
                      class="col-md-2 pt-2 px-0 mx-0 mt-3"
                      aria-label="delete keep"
                      v-if="keep.creatorId === account.id"
                      @click="deleteKeep"
                    >
                      <i
                        class="
                          mdi mdi-delete-outline
                          m-0
                          py-1
                          selectable
                          text-danger
                        "
                      ></i>
                    </h5>

                    <div class="col-md-6 m-0 selectable">
                      <div class="rd-flex justify-content-center m-0">
                        <p
                          class="text-center pt-3"
                          v-if="keep.creator"
                          @click="routeToProfile"
                        >
                          <em class="f-10 d-flex align-items-center">
                            <img
                              :src="keep.creator.picture"
                              alt="profile picture"
                              class="rounded w-25 me-2 d-flex selectable"
                            />

                            {{ keep.creator.name }}
                          </em>
                        </p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService'

export default {


  setup() {
    const router = useRouter()

    const myVaults = ref('Add to Vault')
    const selectedVault = ref()
    return {
      selectedVault,
      myVaults,
      keep: computed(() => AppState.activeKeep),
      profile: computed(() => AppState.profiles.filter(p => p.id === keep.creatorId)),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vaults.filter(v => v.creatorId === AppState.account.id)),
      async createVaultKeep() {
        try {
          logger.log('selected vault', myVaults.value)
          const vaultSelected = selectedVault.value
          const found = AppState.vaults.find(v => v.id === vaultSelected)
          const keepId = AppState.activeKeep.id
          const vaultKeepObject = { vaultId: vaultSelected, keepId: keepId }
          await vaultKeepsService.createVK(vaultKeepObject)
          Pop.toast('You Added this keep to a vault', 'success')
          // myVaults = 'Add to Vault'

        } catch (error) {
          logger.error(error)
          Pop.toast('Failed to create a new vault keep', 'error')
        }
      }
      ,
      async deleteVaultKeep(vkId) {
        try {
          const agree = await Pop.confirm('Remove Keep from Vault?')
          if (!agree) { return }
          await vaultKeepsService.remove(vkId)
          await vaultKeepsService.getKeepsForVault(AppState.activeVault.id)

          Modal.getOrCreateInstance(document.getElementById('keeps-modal')).hide()

        } catch (error) {
          logger.error(error)
          Pop.toast('Failed to remove from vault', 'error')
        }
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.remove()
            Modal.getOrCreateInstance(document.getElementById('keeps-modal')).hide()
          }
        }
        catch (error) {
          logger.error(error)
          Pop.toast('Failed to delete keep', 'error')
        }
      },
      routeToProfile() {
        Modal.getOrCreateInstance(document.getElementById('keeps-modal')).hide()
        router.push({ name: 'Profile', params: { id: AppState.activeKeep.creatorId } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>