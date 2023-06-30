namespace DaoDbNorthwind.query
{
    public static class QueryOrder
    {
        public static string OrderById = "SELECT * FROM Orders o WHERE o.OrderID = @OrderID";

        public static string OrderByCustomerId = "select * from Orders o inner join Customers c on o.CustomerID = c.CustomerID where o.CustomerID = @CustomerID";

        public static string OrderByCity = "select o.*, c.City from Orders o inner join Customers c on o.CustomerID = c.CustomerID where c.City = @City";
        
        public static string CreateOrder = "SET IDENTITY_INSERT Orders ON " +
                            "INSERT INTO Orders (OrderID, " +
                            "CustomerID, " +
                            "EmployeeID," +
                            "OrderDate, " +
                            "RequiredDate," +
                            "ShippedDate, " +
                            "ShipVia," +
                            "Freight, " +
                            "ShipName, " +
                            "ShipAddress," +
                            "ShipCity, " +
                            "ShipRegion) " +
                            "VALUES (@OrderID, @CustomerID, @EmployeeID, @OrderDate, @RequiredDate, " +
                            "@ShippedDate, @ShipVia, @Freight, @ShipName, @ShipAddress, @ShipCity, @ShipRegion); " +
                            "SELECT SCOPE_IDENTITY();";

        public static string DeleteOrder = "DELETE FROM Orders WHERE OrderID = @OrderId";

        public static string UpdateOrder = "UPDATE Orders SET ShipName = @ShipName, ShipCity = @ShipCity WHERE OrderID = @OrderId";
    }
}
