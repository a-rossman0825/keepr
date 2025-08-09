using keepr.Interfaces;

namespace keepr.Repositories;

public class VaultsRepository : IRepository<Vault>
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Vault Create(Vault createData)
  {
    string sql = @"
    INSERT INTO
      vaults(
        name,
        description,
        img,
        is_private,
        creator_id
        )
      VALUES(
      @Name,
      @Description,
      @Img,
      @IsPrivate,
      @CreatorId
      );

    SELECT
      vaults.*,
      accounts.*
    FROM 
      vaults
    JOIN
      accounts
    ON 
      vaults.creator_id = accounts.id
    WHERE
      vaults.id = LAST_INSERT_ID()
    ;";

    Vault newVault = _db.Query(sql, (Vault vault, Account account) =>
    {
      vault.Creator = account;
      return vault;
    }, createData).SingleOrDefault();
    return newVault;
  }

  public void Delete(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE id = @VaultId LIMIT 1;";

    _db.Execute(sql, new { VaultId = vaultId });
  }

  public List<Vault> GetAll()
  {
    throw new NotImplementedException();
  }

  public Vault GetById(int vaultId)
  {
    string sql = @"
    SELECT
      vaults.*,
      accounts.*
    FROM
      vaults
    JOIN
      accounts 
    ON 
      vaults.creator_id = accounts.id
    WHERE
      vaults.id = @VaultId
    ;";

    Vault foundVault = _db.Query(sql, (Vault vault, Account account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { VaultId = vaultId }).SingleOrDefault();

    return foundVault;
  }

  public List<Vault> GetVaultsByCreatorId(string profileId)
  {
    string sql = @"
    SELECT
      vaults.*,
      accounts.id,
      accounts.name,
      accounts.picture,
      accounts.cover_img AS coverImg
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.creator_id = @profileId
    ORDER BY vaults.id DESC
    ;";

    return _db.Query<Vault, Account, Vault>(sql, (vault, creator) =>
    {
      vault.Creator = creator;
      return vault;
    }, new { profileId }).ToList();
  }

  public List<Vault> GetVaultsByCreatorId(string profileId, bool isPrivate)
  {
    if (!isPrivate) return GetVaultsByCreatorId(profileId);

    string sql = @"
    SELECT
      vaults.*,
      accounts.id,
      accounts.name,
      accounts.picture,
      accounts.cover_img AS coverImg
    FROM vaults
    JOIN accounts On accounts.id = vaults.creator_id
    WHERE vaults.creator_id = @profileId
    ORDER BY vaults.id DESC
    ;";
    return _db.Query<Vault, Account, Vault>(sql, (vault, creator) =>
    {
      vault.Creator = creator;
      return vault;
    }, new { profileId }).ToList();
  }

  public void Update(Vault updateData)
  {
    string sql = @"
    UPDATE 
      vaults
    SET 
      name = @Name,
      description = @Description,
      img = @Img,
      is_private = @IsPrivate
    WHERE 
      id = @Id LIMIT 1;";
    int rowsAffected = _db.Execute(sql, updateData);
    if (rowsAffected != 1) throw new Exception(rowsAffected + " ROWS HAVE BEEN UPDATED, CHECK YO CODE BRO!");
  }
}