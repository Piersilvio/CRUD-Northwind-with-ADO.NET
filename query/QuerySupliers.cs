namespace DaoDbNorthwind.query
{
    public static class QuerySupliers
    {
        public static string SuplierByCity = "select * from Suppliers where city = @city";

        public static string CreateSupliers = "SET IDENTITY_INSERT Suppliers ON "
                            + "INSERT INTO Suppliers (SupplierID, "
                            + "CompanyName, "
                            + "ContactName,"
                            + "ContactTitle, "
                            + "Address,"
                            + "City, "
                            + "Region,"
                            + "PostalCode, "
                            + "Country, "
                            + "Phone,"
                            + "Fax)"
                            + "VALUES (@SupplierID, @CompanyName, @ContactName, @ContactTitle, @Address, "
                            + "@City, @Region, @PostalCode, @Country, @Phone, @Fax); "
                            + "SELECT SCOPE_IDENTITY();";

        public static string SuplierById = "select * from Suppliers where SupplierID = @supliersId";

        public static string DeleteSupliers = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";

        public static string UpdateSuplier = "UPDATE Suppliers SET City = @City, Address = @Address WHERE SupplierID = @SupplierID";
    }
}
