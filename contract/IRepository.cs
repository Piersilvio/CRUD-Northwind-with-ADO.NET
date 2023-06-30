namespace DaoDbNorthwind.contract
{
    public interface IRepository<T> where T : class
    {
        //CRUD
        public Task<int> Create (T entity);
        public Task<T> Get(int id);
        public Task<bool> Update (T entity);
        public Task<bool> Delete(T entity);
    }
}
