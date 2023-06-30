using DaoDbNorthwind.contract.enities;

namespace DaoDbNorthwind.contract.dao
{
    public interface IDaoOrders : IRepository<Orders>
    {
        //altre query in più
        Task<List<Orders>> GetOrdersByCustomerID(string CustomerID);
        Task<List<Orders>> GetOrdersByCity(string City);
    }
}
