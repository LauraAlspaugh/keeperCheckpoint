using System.Reflection.Metadata.Ecma335;

namespace keeperCheckpoint.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;
    private readonly Auth0Provider _auth0Provider;
    private readonly KeepsService _keepsService;

    public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider, KeepsService keepsService)
    {
        _profilesService = profilesService;
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;
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
}