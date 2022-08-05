using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using Company2.Models;

namespace Company2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _sqlDataSource;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlDataSource = _configuration.GetConnectionString("Myconnection");
        }

        [HttpGet]
        public JsonResult Get()
        {
            string jsonstr = String.Empty;
            List<Department> dept = new List<Department>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlDataSource))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spDepartmentSelectALL", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    //cmd.Parameters.Add("@DepartmentName", SqlDbType.VarChar).Value = departmentName;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    //Retrieve data from table and Display result/
                    while (sdr.Read())
                    {
                        int id = (int)sdr["DepartmentId"];
                        string name = (string)sdr["DepartmentName"];
                        Department obj = new Department()
                        {
                            DepartmentId = id,
                            DepartmentName = name
                        };
                        dept.Add(obj);
                    }
                    jsonstr = JsonConvert.SerializeObject(dept);
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
        public JsonResult Post(Department dep)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlDataSource))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spDepartmentCreate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DepartmentName", SqlDbType.VarChar).Value = dep.DepartmentName;
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
        public JsonResult Put(Department dep)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlDataSource))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spDepartmentUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar).Value = dep.DepartmentId;
                    cmd.Parameters.Add("@DepartmentName", SqlDbType.VarChar).Value = dep.DepartmentName;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                return new JsonResult(ex);
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{departmentId}")]
        public JsonResult Delete(int departmentId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlDataSource))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spDepartmentDelete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar).Value = departmentId;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                return new JsonResult(ex);
            }
            return new JsonResult("Delete Successfully");
        }
    }
}