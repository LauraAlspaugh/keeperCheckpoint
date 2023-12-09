namespace keeperCheckpoint.Models;
public class VaultKeep
{
    public int Id { get; set; }

    public string CreatorId { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public Account Creator { get; set; }
    public string AccountId { get; set; }
}