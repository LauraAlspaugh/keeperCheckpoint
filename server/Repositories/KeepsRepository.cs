






namespace keeperCheckpoint.Repositories;
public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
    INSERT INTO 
    keeps(name, description, img, views, kept, creatorId)
    VALUES(@Name, @Description, @Img, @Views, @Kept, @CreatorId);
    SELECT 
    kep.*,
    acc.*
    FROM keeps kep 
    JOIN accounts acc ON kep.creatorId = acc.id
    WHERE kep.id = LAST_INSERT_ID();
";
        Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, keepData).FirstOrDefault();
        return keep;
    }

    internal void DestroyKeep(int keepId)
    {
        string sql = @"
      DELETE FROM keeps 
      WHERE id = @keepId LIMIT 1;
      SELECT kep.*,
      acc.*
      FROM keeps kep
      JOIN accounts acc ON kep.creatorId = acc.id
      WHERE kep.id = @keepId;
      ";
        _db.Execute(sql, new { keepId });
    }

    internal Keep EditKeep(Keep keep)
    {
        string sql = @"
        UPDATE keeps
        SET 
        name = @Name,
        description = @Description
        WHERE id = @Id;

        SELECT 
        kep.*,
        acc.*
        FROM keeps kep
        JOIN accounts acc ON kep.creatorId = acc.id
        WHERE kep.id = @Id;
        ";

        Keep newKeep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, keep).FirstOrDefault();
        return newKeep;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
       SELECT 
       kep.*,
       acc.*
       FROM keeps kep
       JOIN accounts acc ON kep.creatorId = acc.id
       WHERE kep.id = @keepId;
       ";
        Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
    SELECT 
    kep.*,
    acc.*
    FROM keeps kep
    JOIN accounts acc ON kep.creatorId = acc.id
    ";
        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }



    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        string sql = @"
        SELECT 
        kep.*,
        acc.*
        FROM keeps kep
        JOIN accounts acc ON kep.creatorId = acc.id
        WHERE kep.id = @profileId;
        ";
        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, new { profileId }).ToList();
        return keeps;
    }

    internal List<VaultKeepKeep> GetKeepsByVaultId(int vaultId)
    {
        string sql = @"
        SELECT
        valkep.*,
        acc.*
        FROM vaultkeeps valkep
        JOIN accounts acc ON valkep.creatorId = acc.id
        WHERE valkep.vaultId = @vaultId;
        ";

        List<VaultKeepKeep> keeps = _db.Query<Account, VaultKeepKeep, VaultKeepKeep>(sql, (account, keep) =>
        {

            // keep.VaultKeepId = account.VaultId;
            // keep.VaultId = account.VaultId;
            keep.KeepId = account.VaultKeepId;
            keep.Creator = account;

            return keep;
        }, new { vaultId }).ToList();
        return keeps;
    }
}