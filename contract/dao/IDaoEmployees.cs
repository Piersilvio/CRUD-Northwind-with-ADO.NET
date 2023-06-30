using DaoDbNorthwind.contract.enities;

namespace DaoDbNorthwind.contract.dao
{
    public interface IDaoEmployees : IRepository<Employees>
    {
        Task<List<Employees>> GetEmpByCity(string city);
    }
}
