namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;
  private readonly Auth0Provider _auth;

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth)
  {
    _profilesService = profilesService;
    _auth = auth;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfile(string profileId)
  {
    try
    {
      Profile profile = _profilesService.GetProfileById(profileId);
      return Ok(profile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetVaults(string profileId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);

      if (userInfo != null)
      {
        List<Vault> creatorVaults = _profilesService.GetVaultsByProfileId(profileId, userInfo.Id);
        return Ok(creatorVaults);
      }

      List<Vault> vaults = _profilesService.GetVaultsByProfileId(profileId, null);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _profilesService.GetKeepsByProfileId(profileId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}