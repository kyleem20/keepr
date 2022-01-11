import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

    async getAll() {
        const res = await api.get('api/keeps')
        logger.log('keeps getall', res.data)
        AppState.keeps = res.data
    }

    async getById(id) {
        const res = await api.get(`api/keeps/${id}`)
        logger.log('keep getById', res.data)
        AppState.activeKeep = new Keep(res.data)
    }
    async create(keep) {
        const res = await api.post('api/keeps', keep)
        logger.log('keep create', res.data)
        AppState.keeps.push(new Keep(res.data))
        AppState.activeKeep = new Keep(res.data)
    }
    async edit(keep) {
        const res = await api.put('api/keeps/' + keep.id, keep)
        logger.log('keep edit', res.data)
        const editedKeep = new Keep(res.data)
        AppState.activeKeep = editedKeep
        const index = AppState.keeps.findIndex(k => k.id === editedKeep.id)
        if (index === -1) {
            AppState.keeps.push(editedKeep)
            return
        }
        AppState.keeps.splice(index, 1, editedKeep)
    }
    async remove() {
        const res = await api.delete('api/keeps/' + AppState.activeKeep.id)
        logger.log('keep remove', res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id !== AppState.activeKeep.id)
        AppState.activeKeep = new Keep()
    }
}

export const keepsService = new KeepsService()