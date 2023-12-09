
namespace keeperCheckpoint.Repositories;
public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetProfileById(string profileId)
    {
        string sql = "SELECT * FROM profiles WHERE id = @id";
        return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();

    }
}