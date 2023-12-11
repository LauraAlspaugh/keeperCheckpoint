import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log('getting keeps', res.data)
        AppState.keeps = res.data.map(pojo => new Keep(pojo))
    }
    async setActiveKeep(keepProp) {
        const res = await api.get(`api/keeps/${keepProp.id}`)
        logger.log('getting keep by id', res.data)

        AppState.activeKeep = keepProp
    }
    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        logger.log('created a keep!', res.data)
        const newKeep = new Keep(res.data)
        AppState.keeps.push(newKeep)
        return newKeep
    }
    async destroyKeep(keepId) {
        const res = await api.delete(`api/keeps/${keepId}`)
        logger.log('destroying keep', res.data)
        AppState.keeps = AppState.keeps.filter((keep) => keep.id != keepId)
    }
    async getKeepById(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        logger.log("getting keep by id", res.data)
        const newKeep = new Keep(res.data)
        AppState.activeKeep = newKeep
    }
    clearAppState() {
        AppState.activeKeep = null
    }
    async createVaultKeep(vaultId, keepId) {

        const res = await api.post('api/vaultkeeps/', { vaultId, keepId })
        logger.log('creating a vaultkeep', res.data)
        AppState.vaultKeeps.push(new VaultKeep(res.data))
    }
}
export const keepsService = new KeepsService()