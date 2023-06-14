using Dapper;
using EmployeeModels.ViewModel;
using EmployeeRepositorys.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeRepositorys
{

    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly string conn;
        
        public EmployeeRepository(string connectionString)
        {
            conn = connectionString;
        }

        public List<EmpDesignationViewModel> GetDesignations()
        {
               using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    return connection.Query<EmpDesignationViewModel>("GetEmployeesDesignation", commandType: CommandType.StoredProcedure).ToList();
                }
            
           
        }

        public List<EmpDetailViewModel> AddEmployee(EmpDetailViewModel employee)
        {
            var parameter = new DynamicParameters();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                parameter.Add("@Name", employee.Name);
                parameter.Add("@DesignationId", employee.DesignationId);
                parameter.Add("@ProfilePicture", employee.ProfilePicture);
                parameter.Add("@Salary", employee.Salary);
                parameter.Add("@DateOfBirth", employee.DateOfBirth);
                parameter.Add("@Email", employee.Email);
                parameter.Add("@Address", employee.Address);
                return connection.Query<EmpDetailViewModel>("AddNewEmpDetails", parameter, commandType: CommandType.StoredProcedure).ToList();

            }
        }

        public List<EmpDetailViewModel> GetAllEmployee()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    return connection.Query<EmpDetailViewModel>("GetEmployees", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, display an error message, etc.)
                // You can also rethrow the exception if you want it to be handled higher up in the call stack
                throw;
            }
        }

        public EmpDetailViewModel GetEmployeeById(int? id)
        {
            var parameter = new DynamicParameters();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                parameter.Add("@Id", id);
                return connection.QuerySingleOrDefault<EmpDetailViewModel>("GetEmpById", parameter, commandType: CommandType.StoredProcedure);
            }
        }

        public List<EmpDetailViewModel> UpdateEmployee(EmpDetailViewModel employee)
        {
            var parameter = new DynamicParameters();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                parameter.Add("@Id", employee.Id);
                parameter.Add("@Name", employee.Name);
                parameter.Add("@DesignationId", employee.DesignationId);
                parameter.Add("@ProfilePicture", employee.ProfilePicture);
                parameter.Add("@Salary", employee.Salary);
                parameter.Add("@DateOfBirth", employee.DateOfBirth);
                parameter.Add("@Email", employee.Email);
                parameter.Add("@Address", employee.Address);
                return connection.Query<EmpDetailViewModel>("UpdateEmpDetails", parameter, commandType: CommandType.StoredProcedure).ToList();

            }
        }
        public EmpDetailViewModel DeleteEmployee(int? id)
        {
            var parameter = new DynamicParameters();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                parameter.Add("@Id", id);
                return connection.QuerySingleOrDefault<EmpDetailViewModel>("DeleteEmpById", parameter, commandType: CommandType.StoredProcedure);
            }
        }

    }
}