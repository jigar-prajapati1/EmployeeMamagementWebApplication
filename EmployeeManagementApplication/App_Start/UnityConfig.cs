using EmployeeRepositorys;
using EmployeeRepositorys.RepositoryInterface;
using Repository;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace EmployeeMvc
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; set; }

        public static void RegisterComponents()
        {
		     Container = new UnityContainer();
            Container.AddExtension(new Diagnostic());
           
            Container.RegisterType<IEmployeeService, EmployeeService>();
            Container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            //string connectionString = "Data Source=localhost/SQLEXPRESS;Initial Catalog=EmpDb;Integrated Security=True";
            //Container.RegisterType<EmployeeRepository>(new InjectionConstructor(connectionString));



            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}