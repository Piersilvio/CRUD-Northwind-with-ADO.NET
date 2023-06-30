using System.Globalization;

namespace DaoDbNorthwind.contract.enities
{
    public class Customers
    {
        public string? _customerid { get; set; }
        public string? _companyname { get; set; }
        public string? _contactname { get; set; }
        public string? _contacttitle { get; set; }
        public string? _address { get; set; }
        public string? _city { get; set; }
        public string? _region { get; set; }
        public string? _postalcode { get; set; }
        public string? _country { get; set; }
        public string? _phone { get; set; }
        public string? _fax { get; set; }
    }
}
