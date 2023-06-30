namespace DaoDbNorthwind.query
{
    public static class QueryProduct
    {
        public static string ProductByName = "select * from Products where ProductName = @ProductName";

        public static string CreateProduct = "SET IDENTITY_INSERT Products ON "
                            + "INSERT INTO Products (ProductID, "
                            + "ProductName, "
                            + "SupplierID,"
                            + "CategoryID, "
                            + "QuantityPerUnit,"
                            + "UnitPrice, "
                            + "UnitsInStock,"
                            + "UnitsOnOrder, "
                            + "ReorderLevel, "
                            + "Discontinued)"
                            + "VALUES (@ProductID, @ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, "
                            + "@UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued); "
                            + "SELECT SCOPE_IDENTITY();";

        public static string ProductById = "select * from Products where ProductID = @ProductsID";

        public static string UpdateProduct = "UPDATE Products SET UnitPrice = @UnitPrice, UnitsInStock = @UnitsInStock WHERE ProductID = @ProductID";

        public static string DeleteProduct = "DELETE FROM Products WHERE ProductID = @ProductsID";
    }
}
