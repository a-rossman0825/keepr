namespace keepr.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;
  private readonly VaultsRepository _vaultsRepo;

  public AccountService(AccountsRepository repo, VaultsRepository vaultsRepo)
  {
    _repo = repo;
    _vaultsRepo = vaultsRepo;
  }

  private Account GetAccount(string accountId)
  {
    Account account = _repo.GetById(accountId);
    if (account == null)
    {
      throw new Exception("Invalid Account Id");
    }
    return account;
  }

  internal Account GetOrCreateAccount(Account userInfo)
  {
    Account account = _repo.GetById(userInfo.Id);
    if (account == null)
    {
      return _repo.Create(userInfo);
    }
    return account;
  }

  internal Account Edit(Account editData, string accountId)
  {
    Account original = GetAccount(accountId);
    original.Name = editData.Name ?? original.Name;
    original.Picture = editData.Picture ?? original.Picture;
    original.CoverImg = editData.CoverImg ?? original.CoverImg;
    return _repo.Edit(original);
  }

  public List<Vault> GetMyVaults(string userId)
  {
    return _vaultsRepo.GetVaultsByCreatorId(userId, true);
    {

    }
  }
}
