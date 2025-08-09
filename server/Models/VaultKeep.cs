namespace keepr.Models;

public class VaultKeep : DbItem<int>
{
  public int KeepId { get; set; }
  public int VaultId { get; set; }
  public string CreatorId { get; set; }
}


// Creates a Keep with VaultKeepId on it
public class SavedKeep : Keep
{
  public int VaultKeepId { get; set; }
}