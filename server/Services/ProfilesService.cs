namespace keepr.Services;

public class ProfilesService
{
  private readonly AccountsRepository _accountsRepo;
  private readonly KeepsRepository _keepsRepo;
  private readonly VaultsRepository _vaultsRepo;

  public ProfilesService(AccountsRepository accountsRepo, KeepsRepository keepsRepo, VaultsRepository vaultsRepo)
  {
    _accountsRepo = accountsRepo;
    _keepsRepo = keepsRepo;
    _vaultsRepo = vaultsRepo;
  }

  public Profile GetProfileById(string profileId)
  {
    Profile profile = _accountsRepo.GetProfileById(profileId);
    if (profile == null) throw new Exception($"No Profile with id: {profileId}");
    return profile;
  }

  public List<Keep> GetKeepsByProfileId(string profileId)
  {
    return _keepsRepo.GetKeepsByCreatorId(profileId);
  }

  public List<Vault> GetVaultsByProfileId(string profileId, string userId)
  {
    if (userId != null && userId == profileId)
    {
      return _vaultsRepo.GetVaultsByCreatorId(profileId, true);
    }
    return _vaultsRepo.GetVaultsByCreatorId(profileId);
  }
    
}