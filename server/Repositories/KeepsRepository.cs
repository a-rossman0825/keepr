using keepr.Interfaces;

namespace keepr.Repositories;

public class KeepsRepository : IRepository<Keep>
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }


  public Keep Create(Keep createData)
  {
    string sql = @"
    INSERT INTO
      keeps(
        name,
        description,
        img,
        views,
        creator_id
        )
      VALUES(
        @Name,
        @Description,
        @Img,
        @Views,
        @CreatorId
      );

    SELECT 
      keeps.*,
      accounts.*
    FROM keeps
    JOIN accounts ON keeps.creator_id = accounts.id
    WHERE keeps.id = LAST_INSERT_ID()
    ;";

    Keep newKeep = _db.Query(sql, (Keep keep, Account account) =>
    {
      keep.Creator = account;
      return keep;
    }, createData).SingleOrDefault();
    return newKeep;
  }

  public void Delete(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @KeepId LIMIT 1;";

    _db.Execute(sql, new { KeepId = keepId });
  }

  public List<Keep> GetAll()
  {
    string sql = @"
    SELECT 
      keeps.*,
      accounts.*
    FROM 
      keeps
    JOIN 
      accounts 
    ON 
      accounts.id = keeps.creator_id
    ORDER BY 
      keeps.created_at ASC
    ;";

    List<Keep> keeps = _db.Query(sql, (Keep keep, Account account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();

    return keeps;
  }

  public Keep GetById(int keepId)
  {
    string sql = @"
    SELECT
      keeps.*,
      accounts.*
    FROM
      keeps
    JOIN 
      accounts ON keeps.creator_id = accounts.id
    WHERE
      keeps.id = @KeepId
    ;";

    Keep foundKeep = _db.Query(sql, (Keep keep, Account account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { KeepId = keepId }).SingleOrDefault();

    return foundKeep;
  }

  public List<Keep> GetKeepsByCreatorId(string profileId)
  {
    string sql = @"
    SELECT
      keeps.*,
      accounts.id,
      accounts.name,
      accounts.picture,
      accounts.cover_img AS CoverImg
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creator_id
    WHERE keeps.creator_id = @profileId
    ORDER BY keeps.id DESC
    ;";

    return _db.Query<Keep, Account, Keep>(sql, (keep, creator) =>
    {
      keep.Creator = creator;
      return keep;
    }, new { profileId }).ToList();
  }

  public void Update(Keep updateData)
  {
    string sql = @"
    UPDATE
      keeps
    SET
      name = @Name,
      description = @Description,
      img = @Img
    WHERE
      id = @Id LIMIT 1
    ;";
    int rowsAffected = _db.Execute(sql, updateData);
    if (rowsAffected != 1) throw new Exception(rowsAffected + " ROWS HABE BEEN UPDATED, CHECK YO CODE BRO!");
  }

  internal Keep getByIdAndIncrement(int keepId)
  {
    string sql = @"
    UPDATE keeps SET views = views + 1 WHERE id = @keepId LIMIT 1;

    SELECT
      keeps.*,
      accounts.*
    FROM keeps
    JOIN accounts ON keeps.creator_id = accounts.id
    WHERE keeps.id = @keepId
    ; ";

    return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { keepId }).SingleOrDefault();
  }
}