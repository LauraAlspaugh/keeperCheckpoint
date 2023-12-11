using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace keeperCheckpoint.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth0Provider;

    public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
    {
        _keepsService = keepsService;
        _auth0Provider = auth0Provider;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = _keepsService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [HttpGet]
    public ActionResult<List<Keep>> GetKeeps()
    {
        try
        {
            List<Keep> keep = _keepsService.GetKeeps();
            return Ok(keep);


        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [HttpGet("{keepId}")]
    public async Task<ActionResult<Keep>> GetKeepById(int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.GetKeepByIdAndIncreaseViews(keepId, userInfo?.Id);
            return Ok(keep);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [Authorize]
    [HttpPut("{keepId}")]
    public async Task<ActionResult<Keep>> EditKeep(int keepId, [FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.EditKeep(keepId, userInfo.Id, keepData);
            return Ok(keep);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [Authorize]
    [HttpDelete("{keepId}")]
    public async Task<ActionResult<string>> DestroyKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            string message = _keepsService.DestroyKeep(keepId, userId);
            return Ok(message);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }


}