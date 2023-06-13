using EmployeeModels;
using EmployeeModels.ViewModel;
using EmployeeRepositorys.RepositoryInterface;
using System.Collections.Generic;
using System.Linq;


namespace Repository
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }


        public void AddEmployee(EmployeeDetail empDetail)
        {
            //view model to model
            var empviewmodel = new EmpDetailViewModel()
            {
                Name = empDetail.Name,
                DesignationId = empDetail.DesignationId,
                ProfilePicture = empDetail.ProfilePicture,
                Salary = empDetail.Salary,
                DateOfBirth = empDetail.DateOfBirth,
                Email = empDetail.Email,
                Address = empDetail.Address,
                Designation = empDetail.Designation

            };
            employeeRepository.AddEmployee(empviewmodel);

        }

        public void DeleteEmployee(int? id)
        {
            employeeRepository.DeleteEmployee(id);
        }

        public void UpdateEmployee(EmployeeDetail empDetail)
        {
            EmployeeModels.ViewModel.EmpDetailViewModel existingEmployee = employeeRepository.GetEmployeeById(empDetail.Id);

            // Update the properties of the existing employee with the values from the updated detail
            existingEmployee.Name = empDetail.Name;
            existingEmployee.Designation = empDetail.Designation;
            existingEmployee.ProfilePicture = empDetail.ProfilePicture;
            existingEmployee.Salary = empDetail.Salary;
            existingEmployee.DateOfBirth = empDetail.DateOfBirth;
            existingEmployee.Email = empDetail.Email;
            existingEmployee.Address = empDetail.Address;

            // Update the employee using the repository
            employeeRepository.UpdateEmployee(existingEmployee);


        }

        public List<EmployeeDetail> GetAllEmployee()
        {
            
                List<EmployeeDetail> employeeList = new List<EmployeeDetail>();
                var viewModelList = employeeRepository.GetAllEmployee();
                //List<EmployeeModels.EmployeeDetail> employeeList = viewModelList.Select(v => new EmployeeModels.EmployeeDetail
                //{
                //    Id = v.Id,
                //    Name = v.Name,
                //    Designation = v.Designation,
                //    ProfilePicture = v.ProfilePicture,
                //    Salary = v.Salary,
                //    DateOfBirth = v.DateOfBirth,
                //    Email = v.Email,
                //    Address = v.Address
                //}).ToList();

                if (viewModelList != null && viewModelList.Count > 0)
                {
                    foreach (var item in viewModelList)
                    {
                        employeeList.Add(new EmployeeDetail
                        {
                            Id = item.Id,
                            Name = item.Name,
                            DesignationId = item.DesignationId,
                            ProfilePicture = item.ProfilePicture,
                            Salary = item.Salary,
                            DateOfBirth = item.DateOfBirth,
                            Email = item.Email,
                            Address = item.Address,
                            Designation = item.Designation
                        });
                    }
                }

                return employeeList;



            }
            

            public EmployeeDetail GetEmployeeById(int? id)
        {


            EmployeeModels.ViewModel.EmpDetailViewModel viewModel = employeeRepository.GetEmployeeById(id);
            EmployeeModels.EmployeeDetail employee = new EmployeeModels.EmployeeDetail
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                DesignationId = viewModel.DesignationId,
                ProfilePicture = viewModel.ProfilePicture,
                Salary = viewModel.Salary,
                DateOfBirth = viewModel.DateOfBirth,
                Email = viewModel.Email,
                Address = viewModel.Address
            };

            return employee;


        }

        public List<EmployeeDesignation> GetDesignations()
        {
            
            List<EmpDesignationViewModel> viewModelList = employeeRepository.GetDesignations();
            List<EmployeeModels.EmployeeDesignation> employeeList = viewModelList.Select(v => new EmployeeModels.EmployeeDesignation
            {
                DesignationId = v.DesignationId,
                Designation = v.Designation,
               
            }).ToList();
            return employeeList;
        }

       
    }
}
