using AutoMapper;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.DTO;
using DaoDbNorthwind.contract.dao;

namespace DaoDbNorthwind.config
{
    public class MapperConfig : IMapperConfig
    {
        public Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Mpping Employees EmployeesDTO

                /*
                 * aggiunto ForMmeber() per mappare una properties
                 * dal DTO e viceversa diverse fra loro
                 */

                cfg.CreateMap<Employees, EmployeesDTO>()
                   .ForMember(dest => dest._birthday, opt => opt.MapFrom(src => src._birthdate));
                cfg.CreateMap<EmployeesDTO, Employees>()
                    .ForMember(dest => dest._birthdate, opt => opt.MapFrom(src => src._birthday));


                //Mapping Customers CustomerDTO
                cfg.CreateMap<Customers, CustomersDTO>();
                cfg.CreateMap<CustomersDTO,Customers>();

                //Mapping Orders OrdersDTO
                cfg.CreateMap<Orders, OrdersDTO>();
                cfg.CreateMap<OrdersDTO, Orders>();

                //Mapping Products ProductsDTO
                cfg.CreateMap<Products, ProductsDTO>();
                cfg.CreateMap<ProductsDTO, Products>();

                //Mapping Suplier SupliersDTO
                cfg.CreateMap<Supliers, SupliersDTO>();
                cfg.CreateMap<SupliersDTO, Supliers>();
            });

            var mapper = new Mapper(config);

            return mapper;
        }

    }
}

