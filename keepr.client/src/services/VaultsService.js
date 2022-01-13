import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

    async getAll() {
        const res = await api.get('account/vaults')
        // logger.log('vault getAll', res.data)
        AppState.vaults = res.data
    }
    async getById(id) {
        const res = await api.get(`api/vaults/${id}`)
        // logger.log('vault getById', res.data)
        AppState.activeVault = res.data
    }
    async getByCreatorId(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        AppState.profileVaults = res.data
    }
    async create(vault) {
        const res = await api.post('api/vaults', vault)
        // logger.log('vault create', res.data)
        AppState.vaults.push(res.data)
        AppState.activeVault = res.data
    }
    async edit(vault) {
        const res = await api.put('api/vaults/' + vault.id, vault)
        // logger.log('vault edit', res.data)
        const editedVault = res.data
        AppState.activeVault = editedVault
        const index = AppState.vaults.findIndex(v => v.id === editedVault.id)
        if (index === -1) {
            AppState.vaults.push(editedVault)
            return
        }
        AppState.vaults.splice(index, 1, editedVault)
    }
    async remove() {
        const res = await api.delete('api/vaults/' + AppState.activeVault.id)
        // logger.log('vault remove', res.data)
        AppState.vaults = AppState.vaults.filter(v => v.id !== AppState.activeVault.id)
        // AppState.activeVault = 
    }

}

export const vaultsService = new VaultsService()