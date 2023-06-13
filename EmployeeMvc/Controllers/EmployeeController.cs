using System.Web.Mvc;
using System.Web;
using Repository;
using EmployeeModels;
using EmployeeModels.ViewModel;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace EmployeeManagementMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeservice)
        {
            _employeeService = employeeservice;
        }
        public EmployeeController()
        {

        }

        [HttpGet]
        public ActionResult GetAllEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = _employeeService.GetEmployeeById(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            var designations = _employeeService.GetDesignations();
            ViewBag.Designations = new SelectList(designations, "DesignationId", "Designation");
            return View("AddEmployee");
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeDetail empDetail)
        {
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        //save image in server
                        HttpPostedFileBase file = Request.Files[0];
                        string fname = file.FileName;
                        string path = Server.MapPath("~/Images/");
                        string st = path + fname;
                        file.SaveAs(path + fname);
                        empDetail.ProfilePicture = fname;
                    }
                    catch (System.Exception ex)
                    {
                        return Json(ex.Message);
                    }
                }
                //pass details to repository
                _employeeService.AddEmployee(empDetail);

                return RedirectToAction("GetAll");
            }
         

        [HttpGet]
        public ActionResult EditEmployee(int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = _employeeService.GetEmployeeById(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        [HttpPost]
        public ActionResult EditEmployee( EmployeeDetail detail)
        {

                if (Request.Files.Count > 0)
                {
                    try
                    {
                        // Save image on the server
                        HttpPostedFileBase file = Request.Files[0];
                        string fname = file.FileName;
                        string path = Server.MapPath("~/Images/");
                        string st = path + fname;
                        file.SaveAs(path + fname);
                        detail.ProfilePicture = fname;
                    }
                    catch (System.Exception ex)
                    {
                        return Json(ex.Message);
                    }
                }

                // Update employee details using the service
                _employeeService.UpdateEmployee(detail);


                return RedirectToAction("GetAll");
            }

            

        [HttpGet]
        public ActionResult DeleteEmp(int? id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                ViewBag.AlertMsg = "Employee details deleted successfully";
            }
            catch
            {
                ViewBag.AlertMsg = "Failed to delete employee details";
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult GetAll()
        {

            List<EmployeeDetail> employees = _employeeService.GetAllEmployee();

            return View("GetAll", employees);

        }
     
    }
}










