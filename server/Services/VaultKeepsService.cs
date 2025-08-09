namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepo;
  private readonly VaultsRepository _vaultsRepo;
  private readonly KeepsRepository _keepsRepo;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsRepository vaultsRepo, KeepsRepository keepsRepo)
  {
    _vaultKeepsRepo = vaultKeepsRepo;
    _vaultsRepo = vaultsRepo;
    _keepsRepo = keepsRepo;
  }

  public VaultKeep CreateVaultKeep(VaultKeep createData)
  {
    Vault vault = _vaultsRepo.GetById(createData.VaultId);
    Keep keep = _keepsRepo.GetById(createData.KeepId);
    if (vault == null)
    {
      throw new Exception($"No Vault with id: {createData.VaultId}");
    }
    if (vault.CreatorId != createData.CreatorId)
    {
      throw new Exception($"That ain't your vault, bruh! Get a lawyer, we're coming for you {createData.CreatorId}!");
    }
    if (keep == null)
    {
      throw new Exception($"NO keep with id: {createData.KeepId}");
    }
    VaultKeep vaultKeep = _vaultKeepsRepo.CreateVaultKeep(createData);
    return vaultKeep;
  }

  public List<SavedKeep> GetKeepsForVault(int vaultId, string userId)
  {
    Vault vault = _vaultsRepo.GetById(vaultId);
    if (vault == null)
    {
      throw new Exception($"No Vault with id: {vaultId}");
    }
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception($"This vault is private.");
    }
    return _vaultKeepsRepo.GetKeepsForVault(vaultId);
  }

  public string Delete(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = _vaultKeepsRepo.GetById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception($"No keep with the Id: {vaultKeepId}");
    }
    if (vaultKeep.CreatorId != userId)
    {
      Vault vault = _vaultsRepo.GetById(vaultKeep.VaultId);
      if (vault == null)
      {
        throw new Exception($"No Vault with Id: {vaultKeep.VaultId}");
      }
      if (vault.CreatorId != userId)
      {
        throw new Exception($"This is not your keep, i'm calling the police on you!");
      }
    }
    _vaultKeepsRepo.Delete(vaultKeepId);
    return $"Keep with the Id of {vaultKeepId} has been removed.";
  }
}