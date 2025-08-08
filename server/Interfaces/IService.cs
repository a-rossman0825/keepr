
namespace keepr.Interfaces;

public interface IService<T>
{
  public T Create(T data);
  public List<T> GetAll();
  public T GetById(int id);
  public T Update(int id, T updateData, Account userInfo);
  public string Delete(int id, Account userInfo);
}