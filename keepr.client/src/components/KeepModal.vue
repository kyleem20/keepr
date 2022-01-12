<template>
  <Modal id="keeps-modal" size="modal-xl">
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-6">
            <img
              height="500"
              class="w-100 image-fit-contain"
              :src="keep.img"
              alt=""
            />
          </div>
          <div class="col-6">
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
                <div class="info col-12">
                  <h2 class="pt-5">{{ keep.name }}</h2>
                  <h4>{{ keep.description }}</h4>
                </div>
                <div class="col-12">
                  <div class="row d-flex align-items-center">
                    <div class="dropdown col-5 my-2">
                      <button
                        class="btn btn-secondary dropdown-toggle"
                        type="button"
                        id="dropdownMenuButton1"
                        data-bs-toggle="dropdown"
                        aria-expanded="false"
                        required
                      >
                        Add to Vault
                      </button>
                      <ul
                        class="dropdown-menu"
                        aria-labelledby="dropdownMenuButton1"
                      >
                        <li v-for="v in vault" :key="v.id">
                          <div
                            class="dropdown-item selectable"
                            @click="vault = vault.name"
                          >
                            {{ vault.name }}
                          </div>
                        </li>
                      </ul>
                    </div>
                    <h5
                      class="col-2 pt-2"
                      aria-label="delete"
                      v-if="keep.creatorId === account.id"
                      @click="deleteKeep"
                    >
                      <i
                        class="
                          m-0
                          p-1
                          selectable
                          mdi mdi-delete-outline
                          text-danger
                        "
                      ></i>
                    </h5>
                    <p class="col-5 ps-2 pt-3">
                      <em class="d-flex align-content-end flex-end">
                        Creator Name
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
  <!-- <Modal id="keeps-modal" size="modal-xl">
    <template #modal-body>
      <div class="container-fluid">
        <button
          type="button"
          class="btn-close"
          data-bs-dismiss="modal"
          aria-label="Close"
        ></button>
        <div class="row">
          <div class="col-6">
            <img
              height="500"
              class="w-100 object-fit-cover"
              :src="keep.img"
              :alt="keep.name"
            />
          </div>
        </div>
      </div>
      <div class="col-6 text-center d-flex flex-column justify-content-center">
        <div class="info">
          <h2>
            {{ keep.name }}
          </h2>
          <h4>{{ keep.description }}</h4>
          <p>
            <img
              class="m-0 col-2 align-content-center rounded-pill"
              :src="keep.creator.picture"
            />
          </p>
        </div>
      </div>
    </template>
  </Modal> -->
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
      vault: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
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
    }
  }
}
</script>


<style lang="scss" scoped>
</style>