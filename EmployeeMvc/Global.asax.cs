using EmployeeManagementMVC.Controllers;
using EmployeeRepositorys;
using EmployeeRepositorys.RepositoryInterface;
using Repository;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;

namespace EmployeeMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new UnityContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            UnityConfig.RegisterComponents();
            container.RegisterType<EmployeeController>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
          //  RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}
