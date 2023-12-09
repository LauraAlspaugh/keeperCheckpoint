import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log('getting keeps', res.data)
        AppState.keeps = res.data.map(pojo => new Keep(pojo))
    }
    setActiveKeep(keepProp) {

        AppState.activeKeep = keepProp
    }
    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        logger.log('created a keep!', res.data)
        const newKeep = new Keep(res.data)
        AppState.keeps.push(newKeep)
        return newKeep
    }
}
export const keepsService = new KeepsService()