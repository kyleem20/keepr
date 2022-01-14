<template>
  <CreateModal id="create-keeps-modal">
    <template #modal-title>
      <div class="bg-secondary">
        <h4>Create a Keep</h4>
      </div>
    </template>
    <template #modal-body>
      <form @submit.prevent="createKeep">
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
              Description...
              <textarea
                type="text"
                aria-label="Keep Description"
                placeholder="Keep Description..."
                class="form-control"
                v-model="editable.description"
                required
              />
            </div>
          </div>

          <div class="dropdown mx-4 my-2"></div>
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
    </template>
  </CreateModal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { watchEffect } from '@vue/runtime-core'
export default {
  props: { keep: { type: Object } },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.keep }
    })
    return {
      editable,
      vault: computed(() => AppState.profileVaults),

      async createKeep() {
        try {
          await keepsService.create(editable.value)
          const newKeep = AppState.activeKeep

        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
</style>