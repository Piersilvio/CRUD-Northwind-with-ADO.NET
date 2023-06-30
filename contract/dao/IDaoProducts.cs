using DaoDbNorthwind.contract.enities;

namespace DaoDbNorthwind.contract.dao
{
    public interface IDaoProducts : IRepository<Products>
    {
        Task<List<Products>> GetProductsByName(string productName);
    }
}
