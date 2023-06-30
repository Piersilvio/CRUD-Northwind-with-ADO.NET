using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.query;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DaoDbNorthwind.DaoImplementation
{
    public class ProductsImpl : IDaoProducts
    {
        private readonly string _ConnString;
        private IConfiguration Configuration { get; set; }

        public ProductsImpl() 
        {
            var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, false);

            Configuration = configurationBuilder.Build();
            _ConnString = Configuration.GetConnectionString("MyConn");
        }
        public static void CloseSqlConn(SqlConnection conn)
        {
            if (conn.State == System.Data.ConnectionState.Closed) { conn.Close(); }
        }

        public async Task<List<Products>> GetProductsByName(string productName)
        {
            using (SqlConnection conn = new(_ConnString))
            {
                var products = new List<Products>();
                try
                {
                    await conn.OpenAsync();
                    var sqlCommand = new SqlCommand(QueryProduct.ProductByName, conn);
                    sqlCommand.Parameters.AddWithValue("@ProductName", productName);
                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var p = new Products();
                                p._productid = reader.GetInt32(0);
                                p._productname = reader.GetString(1);
                                p._supplierid = reader.GetInt32(2);
                                p._categoryid = reader.GetInt32(3);
                                p._quantityperunit = reader.GetString(4);
                                p._unitprice = reader.GetDecimal(5);
                                p._unitsinstock = reader.GetInt16(6);
                                p._unitsinorder = reader.GetInt16(7);
                                p._reorderlevel = reader.GetInt16(8);
                                p._discontinued = reader.GetBoolean(9);
                                products.Add(p);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception();         }
                finally
                {
                    CloseSqlConn(conn);
                }

                return products;
            }
        }

        public async Task<int> Create(Products entity)
        {
            var product = new Products();
            using (SqlConnection connection = new(_ConnString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = QueryProduct.CreateProduct;

                        command.Parameters.AddWithValue("@ProductID", entity._productid);
                        command.Parameters.AddWithValue("@ProductName", entity._productname);
                        command.Parameters.AddWithValue("@SupplierID", entity._supplierid);
                        command.Parameters.AddWithValue("@CategoryID", entity._categoryid);
                        command.Parameters.AddWithValue("@QuantityPerUnit", entity._quantityperunit);
                        command.Parameters.AddWithValue("@UnitPrice", entity._unitprice);
                        command.Parameters.AddWithValue("@UnitsInStock", entity._unitsinstock);
                        command.Parameters.AddWithValue("@UnitsOnOrder", entity._unitsinorder);
                        command.Parameters.AddWithValue("@ReorderLevel", entity._reorderlevel);
                        command.Parameters.AddWithValue("@Discontinued", entity._discontinued);

                        var insertedId = Convert.ToInt32(await command.ExecuteScalarAsync());

                        product._productid = insertedId; //restituisco l'id della create appena fatta
                    }
                }
                catch (Exception) { throw new Exception(); }
                finally { CloseSqlConn(connection); }
            }
            return product._productid;
        }

        public async Task<Products> Get(int id)
        {
            using (SqlConnection conn = new(_ConnString))
            {
                var p = new Products();
                try
                {
                    await conn.OpenAsync();
                    var sqlCommand = new SqlCommand(QueryProduct.ProductById, conn);
                    sqlCommand.Parameters.AddWithValue("@ProductsID", id);
                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                p._productid = reader.GetInt32(0);
                                p._productname = reader.GetString(1);
                                p._supplierid = reader.GetInt32(2);
                                p._categoryid = reader.GetInt32(3);
                                p._quantityperunit = reader.GetString(4);
                                p._unitprice = reader.GetDecimal(5);
                                p._unitsinstock = reader.GetInt16(6);
                                p._unitsinorder = reader.GetInt16(7);
                                p._reorderlevel = reader.GetInt16(8);
                                p._discontinued = reader.GetBoolean(9);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception();
                }
                finally
                {
                    CloseSqlConn(conn);
                }

                return p;
            }
        }

        public async Task<bool> Update(Products entity)
        {
            Boolean update;
            int rowsAffected = 0;
            using (SqlConnection connection = new(_ConnString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = QueryProduct.UpdateProduct;

                        command.Parameters.AddWithValue("@ProductID", entity._productid);
                        command.Parameters.AddWithValue("@UnitPrice", entity._unitprice);
                        command.Parameters.AddWithValue("@UnitsInStock", entity._unitsinstock);

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception) { throw new Exception(); }
                finally { CloseSqlConn(connection); }
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }

        public async Task<bool> Delete(Products entity)
        {
            Boolean update;
            int rowsAffected = 0;
            using (SqlConnection connection = new(_ConnString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = QueryProduct.DeleteProduct;

                        command.Parameters.AddWithValue("@ProductsID", entity._productid);

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception) { throw new Exception(); }
                finally { CloseSqlConn(connection); }
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }
    }
}
