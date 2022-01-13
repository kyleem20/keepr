<template>
  <div class="AccountOffcanvas">
    <div
      class="offcanvas offcanvas-end"
      tabindex="-1"
      id="offcanvasRight"
      aria-labelledby="offcanvasRightLabel"
    >
      <div class="offcanvas-header">
        <h5 id="offcanvasRightLabel">Edit Your Account Info</h5>

        <button
          type="button"
          class="btn-close text-reset"
          data-bs-dismiss="offcanvas"
          aria-label="Close"
        ></button>
      </div>
      <div class="offcanvas-body">
        <div class="row text-center">
          <div class="col">
            <img
              :src="account.picture"
              alt="user photo"
              class="rounded-circle profile-img"
            />
            <h3>{{ account.name }}</h3>
          </div>
        </div>
        <form @submit.prevent="edit">
          <div class="row">
            <div class="mt-4">
              <label for="picture" class="form-label">Account Picture</label>
              <input
                type="url"
                class="form-control"
                name="picture"
                id="picture"
                aria-describedby="picture"
                placeholder="URL..."
                v-model="editable.picture"
              />
            </div>
            <div class="mt-4">
              <label for="name" class="form-label">Account Name</label>
              <input
                type="text"
                class="form-control"
                name="name"
                id="name"
                aria-describedby="name"
                placeholder="Name..."
                v-model="editable.name"
              />
            </div>
          </div>
          <div class="modal-footer">
            <button type="submit" class="btn btn-secondary">Save</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { logger } from '../utils/Logger'
import { accountService } from '../services/AccountService'
import Pop from '../utils/Pop'
import { watchEffect } from '@vue/runtime-core'
import { AppState } from "../AppState"
import { AuthService } from "../services/AuthService"
import { useRouter } from "vue-router"
export default {
  setup() {
    const editable = ref({})
    const router = useRouter()
    watchEffect(() => {
      editable.value = {}
    })
    return {
      account: computed(() => AppState.account),
      editable,
      async edit() {
        try {
          await accountService.edit(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast('Failed to Edit', 'error')
        }
      },
      async logout() {
        router.push({ name: 'HomePage' })
        AuthService.logout()
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-img {
  height: 75px;
  width: 75px;
  object-fit: cover;
}
</style>