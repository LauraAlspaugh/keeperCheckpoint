

namespace keeperCheckpoint.Services;
public class VaultKeepsService
{
    private readonly VaultKeepsRepository _vaultKeepsRepository;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
        _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {

        _vaultsService.GetVaultById(vaultKeepData.VaultId, userId);






        VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
        // if (vaultKeep.CreatorId != userId)
        // {
        //     throw new Exception("not your vault");
        // }

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