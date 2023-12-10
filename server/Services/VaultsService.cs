


namespace keeperCheckpoint.Services;
public class VaultsService
{
    private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository vaultsRepository)
    {
        _vaultsRepository = vaultsRepository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _vaultsRepository.CreateVault(vaultData);
        return vault;
    }

    internal string DestroyVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId, userId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("do not even try it!");
        }
        _vaultsRepository.DestroyVault(vaultId);
        return "it really is gone bro!";
    }

    internal Vault EditVault(int vaultId, string userId, Vault vaultData)
    {
        Vault vault = GetVaultById(vaultId, userId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("not your vault to edit!");
        }
        vault.Name = vaultData.Name ?? vault.Name;
        vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;
        _vaultsRepository.EditVault(vault);
        return vault;
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        List<Vault> vaults = _vaultsRepository.GetMyVaults(userId);
        return vaults;
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _vaultsRepository.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception("not a valid vault id");

        }
        if (vault.IsPrivate == true && vault.CreatorId != userId)
        {
            throw new Exception("There has been an issue...");
        }
        return vault;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
        List<Vault> vaults = _vaultsRepository.GetVaultsByProfileId(profileId);
        vaults = vaults.FindAll(vault => vault.IsPrivate == false);
        return vaults;
    }
}