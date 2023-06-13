using EmployeeModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRepositorys.RepositoryInterface
{
    public interface IEmployeeRepository
    {
        List<EmpDesignationViewModel> GetDesignations();
        List<EmpDetailViewModel> AddEmployee(EmpDetailViewModel employee);
        List<EmpDetailViewModel> GetAllEmployee();
        EmpDetailViewModel GetEmployeeById(int? id);
        List<EmpDetailViewModel> UpdateEmployee(EmpDetailViewModel employee);
         EmpDetailViewModel DeleteEmployee(int? id);
    }
}
