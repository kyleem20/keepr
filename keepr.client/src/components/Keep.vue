<template>
  <div class="keep">
    <div
      class="p- bg-light elevation-2 rounded selectable"
      :title="keep.name"
      data-bs-toggle="modal"
      data-bs-target="#keeps-modal"
      @click="setActive"
      :style="{ backgroundImage: `url(${keep.img})` }"
    >
      <img
        :src="keep.img"
        :alt="keep.name"
        height="300"
        class="p-0 object-fit-cover w-100 rounded-top img-fluid img-cover"
      />
      <div
        class="
          p-3
          d-flex
          justify-content-between
          align-content-center
          text-center
        "
      >
        <h5 class="m-0 col-10 text-light text-shadow shadowed">
          <b>{{ keep.name }}</b>
        </h5>

        <img
          class="m-0 col-2 align-content-center rounded-circle"
          :src="keep.creator.picture"
        />
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
.transparent {
  background: linear-gradient(
    to right,
    transparent 10%,
    #f6f6f6 50%,
    transparent 100%
  );
}
.img {
  max-width: 100%;
  height: auto;
}
.keep {
  min-height: 200px;
}
.shadowed {
  text-shadow: 2px 1px #000000b0;
}
</style>