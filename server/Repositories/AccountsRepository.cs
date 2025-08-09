namespace keepr.Repositories;

public class AccountsRepository
{
  private readonly IDbConnection _db;

  public AccountsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Account GetByEmail(string userEmail)
  {
    string sql = "SELECT * FROM accounts WHERE email = @userEmail";
    return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
  }

  internal Account GetById(string id)
  {
    string sql = "SELECT * FROM accounts WHERE id = @id";
    return _db.QueryFirstOrDefault<Account>(sql, new { id });
  }

  internal Account Create(Account newAccount)
  {
    string sql = @"
            INSERT INTO accounts
              (name, picture, email, cover_img, id)
            VALUES
              (@Name, @Picture, @Email, @CoverImg, @Id)";
    _db.Execute(sql, newAccount);
    return newAccount;
  }

  internal Account Edit(Account update)
  {
    string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture,
              cover_img = @CoverImg
            WHERE id = @Id;";
    _db.Execute(sql, update);
    return update;
  }

  public Profile GetProfileById(string profileId)
  {
    string sql = @"
    SELECT
      accounts.id,
      accounts.name,
      accounts.picture,
      accounts.cover_img AS CoverImg
    FROM accounts
    WHERE accounts.id = @profileId
    ;";
    return _db.Query<Profile>(sql, new { profileId }).SingleOrDefault();
  }
}

