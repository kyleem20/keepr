<template>
  <div class="modal" tabindex="-1">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header bg-secondary">
          <h4 class="p-2 modal-title">
            <slot name="modal-title"></slot>
          </h4>
          <button
            type="button"
            class="btn-close me-2"
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
</template>