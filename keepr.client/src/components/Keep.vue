<template>
  <div class="keep">
    <div
      class="p-2"
      :title="keep.name"
      data-bs-toggle="modal"
      data-bs-target="#keeps-modal"
      @click="setActive"
    >
      <div class="row h-50 bg-light elevation-2 rounded selectable">
        <img
          :src="keep.img"
          :alt="keep.name"
          class="p-0 object-fit-cover w-100 rounded-top"
        />
        <div
          class="
            py-3
            d-flex
            justify-content-between
            align-content-center
            text-center
          "
        >
          <h5 class="m-0 col-10">
            <b>{{ keep.name }}</b>
          </h5>

          <img
            class="m-0 col-2 align-content-center rounded-pill"
            :src="keep.creator.picture"
            height="35"
          />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { Modal } from 'bootstrap'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { useRouter } from 'vue-router'
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    const router = useRouter()

    return {
      async setActive() {
        try {
          AppState.activeKeep = props.keep
          await keepsService.getById(props.keep.id)
        }
        catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById('keeps-modal')).hide()
          Pop.toast(error, 'error')
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