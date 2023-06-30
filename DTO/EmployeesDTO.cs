namespace DaoDbNorthwind.DTO
{
    public class EmployeesDTO
    {
        public int _employeeid { get; set; }
        public string? _lastname { get; set; }
        public string? _firstname { get; set; }
        public string? _title { get; set; }
        public string? _titleofcourtesy { get; set; }
        public DateTime? _birthday { get; set; }
        public DateTime? _hiredate { get; set; }
        public string? _address { get; set; }
        public string? _city { get; set; }
        public string? _region { get; set; }
        public string? _postalcode { get; set; }
        public string? _country { get; set; }
    }
}
