namespace keeperCheckpoint.Models;

public class Account
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Picture { get; set; }
  public int VaultId { get; set; }
  public int VaultKeepId { get; set; }
}
