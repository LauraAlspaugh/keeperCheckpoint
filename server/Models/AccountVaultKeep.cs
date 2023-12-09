namespace keeperCheckpoint.Models;
public class AccountVaultKeep : Account
{
    public int VaultKeepId { get; set; }
    public int KeepId { get; set; }
    public int VaultId { get; set; }
}