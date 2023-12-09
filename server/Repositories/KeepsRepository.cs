





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

    internal List<Keep> GetKeepsByVaultId(int vaultId)
    {
        string sql = @"
        SELECT
        kep.*,
        acc.*
        FROM keeps kep
        JOIN accounts acc ON kep.accountId = acc.id
        WHERE kep.vaultId = @vaultId;
        ";

        List<Keep> keeps = _db.Query<Account, Keep, Keep>(sql, (account, keep) =>
        {
            keep.Creator = account;

            return keep;
        }, new { vaultId }).ToList();
        return keeps;
    }
}