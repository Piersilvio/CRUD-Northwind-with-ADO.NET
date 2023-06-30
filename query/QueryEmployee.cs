namespace DaoDbNorthwind.query
{
    public static class QueryEmployee
    {
        public static string EmployeesByCity = "select * from Employees where City = @city";

        public static string CreateEmployee = "SET IDENTITY_INSERT Employees ON " +
                                            "INSERT INTO Employees (EmployeeID, " +
                                            "LastName, " +
                                            "FirstName," +
                                            "Title, " +
                                            "TitleOfCourtesy," +
                                            "BirthDate, " +
                                            "HireDate," +
                                            "Address, " +
                                            "City, " +
                                            "Region," +
                                            "PostalCode, " +
                                            "Country) " +
                                            "VALUES (@EmployeeID, @LastName, @FirstName, @Title, @TitleOfCourtesy, " +
                                            "@BirthDate, @HireDate, @address, @city, @region, @postalCode, @country); " +
                                            "SELECT SCOPE_IDENTITY();";

        public static string EmployeeById = "select * from Employees where EmployeeID = @empId";

        public static string UpdateEmployee = "UPDATE Employees SET LastName = @LastName, City = @city WHERE EmployeeID = @EmployeeId";

        public static string DeleteEmployee = "DELETE FROM Employees WHERE EmployeeID = @EmployeeId";
    }
}
