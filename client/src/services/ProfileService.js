import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Profile.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfileService {
    async getProfile(profileId) {
        AppState.profile = null
        const res = await api.get(`api/profiles/${profileId}`)
        logger.log(res.data)
        AppState.profile = new Profile(res.data)
    }
    async getKeepsByProfileId(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log(res.data)
        AppState.keeps = res.data.map((keep) => new Keep(keep))


    }
}
export const profileService = new ProfileService()