using System.Reflection.Metadata.Ecma335;

namespace keeperCheckpoint.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;
    private readonly Auth0Provider _auth0Provider;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider, KeepsService keepsService, VaultsService vaultsService)
    {
        _profilesService = profilesService;
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
    }
    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {
            Profile profile = _profilesService.GetProfileById(profileId);
            return Ok(profile);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
        try
        {
            List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
            return Ok(keeps);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByProfileId(string profileId)
    {
        try
        {
            List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId);
            return Ok(vaults);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}