<template>
  <div class="component">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header bg-secondary">
          <h5 class="modal-title" id="exampleModalToggleLabel">
            Create a Vault
          </h5>
          <button
            type="button"
            class="btn-close bg-secondary"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <form @submit.prevent="createVault()">
          <div class="modal-body">
            <div class="row">
              <div class="col m-2">
                <input
                  type="text"
                  aria-label="name"
                  placeholder="Name your Vault"
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
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>