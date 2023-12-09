
namespace keeperCheckpoint.Services;
public class ProfilesService
{
    private readonly ProfilesRepository _profilesRepository;

    public ProfilesService(ProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    internal Profile GetProfileById(int profileId)
    {
        Profile profile = _profilesRepository.GetProfileById(profileId);
        if (profile == null)
        {
            throw new Exception("not a valid profile id");

        }
        return profile;
    }
}