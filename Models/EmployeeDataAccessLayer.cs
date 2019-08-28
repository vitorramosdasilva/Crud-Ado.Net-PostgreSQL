    using System;    
    using System.Collections.Generic;    
    using System.Data;    
    //using System.Data.SqlClient;    
    using System.Linq;    
    using System.Threading.Tasks;
    using Npgsql;

        
    namespace MVCAdoDemo.Models    
    {    
        public class EmployeeDataAccessLayer    
        {    
            string connectionString = "Server=127.0.0.1;User Id=postgres; Password=postgres;Database=MVCAdoDemo;";
                   
        
            //To View all employees details      
            public IEnumerable<Employee> GetAllEmployees()    
            {    
                List<Employee> lstemployee = new List<Employee>();    
        
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))    
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("select * from tblemployee", con);
                    //ds.Reset();   
                    //cmd.CommandType = CommandType.StoredProcedure;    
        
                    con.Open();    
                    NpgsqlDataReader rdr = cmd.ExecuteReader();    
        
                    while (rdr.Read())    
                    {    
                        Employee employee = new Employee();    
        
                        employee.ID = Convert.ToInt32(rdr["EmployeeID"]);    
                        employee.Name = rdr["Name"].ToString();    
                        employee.Gender = rdr["Gender"].ToString();    
                        employee.Department = rdr["Department"].ToString();    
                        employee.City = rdr["City"].ToString();    
        
                        lstemployee.Add(employee);    
                    }    
                    con.Close();    
                }    
                return lstemployee;    
            }    
        
            //To Add new employee record      
            public void AddEmployee(Employee employee)    
            {    
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))    
                {    
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO tblemployee(name, city, department, gender) VALUES (@Name, @City, @Department, @Gender);", con);                        
                    //cmd.CommandType = CommandType.StoredProcedure;   
        
                    cmd.Parameters.AddWithValue("@Name", employee.Name);    
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);    
                    cmd.Parameters.AddWithValue("@Department", employee.Department);    
                    cmd.Parameters.AddWithValue("@City", employee.City);    
        
                    con.Open();    
                    cmd.ExecuteNonQuery();    
                    con.Close();    
                }    
            }    
        
            //To Update the records of a particluar employee    
            public void UpdateEmployee(Employee employee)    
            {    
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))    
                {    
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE tblemployee SET name=@Name, city=@City, department=@Department, gender=@Gender WHERE employeeid=@EmpId;", con);    
                    //cmd.CommandType = CommandType.StoredProcedure;    
        
                    cmd.Parameters.AddWithValue("@EmpId", employee.ID);    
                    cmd.Parameters.AddWithValue("@Name", employee.Name);    
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);    
                    cmd.Parameters.AddWithValue("@Department", employee.Department);    
                    cmd.Parameters.AddWithValue("@City", employee.City);    
        
                    con.Open();    
                    cmd.ExecuteNonQuery();    
                    con.Close();    
                }    
            }    
        
            //Get the details of a particular employee    
            public Employee GetEmployeeData(int? id)    
            {    
                Employee employee = new Employee();    
        
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))    
                {    
                    string sqlQuery = "SELECT * FROM tblemployee WHERE EmployeeID= " + id;    
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con);    
        
                    con.Open();    
                    NpgsqlDataReader rdr = cmd.ExecuteReader();    
        
                    while (rdr.Read())    
                    {    
                        employee.ID = Convert.ToInt32(rdr["EmployeeID"]);    
                        employee.Name = rdr["Name"].ToString();    
                        employee.Gender = rdr["Gender"].ToString();    
                        employee.Department = rdr["Department"].ToString();    
                        employee.City = rdr["City"].ToString();    
                    }    
                }    
                return employee;    
            }    
        
            //To Delete the record on a particular employee    
            public void DeleteEmployee(int? id)    
            {    
        
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))    
                {    
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM tblemployee WHERE EmployeeID = @EmpId", con);    
                    //cmd.CommandType = CommandType.StoredProcedure;    
        
                    cmd.Parameters.AddWithValue("@EmpId", id);    
        
                    con.Open();    
                    cmd.ExecuteNonQuery();    
                    con.Close();    
                }    
            }    
        }    
    }    
