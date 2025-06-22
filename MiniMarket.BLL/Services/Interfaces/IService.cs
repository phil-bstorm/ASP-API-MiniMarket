namespace MiniMarket.BLL.Services.Interfaces
{
    public interface IService<Key, T>
    {
        T Create(T entity);
        T Update(T entity);
        bool Delete(Key id);
        IEnumerable<T> GetAll(int offset, int limit = 20);
        T GetById(Key key);
        IEnumerable<T> GetByIds(List<Key> keys);
    }
}
