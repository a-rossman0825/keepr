using keepr.Interfaces;

namespace keepr.Services;

public class KeepsService : IService<Keep>
{

  private readonly KeepsRepository _keepsRepo;

  public KeepsService(KeepsRepository keepsRepo)
  {
    _keepsRepo = keepsRepo;
  }
  public Keep Create(Keep keepData)
  {
    Keep keep = _keepsRepo.Create(keepData);
    return keep;
  }

  public string Delete(int keepId, Account userInfo)
  {
    Keep keepToDelete = GetById(keepId);

    if (keepToDelete.CreatorId != userInfo.Id)
    {
      throw new Exception($"You Cannot Delete Another User's Keep, {userInfo.Name}!");
    }

    _keepsRepo.Delete(keepId);

    return $"Your Keep \"{keepToDelete.Name}\" has been deleted!";
  }

  public List<Keep> GetAll()
  {
    List<Keep> keeps = _keepsRepo.GetAll();
    return keeps;
  }

  public Keep GetById(int keepId)
  {
    Keep keep = _keepsRepo.GetById(keepId);
    if (keep == null)
    {
      throw new Exception($"Invalid id: {keepId}");
    }

    return keep;
  }

  public Keep Update(int keepId, Keep updateData, Account userInfo)
  {
    Keep originalKeep = GetById(keepId);
    if (originalKeep.CreatorId != userInfo.Id)
    {
      throw new Exception($"You can not update someone else's keep, {userInfo.Name}!");
    }

    originalKeep.Name = updateData.Name ?? originalKeep.Name;
    originalKeep.Description = updateData.Description ?? originalKeep.Description;
    originalKeep.Img = updateData.Img ?? originalKeep.Img;
    _keepsRepo.Update(originalKeep);
    return originalKeep;
  }
}