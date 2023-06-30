using DaoDbNorthwind.contract;
using DaoDbNorthwind.contract.dao;
using DaoDbNorthwind.contract.enities;
using DaoDbNorthwind.query;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace DaoDbNorthwind.DaoImplementation
{
    public class EmplImpl : IDaoEmployees
    {
        private readonly string _ConnString;
        private IConfiguration Configuration { get; }

        public  EmplImpl()
        {
            var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, false);

            Configuration = configurationBuilder.Build();
            _ConnString = Configuration.GetConnectionString("MyConn");
        }

        public async Task<List<Employees>> GetEmpByCity(string city)
        {
            using(var conn = new SqlConnection(_ConnString))
            {
                var emps = new List<Employees>();
                try
                {
                    await conn.OpenAsync();
                    var sqlCommand = new SqlCommand(QueryEmployee.EmployeesByCity, conn);
                    sqlCommand.Parameters.AddWithValue("@city", city);
                    using(SqlDataReader reader =  await sqlCommand.ExecuteReaderAsync()) 
                    { 
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var e = new Employees
                                {
                                    _employeeid = (int)reader[0],
                                    _lastname = reader[1].ToString(),
                                    _firstname = reader[2].ToString(),
                                    _title = reader[3].ToString(),
                                    _titleofcourtesy = reader[4].ToString()
                                };
                                var date = reader[5];
                                if (date != null) { e._birthdate = (DateTime)date; }
                                date = reader[6];
                                if (date != null) { e._hiredate = (DateTime)date; }

                                e._address = reader[7].ToString();
                                e._city = reader[8].ToString();
                                e._region = reader[9].ToString();
                                e._postalcode = reader[10].ToString();
                                e._country = reader[11].ToString();
                                emps.Add(e);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }finally { CloseSqlConn(conn); }
                return emps;
            }
        }

        public static void CloseSqlConn(SqlConnection conn)
        {
            if (conn.State == System.Data.ConnectionState.Closed) { conn.Close(); }
        }

        public async Task<int> Create(Employees entity)
        {
            Employees employee = new();
            using (SqlConnection connection = new(_ConnString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = QueryEmployee.CreateEmployee;

                        command.Parameters.AddWithValue("@EmployeeID", entity._employeeid);
                        command.Parameters.AddWithValue("@LastName", entity._lastname);
                        command.Parameters.AddWithValue("@FirstName", entity._firstname);
                        command.Parameters.AddWithValue("@Title", entity._title);
                        command.Parameters.AddWithValue("@TitleOfCourtesy", entity._titleofcourtesy);
                        command.Parameters.AddWithValue("@BirthDate", entity._birthdate);
                        command.Parameters.AddWithValue("@HireDate", entity._birthdate);
                        command.Parameters.AddWithValue("@address", entity._address);
                        command.Parameters.AddWithValue("@city", entity._city);
                        command.Parameters.AddWithValue("@region", entity._region);
                        command.Parameters.AddWithValue("@postalCode", entity._postalcode);
                        command.Parameters.AddWithValue("@country", entity._country);

                        var insertedId = Convert.ToInt32(await command.ExecuteScalarAsync());

                        employee._employeeid = insertedId; //restituisco l'id della create appena fatta
                    }
                }
                catch (Exception ex) { throw new Exception(); }
                finally { CloseSqlConn(connection); }
            }
            return employee._employeeid;
        }

        public async Task<Employees> Get(int id)
        {
            using (SqlConnection conn = new(_ConnString))
            {
                var e = new Employees();
                try
                {
                    await conn.OpenAsync();
                    var sqlCommand = new SqlCommand(QueryEmployee.EmployeeById, conn);
                    sqlCommand.Parameters.AddWithValue("@empId", id);
                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                e._employeeid = (int)reader[0];
                                e._lastname = reader[1].ToString();
                                e._firstname = reader[2].ToString();
                                e._title = reader[3].ToString();
                                e._titleofcourtesy = reader[4].ToString();
                                var date = reader[5];
                                if (date != null) { e._birthdate = (DateTime)date; }
                                date = reader[6];
                                if (date != null) { e._hiredate = (DateTime)date; }

                                e._address = reader[7].ToString();
                                e._city = reader[8].ToString();
                                e._region = reader[9].ToString();
                                e._postalcode = reader[10].ToString();
                                e._country = reader[11].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
                finally
                {
                    CloseSqlConn(conn);
                }
                return e;
            }
        }

        public async Task<bool> Update(Employees entity)
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
                        command.CommandText = QueryEmployee.UpdateEmployee;

                        command.Parameters.AddWithValue("@EmployeeID", entity._employeeid);
                        command.Parameters.AddWithValue("@LastName", entity._lastname);
                        command.Parameters.AddWithValue("@city", entity._city);

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }catch (Exception ex) { throw new Exception(); }
                finally { CloseSqlConn(connection); }
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }

        public async Task<bool> Delete(Employees entity)
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
                        command.CommandText = QueryEmployee.DeleteEmployee;

                        command.Parameters.AddWithValue("@EmployeeId", entity._employeeid);

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }catch(Exception ex) { throw new Exception(); }
                finally { CloseSqlConn(connection); }
            }

            if (rowsAffected > 0) { update = true; }
            else { update = false; }

            return update;
        }
    }
}
