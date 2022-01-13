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
                <div class="col-12">
                  <div class="row">
                    <div class="col-md-4 mdi mdi-eye text-secondary">
                      {{ keep.views }}
                    </div>
                    <div class="col-md-4 mdi mdi-pin text-secondary">
                      {{ keep.keeps }}
                    </div>
                    <div class="col-md-4 mdi mdi-share text-secondary">
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
                        col-5
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
                        {{ myVaults }}
                      </button>
                      <ul
                        class="dropdown-menu"
                        aria-labelledby="dropdownMenuButton1"
                      >
                        <li v-for="v in vault" :key="v.id">
                          <div
                            class="dropdown-item selectable"
                            @click="myVaults = v.name"
                          >
                            {{ v.name }}
                          </div>
                        </li>
                      </ul>
                    </div>
                    <h5
                      class="col-2 pt-2 px-0 mx-0"
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
                    <p class="col-5 pt-3" v-if="keep.creator">
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
import { AuthService } from '../services/AuthService'
export default {


  setup(props) {
    const myVaults = ref('Add to Vault')
    return {
      myVaults,
      keep: computed(() => AppState.activeKeep),
      profile: computed(() => AppState.profiles.filter(p => p.id === props.keep.creatorId)),
      vault: computed(() => AppState.vaults.filter(v => v.creatorId == AuthService.userInfo.id)),
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
      routeToProfile() {
        Modal.getOrCreateInstance(document.getElementById('keeps-modal')).hide()
        router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>