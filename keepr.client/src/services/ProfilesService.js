import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {


    async getVaultsByCreatorId(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        AppState.profileVaults = res.data
    }
    async getKeepsByCreatorId(id) {
        const res = await api.get(`api/profiles/${id}/keeps`)
        AppState.profileKeeps = res.data
    }
}

export const profilesService = new ProfilesService()