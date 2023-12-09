

namespace keeperCheckpoint.Services;
public class VaultKeepsService
{
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception("not a valid vault keep");

        }
        return vaultKeep;
    }

    internal string DestroyVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception("do not try it!");
        }
        _vaultKeepsRepository.DestroyVaultKeep(vaultKeepId);
        return "it really is gone!";

    }
}