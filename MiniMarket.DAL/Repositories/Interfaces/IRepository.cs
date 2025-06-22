namespace MiniMarket.DAL.Repositories.Interfaces
{
    public interface IRepository<Key, T>
    {
        public IEnumerable<T> GetAll(int offset, int limit = 20);
        public T? GetById(Key key);
        public IEnumerable<T> GetByIds(List<Key> keys);
        public T Create(T entity);
        public T Update(T entity);
        public bool Delete(T entity);
    }

}
