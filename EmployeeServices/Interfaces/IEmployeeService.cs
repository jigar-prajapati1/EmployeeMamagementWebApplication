using EmployeeModels;
using EmployeeModels.ViewModel;
using System.Collections.Generic;

namespace Repository
{
    public interface IEmployeeService
    {

        List<EmployeeDetail> GetAllEmployee();
        List<EmployeeDesignation> GetDesignations();
        EmployeeDetail GetEmployeeById(int? id);
        void DeleteEmployee(int? id);
        void UpdateEmployee(EmployeeDetail empDetail);r
        void AddEmployee(EmployeeDetail empDetail);
       
    }
}


