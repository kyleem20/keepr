<template>
  <Modal id="keeps-modal" size="modal-xl">
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-md-6">
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
                <div class="col-12">
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
                  <p class="pt-5 text-grey darken-30">{{ keep.description }}</p>
                </div>
                <div class="col-12">
                  <div class="row">
                    <div
                      class="
                        dropdown
                        col-md-5
                        p-0
                        d-flex
                        flex-row
                        justify-content-end
                      "
                    >
                      <button
                        class="btn btn-secondary dropdown-toggle"
                        type="button"
                        id="dropdownMenuButton1"
                        data-bs-toggle="dropdown"
                        aria-expanded="false"
                        required
                      >
                        <div v-if="keep.vaultKeepId" @click="removeFromVault">
                          Remove From Vault
                        </div>
                        <div v-else>
                          {{ myVaults }}
                        </div>
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
                      class="col-md-2 pt-2 px-0 mx-0"
                      aria-label="delete"
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
                    <p
                      class="col-md-5 pt-3 selectable"
                      v-if="keep.creator"
                      @click="routeToProfile"
                    >
                      <em class="d-flex align-content-end f-10">
                        <img
                          :src="keep.creator.picture"
                          alt="profile picture"
                          class="rounded w-25 me-2"
                        />
                        <p class="d-flex align-self-center">
                          {{ keep.creator.name }}
                        </p>
                      </em>
                    </p>
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

        } catch (error) {
          logger.error(error)
          Pop.toast('Failed to create a new vault keep', 'error')
        }
      }
      ,
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