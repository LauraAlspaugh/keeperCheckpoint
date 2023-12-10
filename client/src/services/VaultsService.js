import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
    async getVaults(profileId) {
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('getting vaults', res.data)
        AppState.vaults = res.data.map((vault) => new Vault(vault))
    }
    async getVaultById(vaultId) {
        AppState.activeVault = null
        const res = await api.get(`api/vaults/${vaultId}`)
        logger.log('getting vault by id', res.data)
        AppState.activeVault = new Vault(res.data)

    }
    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('getting keeps by vault id', res.data)
        AppState.keeps = res.data.map((keep) => new Keep(keep))
    }
}
export const vaultsService = new VaultsService()