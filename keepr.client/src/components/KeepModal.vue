<template>
  <div class="component">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header bg-secondary">
          <h5 class="modal-title" id="exampleModalToggleLabel">
            Create a Keep
          </h5>
          <button
            type="button"
            class="btn-close bg-secondary"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <form @submit.prevent="createKeep()">
          <div class="modal-body">
            <div class="row">
              <div class="col m-2">
                <input
                  type="text"
                  aria-label="name"
                  placeholder="Name your Keep"
                  class="form-control"
                  v-model="state.editable.name"
                  required
                />
              </div>
            </div>
            <div class="row">
              <div class="col m-2">
                <input
                  type="text"
                  aria-label="image"
                  placeholder="Image"
                  class="form-control"
                  v-model="state.editable.img"
                  required
                />
              </div>
            </div>

            <div class="dropdown mx-4 my-2">
              <button
                class="btn btn-secondary dropdown-toggle"
                type="button"
                id="dropdownMenuButton1"
                data-bs-toggle="dropdown"
                aria-expanded="false"
                required
              >
                {{ keep }}
              </button>
              <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li v-for="keep in keeps" :key="keep.id">
                  <div
                    class="dropdown-item selectable"
                    @click="keep = activeVault"
                  >
                    {{ keep.name }}
                  </div>
                </li>
              </ul>
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
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
    }
  }
}
</script>


<style lang="scss" scoped>
</style>