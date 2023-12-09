
namespace keeperCheckpoint.Repositories;
public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetProfileById(int profileId)
    {
        string sql = @"
        SELECT 
       pro.*,
       acc.*
       FROM profiles pro
       JOIN accounts acc ON pro.creatorId = acc.id
       WHERE pro.id = @profileId;
       ";
        Profile profile = _db.Query<Profile, Account, Profile>(sql, (profile, account) =>
        {
            profile.Creator = account;
            return profile;
        }, new { profileId }).FirstOrDefault();
        return profile;

    }
}