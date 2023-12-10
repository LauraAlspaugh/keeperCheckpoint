import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
    async getVaults(profileId) {
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('getting vaults', res.data)
        AppState.vaults = res.data.map((vault) => new Vault(vault))
    }
}
export const vaultsService = new VaultsService()