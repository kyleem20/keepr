<template>
  <!-- <div class="modal">
    <div class="modal" tabindex="-1">
      <div class="modal-dialog modal-xl">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              <slot name="modal-title"> </slot>
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <slot name="modal-body" />
          </div>
        </div>
      </div>
    </div>
  </div> -->
  <div class="modal">
    <div class="modal-dialog" :class="size">
      <div class="modal-content">
        <div class="modal-body">
          <slot name="modal-body"></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    size: {
      type: String,
      default: 'modal-dialog'
    }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>