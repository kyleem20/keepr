<template>
  <div class="component">
    <div
      class="vault p-2"
      :title="keep.name"
      data-bs-toggle="modal"
      data-bs-target="#vault-modal"
      @click="setActive"
    >
      <div class="bg-light elevation-1 rounded selectable">
        <img
          :src="keep.img"
          :alt="keep.name"
          class="object-fit-cover w-100 p-1 rounded"
        />
        <div class="row d-flex p-2 justify-content-between text-center">
          <h3 class="col-12">{{ keep.name }}</h3>

          <p>
            <b class="col-12" @click="routeToProfile">
              ~ {{ keep.creator.name }}</b
            >
          </p>
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
          Modal.getOrCreateInstance(document.getElementById('vault-modal')).hide()
          Pop.toast(error, 'error')
        }
      },
      routeToProfile() {
        Modal.getOrCreateInstance(document.getElementById('vault-modal')).hide()
        router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>