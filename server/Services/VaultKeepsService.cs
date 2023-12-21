

namespace keeperCheckpoint.Services;
public class VaultKeepsService
{
    private readonly VaultKeepsRepository _vaultKeepsRepository;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    private readonly KeepsRepository _keepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService, KeepsRepository keepsRepository)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
        _keepsRepository = keepsRepository;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {

        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("not your vault to use!");
        }






        VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
        // if (vaultKeep.CreatorId != userId)
        // {
        //     throw new Exception("not your vault");
        // }
        Keep keep = _keepsService.GetKeepById(vaultKeepData.KeepId, userId);
        keep.Kept++;
        _keepsRepository.EditKeep(keep);
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
        // Keep keep = _keepsService.GetKeepById(userId);
        // keep.Kept--;
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);

        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception("do not try it!");
        }
        Keep keep = _keepsService.GetKeepById(vaultKeep.KeepId, userId);
        keep.Kept--;
        _keepsRepository.EditKeep(keep);
        _vaultKeepsRepository.DestroyVaultKeep(vaultKeepId);
        return "it really is gone!";

    }
}