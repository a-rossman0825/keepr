namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth = auth;
  }

  [HttpPost, Authorize]
  public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
      return Ok(vaultKeep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{vaultKeepId}"), Authorize]
  public async Task<ActionResult<string>> Delete(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultKeepsService.Delete(vaultKeepId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}