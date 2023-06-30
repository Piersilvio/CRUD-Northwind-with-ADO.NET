using AutoMapper;
using AutoMapper.Configuration.Annotations;
using DaoDbNorthwind.config;
using DaoDbNorthwind.contract;
using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.DaoImplementation;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.DaoImplementation;
using DaoDbNorthwind.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DaoDbNorthwind.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NorthwindController : ControllerBase
    {
        //un DTO per ogni API
        private readonly IDaoOrders repoOrder;
        private readonly IDaoProducts repoProducts;
        private readonly IDaoSupliers repoSupliers;
        private readonly IDaoEmployees repoEmployees;
        private readonly IMapperConfig mapper;

        private readonly ILogger<NorthwindController> _logger;

        public NorthwindController(ILogger<NorthwindController> logger, IDaoEmployees daoEmp, IDaoOrders daoOrder, IDaoProducts daoProduct, IDaoSupliers daoSup, IMapperConfig MapperConfig)
        {
            _logger = logger;
            this.repoOrder = daoOrder;
            this.repoProducts = daoProduct;
            this.repoSupliers = daoSup;
            this.repoEmployees = daoEmp;
            this.mapper = MapperConfig;
        }

        [HttpGet("GetOrderById")]
        public async Task<OrdersDTO> GetOrderById(int OrderID)
        {
            var map = mapper.InitializeAutomapper();
            var orderDTO = map.Map<OrdersDTO>(await repoOrder.Get(OrderID));
            return orderDTO;
        }

        [HttpGet("GetOrderByCustomerID")]
        public async Task<List<OrdersDTO>> GetOrdersByCustomerID(string CustomerID)
        {
            var map = mapper.InitializeAutomapper();
            var orderByCustomerIDDTO = map.Map<List<OrdersDTO>>(await repoOrder.GetOrdersByCustomerID(CustomerID));
            return orderByCustomerIDDTO;
        }

        [HttpGet("GetOrdersByCity")]
        public async Task<List<OrdersDTO>> GetOrdersByCity(string city)
        {
            var map = mapper.InitializeAutomapper();
            var ordersByCity = map.Map<List<OrdersDTO>>(await repoOrder.GetOrdersByCity(city));
            return ordersByCity;
        }

        [HttpGet("GetProductsById")]
        public async Task<ProductsDTO> GetProductsById(int ProductID)
        {
            var map = mapper.InitializeAutomapper();
            var productById = map.Map<ProductsDTO>(await repoProducts.Get(ProductID));
            return productById;
        }

        [HttpGet("GetSuppliersById")]
        public async Task<SupliersDTO> GetSuppliersById(int suppliersId)
        {
            var map = mapper.InitializeAutomapper();
            var suplierById = map.Map<SupliersDTO>(await repoProducts.Get(suppliersId));
            return suplierById;
        }

        [HttpGet("GetEmployeeById")]
        public async Task<EmployeesDTO> GetEmployeeById(int empId)
        {
            var map = mapper.InitializeAutomapper();
            var employeeById = map.Map<EmployeesDTO>(await repoEmployees.Get(empId));
            return employeeById;
        }

        [HttpGet("GetSuppliersByCity")]
        public async Task<List<SupliersDTO>> GetSuppliersByCity(string city)
        {
            var map = mapper.InitializeAutomapper();
            var suplierByCity = map.Map<List<SupliersDTO>>(await repoSupliers.GetSuppliersByCity(city));
            return suplierByCity;
        }

        [HttpGet("GetEmployeesByCity")]
        public async Task<List<EmployeesDTO>> GetEmployeesByCity(string city)
        {
            var map = mapper.InitializeAutomapper();
            var EmployeeByCity = map.Map<List<EmployeesDTO>>(await repoEmployees.GetEmpByCity(city));
            return EmployeeByCity;
        }

        [HttpGet("GetProductByNamer")]
        public async Task<List<ProductsDTO>> GetProductsByNamer(string productName)
        {
            var map = mapper.InitializeAutomapper();
            var ProductByNamer = map.Map<List<ProductsDTO>>(await repoProducts.GetProductsByName(productName));
            return ProductByNamer;
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeesDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var employee = map.Map<Employees>(entity);

            int empID = await repoEmployees.Create(employee);
            return new OkObjectResult(empID);
        }

        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeesDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var employee = map.Map<Employees>(entity);

            bool update = await repoEmployees.Update(employee);
            return new OkObjectResult(update);
        }

        [HttpPost("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(EmployeesDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var employee = map.Map<Employees>(entity);

            bool delete = await repoEmployees.Delete(employee);
            return new OkObjectResult(delete);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrdersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var order = map.Map<Orders>(entity);

            int empID = await repoOrder.Create(order);
            return new OkObjectResult(empID);
        }

        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrdersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var order = map.Map<Orders>(entity);

            bool update = await repoOrder.Update(order);
            return new OkObjectResult(update);
        }

        [HttpPost("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(OrdersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var order = map.Map<Orders>(entity);

            bool delete = await repoOrder.Delete(order);
            return new OkObjectResult(delete);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductsDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var product = map.Map<Products>(entity);

            int empID = await repoProducts.Create(product);
            return new OkObjectResult(empID);
        }

        [HttpPost("UpdateProducts")]
        public async Task<IActionResult> UpdateProducts(ProductsDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var product = map.Map<Products>(entity);

            bool update = await repoProducts.Update(product);
            return new OkObjectResult(update);
        }

        [HttpPost("DeleteProducts")]
        public async Task<IActionResult> DeleteProducts(ProductsDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var product = map.Map<Products>(entity);

            bool delete = await repoProducts.Delete(product);
            return new OkObjectResult(delete);
        }

        [HttpPost("CreateSupplier")]
        public async Task<IActionResult> CreateSupplier(SupliersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var supplier = map.Map<Supliers>(entity);

            int empID = await repoSupliers.Create(supplier);
            return new OkObjectResult(empID);
        }

        [HttpPost("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier(SupliersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var supplier = map.Map<Supliers>(entity);

            bool update = await repoSupliers.Update(supplier);
            return new OkObjectResult(update);
        }

        [HttpPost("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier(SupliersDTO entity)
        {
            var map = mapper.InitializeAutomapper();
            var supplier = map.Map<Supliers>(entity);

            bool delete = await repoSupliers.Delete(supplier);
            return new OkObjectResult(delete);
        }
    }
}
