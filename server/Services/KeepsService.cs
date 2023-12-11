


namespace keeperCheckpoint.Services;
public class KeepsService
{
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
        _keepsRepository = keepsRepository;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _keepsRepository.CreateKeep(keepData);
        return keep;
    }

    internal string DestroyKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId, userId);
        if (keep.CreatorId != userId)
        {
            throw new Exception("Don't even try it!");
        }
        _keepsRepository.DestroyKeep(keepId);
        return "This keep has been destroyed!";
    }

    internal Keep EditKeep(int keepId, string userId, Keep keepData)
    {
        Keep keep = GetKeepById(keepId, userId);
        if (keep.CreatorId != userId)
        {
            throw new Exception("Not your keep to edit!");
        }
        keep.Name = keepData.Name ?? keep.Name;
        keep.Description = keepData.Description ?? keep.Description;
        _keepsRepository.EditKeep(keep);
        return keep;


    }

    internal Keep GetKeepById(int keepId, string userId)
    {
        Keep keep = _keepsRepository.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception("Not a valid id");
        }
        return keep;
    }

    internal Keep GetKeepByIdAndIncreaseViews(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId, userId);
        keep.Views++;
        _keepsRepository.EditKeep(keep);
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = _keepsRepository.GetKeeps();
        return keeps;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        List<Keep> keeps = _keepsRepository.GetKeepsByProfileId(profileId);
        return keeps;
    }

    internal List<VaultKeepKeep> GetKeepsByVaultId(int vaultId)
    {
        List<VaultKeepKeep> keeps = _keepsRepository.GetKeepsByVaultId(vaultId);


        return keeps;
    }
}