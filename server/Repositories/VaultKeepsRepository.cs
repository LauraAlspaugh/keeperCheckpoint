


namespace keeperCheckpoint.Repositories;
public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
       INSERT INTO vaultkeeps(keepId, vaultId, creatorId)
       VALUES(@KeepId, @VaultId, @CreatorId);

       SELECT * FROM vaultkeeps
       WHERE id = LAST_INSERT_ID();
       ";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal void DestroyVaultKeep(int vaultKeepId)
    {
        string sql = "DELETE FROM vaultkeeps WHERE id = @vaultkeepId LIMIT 1;";
        _db.Execute(sql, new { vaultKeepId });
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = "SELECT * FROM vaultkeeps WHERE id = @vaultkeepId;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }
}