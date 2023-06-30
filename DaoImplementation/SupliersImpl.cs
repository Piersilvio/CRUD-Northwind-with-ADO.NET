using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.query;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace DaoDbNorthwind.DaoImplementation
{
    public class SupliersImpl : IDaoSupliers
    {
        private readonly string _ConnString;
        private IConfiguration Configuration { get; }

        public SupliersImpl()
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

        public async Task<List<Supliers>> GetSuppliersByCity(string city)
        {
            using(SqlConnection conn = new(_ConnString))
            {
                var suppliers = new List<Supliers>();
                try
                {
                    await conn.OpenAsync();
                    var sqlCommand = new SqlCommand(QuerySupliers.SuplierByCity, conn);
                    sqlCommand.Parameters.AddWithValue("@city", city);
                    using(SqlDataReader reader = await sqlCommand.ExecuteReaderAsync()) 
                    { 
                        if (reader.HasRows) 
                        {
                            while (reader.Read())
                            {
                                var s = new Supliers();
                                s._supplierid = (int)reader[0];
                                s._companyname = reader[1].ToString();
                                s._contactname = reader[2].ToString();
                                s._contacttitle = reader[3].ToString();
                                s._address = reader[4].ToString();
                                s._city = reader[5].ToString();
                                s._region = reader[6].ToString();
                                s._postalcode = reader[7].ToString();
                                s._country = reader[8].ToString();
                                s._phone = reader[9].ToString();
                                s._fax = reader[10].ToString();
                                suppliers.Add(s);
                            }
                        }
                    }
                }catch(Exception)
                {
                    throw new Exception();
                }finally { CloseSqlConn(conn); }

                return suppliers;
            }
        }

        public async Task<int> Create(Supliers entity)
        {
            var supplier = new Supliers();
            using (SqlConnection connection = new (_ConnString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = QuerySupliers.CreateSupliers;

                        command.Parameters.AddWithValue("@SupplierID", entity._supplierid);
                        command.Parameters.AddWithValue("@CompanyName", entity._companyname);
                        command.Parameters.AddWithValue("@ContactName", entity._contactname);
                        command.Parameters.AddWithValue("@ContactTitle", entity._contacttitle);
                        command.Parameters.AddWithValue("@Address", entity._address);
                        command.Parameters.AddWithValue("@City", entity._city);
                        command.Parameters.AddWithValue("@Region", entity._region);
                        command.Parameters.AddWithValue("@PostalCode", entity._postalcode);
                        command.Parameters.AddWithValue("@Country", entity._country);
                        command.Parameters.AddWithValue("@Phone", entity._phone);
                        command.Parameters.AddWithValue("@Fax", entity._fax);

                        var insertedId = Convert.ToInt32(await command.ExecuteScalarAsync());

                        supplier._supplierid = insertedId; //restituisco l'id della create appena fatta
                    }
                }
                catch (Exception) { throw new Exception(); }
                finally { CloseSqlConn(connection); }
            }
            return supplier._supplierid;
        }

        public async Task<Supliers> Get(int id)
        {
            using (SqlConnection conn = new (_ConnString))
            {
                var s = new Supliers();
                try
                {
                    await conn.OpenAsync();
                    var sqlCommand = new SqlCommand(QuerySupliers.SuplierById, conn);
                    sqlCommand.Parameters.AddWithValue("@supliersId", id);
                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                s._supplierid = (int)reader[0];
                                s._companyname = reader[1].ToString();
                                s._contactname = reader[2].ToString();
                                s._contacttitle = reader[3].ToString();
                                s._address = reader[4].ToString();
                                s._city = reader[5].ToString();
                                s._region = reader[6].ToString();
                                s._postalcode = reader[7].ToString();
                                s._country = reader[8].ToString();
                                s._phone = reader[9].ToString();
                                s._fax = reader[10].ToString();
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
                return s;
            }
        }

        public async Task<bool> Update(Supliers entity)
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
                        command.CommandText = QuerySupliers.UpdateSuplier;

                        command.Parameters.AddWithValue("@SupplierID", entity._supplierid);
                        command.Parameters.AddWithValue("@City", entity._city);
                        command.Parameters.AddWithValue("@Address", entity._address);

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

        public async Task<bool> Delete(Supliers entity)
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
                        command.CommandText = QuerySupliers.DeleteSupliers;

                        command.Parameters.AddWithValue("@SupplierID", entity._supplierid);

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
