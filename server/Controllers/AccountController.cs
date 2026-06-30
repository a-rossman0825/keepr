

using System.Net;
using System.Security.Claims;

namespace keepr.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      userInfo.Id = ResolveUserId(userInfo.Id);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      userInfo.Id = ResolveUserId(userInfo.Id);
      List<Vault> vaults = _accountService.GetMyVaults(userInfo.Id);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpPut]
  public async Task<ActionResult<Account>> EditAccount([FromBody] Account editData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      userInfo.Id = ResolveUserId(userInfo.Id);
      Account updatedAccount = _accountService.Edit(editData, userInfo.Id);
      return Ok(updatedAccount);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  private string ResolveUserId(string currentId)
  {
    if (!string.IsNullOrWhiteSpace(currentId)) return currentId;

    string resolvedId = HttpContext.User.FindFirst("sub")?.Value
      ?? HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
      ?? HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

    if (!string.IsNullOrWhiteSpace(resolvedId)) return resolvedId;

    string claimTypes = string.Join(", ", HttpContext.User.Claims.Select(c => c.Type).Distinct());
    throw new Exception($"Unable to resolve authenticated user id. Claims found: {claimTypes}");
  }
}
