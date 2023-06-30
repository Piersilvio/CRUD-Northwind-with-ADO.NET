using AutoMapper;
using DaoDbNorthwind.contract.enities;

namespace DaoDbNorthwind.contract.dao
{
    public interface IMapperConfig
    {
        Mapper InitializeAutomapper();
    }
}
