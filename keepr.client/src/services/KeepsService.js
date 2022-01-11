import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

    async getAll() {
        const res = await api.get('api/keeps')
        logger.log(res.data)
        AppState.keeps = res.data
    }
    async getKeepsByCreatorId(id) {
        const res = await api.get(`api/profiles/${id}/keeps`)
        AppState.profileKeeps = res.data
    }
}

export const keepsService = new KeepsService()