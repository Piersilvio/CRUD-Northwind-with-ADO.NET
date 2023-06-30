namespace DaoDbNorthwind.contract.enities
{
    public class Orders
    {
        public int _orderid { get; set; }
        public string? _customerId { get; set; }
        public int? _employeesId { get; set; }
        public DateTime _orderdate { get; set; }
        public DateTime _requireddate { get; set; }
        public DateTime _shipdate { get; set; }
        public int? _shipvia { get; set; }
        public decimal? _freight { get; set; }
        public string? _shipname { get; set; }
        public string? _shipaddress { get; set; }
        public string? _shipcity { get; set; }
        public string? _shipregion { get; set; }

        //nav. prop.
        public Customers? _customer { get; set; }
        public Employees? _employees { get; set; }
        public Shippers? _shippers { get; set; } 
    }
}
