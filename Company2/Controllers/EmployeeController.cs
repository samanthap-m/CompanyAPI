using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Company2.Models;

namespace Company2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _sqlDataSource;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlDataSource = _configuration.GetConnectionString("Myconnection");
        }

        [HttpGet]
        public JsonResult Get()
        {
            string jsonstr  = String.Empty;
            List<dynamic> list = new List<dynamic>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlDataSource))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spEmployeeSelectAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    //Retrieve data from table and Display result/
                    while (sdr.Read())
                    {
                        int eid = (int)sdr["EmployeeId"];
                        string ename = (string)sdr["EmployeeName"];
                        int did = (int)sdr["DepartmentId"];
                        string dname = (string)sdr["DepartmentName"];
                        DateTime date = (DateTime)sdr["DateOfJoining"];
                        string photo = (string)sdr["PhotoFileName"];
                       Employee obj = new Employee()
                        {
                            DepartmentId = did,
                            EmployeeName = ename,
                            EmployeeId = eid,
                            DateOfJoining = date,
                            PhotoFileName = photo,
                            
                        };
                        Department obj2 = new Department()
                        {
                            DepartmentId=did,
                            DepartmentName = dname
                        };
                        list.Add(obj);
                        list.Add(obj2);
                    }
                    jsonstr = JsonConvert.SerializeObject(list);
                    var o=JsonConvert.DeserializeObject<dynamic>(jsonstr);
                    foreach(var item in o)
                    {
                        item.Property("DepartmentId").Remove();
                    }
                    jsonstr = JsonConvert.SerializeObject(o);
                    jsonstr = jsonstr.Replace("\"", " ");
                    //Close the connection
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                jsonstr = String.Empty;
            }
            return new JsonResult(jsonstr);
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlDataSource))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spEmployeeCreate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeName", SqlDbType.VarChar).Value = emp.EmployeeName;
                    cmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar).Value = emp.DepartmentId;
                    cmd.Parameters.Add("@DateOfJoining", SqlDbType.VarChar).Value = emp.DateOfJoining;
                    cmd.Parameters.Add("@PhotoFileName", SqlDbType.VarChar).Value = emp.PhotoFileName;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                return new JsonResult(ex);
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlDataSource))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spEmployeeUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeName", SqlDbType.VarChar).Value = emp.EmployeeName;
                    cmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar).Value = emp.DepartmentId;
                    cmd.Parameters.Add("@DateOfJoining", SqlDbType.VarChar).Value = emp.DateOfJoining;
                    cmd.Parameters.Add("@PhotoFileName", SqlDbType.VarChar).Value = emp.PhotoFileName;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = emp.EmployeeId;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{empId}")]
        public JsonResult Delete(int empId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlDataSource))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spEmployeeDelete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = empId;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
            return new JsonResult("Delete Successfully");
        }
    }
}