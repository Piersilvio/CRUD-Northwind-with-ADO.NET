using DaoDbNorthwind.contract.enities;

namespace DaoDbNorthwind.contract.dao
{
    public interface IDaoSupliers : IRepository<Supliers>
    {
        Task<List<Supliers>> GetSuppliersByCity(string city);
    }
}
