


using System.Reflection.Metadata.Ecma335;

namespace keeperCheckpoint.Repositories;
public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
      INSERT INTO 
      vaults(name, description, img, creatorId )
      VALUES(@Name, @Description, @Img, @CreatorId);
      SELECT 
      val.*,
      acc.*
      FROM vaults val 
      JOIN accounts acc ON val.creatorId = acc.id
      WHERE val.id = LAST_INSERT_ID();
      ";
        Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    internal void DestroyVault(int vaultId)
    {
        string sql = @"
      DELETE FROM vaults
      WHERE id = @vaultId LIMIT 1;
      SELECT 
      val.*,
      acc.*
      FROM vaults val
      JOIN accounts acc ON val.creatorId = acc.id
      WHERE val.id = @vaultId;
      ";
        _db.Execute(sql, new { vaultId });
    }

    internal Vault EditVault(Vault vault)
    {
        string sql = @"
UPDATE vaults
SET 
name = @Name,
isPrivate = @IsPrivate
WHERE id = @Id;

SELECT 
val.*,
acc.*
FROM vaults val
JOIN accounts acc ON val.creatorId = acc.id
WHERE val.id = @Id;
";

        Vault newVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, vault).FirstOrDefault();
        return newVault;
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        string sql = @"
       SELECT 
       val.*,
       acc.*
       FROM vaults val
       JOIN accounts acc ON acc.id = val.creatorId
       WHERE val.accountId = @userid;
       ";

        List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { userId }).ToList();
        return vaults;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
      SELECT 
      val.*,
      acc.*
      FROM vaults val
      JOIN accounts acc ON val.creatorId = acc.id
    WHERE  val.id = @vaultId;
       ";
        Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }
}