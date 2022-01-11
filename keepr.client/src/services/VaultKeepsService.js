import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {

    // Probably don't need this function
    async getById(id) {
        const res = await api.get(`api/vaultkeeps/${id}`)
        logger.log('keep getById', res.data)
        AppState.vaultKeeps = new VaultKeep(res.data)
    }
    async getKeepsForVault(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        logger.log('keep by vault id', res.data)
        AppState.keeps = res.data.map(vk => new VaultKeep(k))
    }
    async createVK(vaultKeep) {
        const res = await api.post('api/vaultkeeps', vaultKeep)
        AppState.vaultKeeps = new VaultKeep(res.data)
        AppState.vaultKeep.unshift(new VaultKeep(res.data))
    }


    async remove(id) {
        const res = await api.delete('api/vaultkeeps/' + id)
        logger.log('keep remove', res.data)
        AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.id !== AppState.vaultKeeps.id)
    }
}

export const vaultKeepsService = new VaultKeepsService()