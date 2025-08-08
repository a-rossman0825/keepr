using keepr.Interfaces;

namespace keepr.Services;

public class VaultsService : IService<Vault>
{

  private readonly VaultsRepository _vaultsRepo;

  public VaultsService(VaultsRepository vaultsRepo)
  {
    _vaultsRepo = vaultsRepo;
  }

  public Vault Create(Vault data)
  {
    Vault vault = _vaultsRepo.Create(data);
    return vault;
  }

  public string Delete(int vaultId, Account userInfo)
  {
    Vault vaultToDelete = GetById(vaultId);

    if (vaultToDelete.CreatorId != userInfo.Id)
    {
      throw new Exception($"You Cannot Delete Another User's Keep, {userInfo.Name}!");
    }

    _vaultsRepo.Delete(vaultId);

    return $"Your Vault \"{vaultToDelete.Name}\" has been deleted!";
  }

  public List<Vault> GetAll()
  {
    throw new NotImplementedException();
  }

  public Vault GetById(int vaultId)
  {
    Vault vault = _vaultsRepo.GetById(vaultId);
    if (vault == null)
    {
      throw new Exception($"Invalid id: {vaultId}");
    }

    return vault;
  }

  public Vault Update(int vaultId, Vault updateData, Account userInfo)
  {
    Vault originalVault = GetById(vaultId);
    if (originalVault.CreatorId != userInfo.Id)
    {
      throw new Exception($"You can not update someone else's keep, {userInfo.Name}!");
    }

    originalVault.Name = updateData.Name ?? originalVault.Name;
    originalVault.Description = updateData.Description ?? originalVault.Description;
    originalVault.Img = updateData.Img ?? originalVault.Img;
    originalVault.IsPrivate = updateData.IsPrivate ?? originalVault.IsPrivate;
    _vaultsRepo.Update(originalVault);
    return originalVault;
  }
}