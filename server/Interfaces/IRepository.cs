namespace keepr.Interfaces;

public interface IRepository<T>
{
  public T Create(T data);
  public List<T> GetAll();
  public T GetById(int id);
  public void Update(T updateData);
  public void Delete(int id);
}