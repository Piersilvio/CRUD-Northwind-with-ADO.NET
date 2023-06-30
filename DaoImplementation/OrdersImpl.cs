using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.query;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DaoDbNorthwind.contract.DaoImplementation
{
    public class OrdersImpl : IDaoOrders
    {
        private readonly string _ConnString;
        private IConfiguration Configuration { get; }

        public OrdersImpl() 
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

        public async Task<List<Orders>> GetOrdersByCustomerID(string CustomerID)
        {
            using(SqlConnection conn = new(_ConnString))
            {
                var orders = new List<Orders>();
                try
                {
                    await conn.OpenAsync();
                    var sqlCommand = new SqlCommand(QueryOrder.OrderByCustomerId);
                    sqlCommand.Connection = conn;
                    sqlCommand.Parameters.AddWithValue("@CustomerID", CustomerID);

                    SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    if(reader != null)
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var order = new Orders();

                                order._orderid = (int)reader[0];
                                order._customerId = reader[1].ToString() ?? null;
                                order._employeesId = (int)reader[2];
                                var date = reader[3];
                                if(date != null) { order._orderdate = (DateTime)date; }
                                date = reader[4];
                                if(date != null) { order._requireddate = (DateTime)date; }
                                date = reader[5];
                                if(date != null) { order._shipdate = (DateTime)date; } 
                                order._shipvia = (int)reader[6];
                                order._freight = (decimal)reader[7];
                                order._shipname = reader[8].ToString() ?? null;
                                order._shipaddress = reader[9].ToString() ?? null;
                                order._shipcity = reader[10].ToString() ?? null;
                                order._shipregion = reader[11].ToString() ?? null;

                                var customer = new Customers();

                                customer._customerid = reader[14].ToString() ?? null;
                                customer._companyname = reader[15].ToString() ?? null;
                                customer._contactname = reader[16].ToString() ?? null;
                                customer._contacttitle = reader[17].ToString() ?? null;
                                customer._address = reader[18].ToString() ?? null;
                                customer._city = reader[19].ToString() ?? null;
                                customer._region = reader[20].ToString() ?? null;
                                customer._postalcode = reader[21].ToString() ?? null;
                                customer._country = reader[22].ToString() ?? null;
                                customer._phone = reader[23].ToString() ?? null;
                                customer._fax = reader[24].ToString() ?? null;

                                order._customer = customer;

                                orders.Add(order);
                            }
                        }
                    }
                }
                catch(SqlException ex)
                {
                    throw new Exception();
                }
                finally
                {
                    CloseSqlConn(conn);
                }
                return orders;
            }
        }

        public async Task<List<Orders>> GetOrdersByCity(string city)
        {
            using(SqlConnection conn = new(_ConnString)) 
            { 
                List<Orders> orders = new();
                try
                {
                    await conn.OpenAsync();
                    var SqlCommand = new SqlCommand(QueryOrder.OrderByCity, conn);
                    SqlCommand.Parameters.AddWithValue("@City", city);
                    using(SqlDataReader reader = await SqlCommand.ExecuteReaderAsync())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var order = new Orders();

                                order._orderid = (int)reader[0];
                                order._customerId = reader[1].ToString() ?? null;
                                order._employeesId = (int)reader[2];
                                var date = reader[3];
                                if (date != null) { order._orderdate = (DateTime)date; }
                                date = reader[4];
                                if (date != null) { order._requireddate = (DateTime)date; }
                                date = reader[5];
                                if (date != null) { order._shipdate = (DateTime)date; }
                                order._shipvia = (int)reader[6];
                                order._freight = (decimal)reader[7];
                                order._shipname = reader[8].ToString() ?? null;
                                order._shipaddress = reader[9].ToString() ?? null;
                                order._shipcity = reader[10].ToString() ?? null;
                                order._shipregion = reader[11].ToString() ?? null;

                                var customer = new Customers();

                                customer._city = reader[14].ToString() ?? null;
                                order._customer = customer;

                                orders.Add(order);
                            }
                        }
                    }
                }catch(Exception)
                {
                    throw new Exception();
                }
                finally
                {
                    CloseSqlConn(conn);
                }
                return orders;
            }
        }

        //FUNZIONA SOLO SE SI METTE UN CustomerID, ShipVia e EmployeeID sono esistenti
        public async Task<int> Create(Orders entity)
        {
            var order = new Orders();
            using (SqlConnection connection = new(_ConnString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = QueryOrder.CreateOrder;

                        command.Parameters.AddWithValue("@OrderID", entity._orderid);
                        command.Parameters.AddWithValue("@CustomerID", entity._customerId);
                        command.Parameters.AddWithValue("@EmployeeID", entity._employeesId);
                        command.Parameters.AddWithValue("@OrderDate", entity._orderdate);
                        command.Parameters.AddWithValue("@RequiredDate", entity._requireddate);
                        command.Parameters.AddWithValue("@ShippedDate", entity._shipdate);
                        command.Parameters.AddWithValue("@ShipVia", entity._shipvia);
                        command.Parameters.AddWithValue("@Freight", entity._freight);
                        command.Parameters.AddWithValue("@ShipName", entity._shipname);
                        command.Parameters.AddWithValue("@ShipAddress", entity._shipaddress);
                        command.Parameters.AddWithValue("@ShipCity", entity._shipcity);
                        command.Parameters.AddWithValue("@ShipRegion", entity._shipregion);

                        var insertedId = Convert.ToInt32(await command.ExecuteScalarAsync());

                        order._orderid = insertedId; //restituisco l'id della create appena fatta
                    }
                }
                catch (Exception) { throw new Exception(); }
                finally { CloseSqlConn(connection); }
            }
            return order._orderid;
        }

        public async Task<Orders> Get(int id)
        {
            using (SqlConnection conn = new(_ConnString))
            {
                Orders order = new();
                try
                {
                    //apro la connessione
                    await conn.OpenAsync();
                    var sqlCommand = new SqlCommand(QueryOrder.OrderById);
                    sqlCommand.Connection = conn;
                    sqlCommand.Parameters.AddWithValue("@OrderID", id);

                    SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                order._orderid = (int)reader[0];
                                order._customerId = reader[1].ToString();
                                order._employeesId = (int)reader[2];
                                var date = reader[3];
                                if (date != null)
                                    order._orderdate = (DateTime)date;
                                date = reader[4];
                                if (date != null)
                                    order._requireddate = (DateTime)date;
                                date = reader[5];
                                if (date != null)
                                    order._shipdate = (DateTime)date;
                                order._shipvia = (int)reader[6];
                                order._freight = (decimal)reader[7];
                                order._shipname = reader[8].ToString();
                                order._shipaddress = reader[9].ToString();
                                order._shipcity = reader[10].ToString();
                                order._shipregion = reader[11].ToString();
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                    throw new Exception();
                }
                finally
                {
                    CloseSqlConn(conn);
                }

                return order;
            }
        }

        public async Task<bool> Update(Orders entity)
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
                        command.CommandText = QueryOrder.UpdateOrder;

                        command.Parameters.AddWithValue("@OrderId", entity._orderid);
                        command.Parameters.AddWithValue("@ShipName", entity._shipname);
                        command.Parameters.AddWithValue("@ShipCity", entity._shipcity);

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

        public async Task<bool> Delete(Orders entity)
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
                        command.CommandText = QueryOrder.DeleteOrder;

                        command.Parameters.AddWithValue("@OrderId", entity._orderid);

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
