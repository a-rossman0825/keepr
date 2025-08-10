
namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth;
  private readonly VaultKeepsService _vaultKeepsService;
  public VaultsController(VaultsService vaultsService, Auth0Provider auth, VaultKeepsService vaultKeepsService)
  {
    _vaultsService = vaultsService;
    _auth = auth;
    _vaultKeepsService = vaultKeepsService;
  }

  [HttpGet("{vaultId}")]
  public async  Task<ActionResult<List<Vault>>> GetVaultById(int vaultId)
  {
    try
    {
      Vault vault = _vaultsService.GetById(vaultId);
      if (vault == null)
      {
        return BadRequest("Vault Not Found.");
      }
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);

      if (vault.IsPrivate == true && (userInfo == null || vault.CreatorId != userInfo.Id))
      {
        return BadRequest("No Vault With This Id");
      }
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpPost, Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.Create(vaultData);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpPut("{vaultId}"), Authorize]
  public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultUpdateData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.Update(vaultId, vaultUpdateData, userInfo);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{vaultId}"), Authorize]
  public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultsService.Delete(vaultId, userInfo);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{vaultId}/keeps")]
  [AllowAnonymous]
  public async Task<ActionResult<List<SavedKeep>>> GetKeepsForVault(int vaultId)
  {
    try
    {
      Account userInfo = null;
      try
      {
        userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      }
      catch
      {
        userInfo = null;
      }

      string userId = userInfo?.Id != null ? userInfo?.Id : null;
      List<SavedKeep> keeps = _vaultKeepsService.GetKeepsForVault(vaultId, userId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}