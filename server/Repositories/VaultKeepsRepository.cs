using keepr.Interfaces;

namespace keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;
  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  public VaultKeep CreateVaultKeep(VaultKeep createData)
  {
    string sql = @"
    INSERT INTO
      vault_keep(
        keep_id, vault_id, creator_id
      )
      VALUES(
        @KeepId, @VaultId, @CreatorId
      );
    
    SELECT
      vault_keep.id AS Id,
      vault_keep.keep_id AS KeepId,
      vault_keep.vault_id AS VaultId,
      vault_keep.creator_id AS CreatorId
    FROM vault_keep
    WHERE vault_keep.id = LAST_INSERT_ID()
    ;";

    VaultKeep newVaultKeep = _db.Query<VaultKeep>(sql, createData).SingleOrDefault();
    return newVaultKeep;
  }

  public List<SavedKeep> GetKeepsForVault(int vaultId)
  {
    string sql = @"
    SELECT
      keeps.*,
      vault_keep.id AS VaultKeepId,
      accounts.*
    FROM
      vault_keep
    JOIN
      keeps ON keeps.id = vault_keep.keep_id
    JOIN accounts ON accounts.id = keeps.creator_id
    WHERE vault_keep.vault_id = @vaultId
    ;";

    List<SavedKeep> savedKeeps = _db.Query<SavedKeep, Account, SavedKeep>(sql, (savedKeep, account) =>
    {
      savedKeep.Creator = account;
      return savedKeep;
    }, new { vaultId }).ToList();
    return savedKeeps;
  }

  public VaultKeep GetById(int vaultKeepId)
  {
    string sql = @"
    SELECT
      vault_keep.id AS Id,
      vault_keep.keep_id AS KeepId,
      vault_keep.vault_id AS VaultId,
      vault_keep.creator_id AS CreatorId
    FROM vault_keep
    WHERE vault_keep.id = @vaultKeepId
    ;";

    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();
  }

  public void Delete(int vaultKeepId)
  {
    string sql = "DELETE FROM vault_keep WHERE id = @vaultKeepId LIMIT 1;";

    int affectedRows = _db.Execute(sql, new { vaultKeepId });
    if (affectedRows == 0)
    {
      throw new Exception("Saved keep not found or already deleted.");
    }
    if (affectedRows > 1)
    {
      throw new Exception("Oh hell, there they all go. Welp, there goes my career as a software developer 😢");
    }
  }
}