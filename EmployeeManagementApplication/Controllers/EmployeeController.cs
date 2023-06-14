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

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public EmployeeController()
        {

        }

        [HttpGet]
        public ActionResult GetAllEmployee(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var employees = _employeeService.GetEmployeeById(id);
                if (employees == null)
                {
                    return NotFound();
                }
                ViewBag.AlertMsg = "Employee Get successfully";
            }
            catch (Exception ex)
            {
                ViewBag.AlertMsg = "Failed to Get employee";
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
                        string firstname = file.FileName;
                        string path = Server.MapPath("~/Images/");
                        string st = path + firstname;
                        file.SaveAs(path + firstname);
                        empDetail.ProfilePicture = firstname;
                    }
                    catch (System.Exception ex)
                    {
                        return (ex.Message);
                    }
                }
                //pass details to repository
                _employeeService.AddEmployee(empDetail);

                return RedirectToAction("GetAll");
        }
        [HttpGet]
        public ActionResult EditEmployee(int? id )
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var employees = _employeeService.GetEmployeeById(id);
                if (employees == null)
                {
                    return NotFound();
                }
                return View(employees);
            }
            catch (Exception ex)
            {
                return View("Error", new { ErrorMessage = "An error occurred while processing the request." });
            }
        }
    }
        [HttpPost]
        public ActionResult EditEmployee( EmployeeDetail detail)
        {
        try
        {
            if (Request.Files.Count > 0)
            {
                // Save image on the server
                HttpPostedFileBase file = Request.Files[0];
                string firstname = file.FileName;
                string path = Server.MapPath("~/Images/");
                string st = path + firstname;
                file.SaveAs(path + firstname);
                detail.ProfilePicture = firstname;
            }
            // Update employee details using the service
            _employeeService.UpdateEmployee(detail);
            return RedirectToAction("GetAll");
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while processing the request.";
            return View("Error");
        }
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
        try
        {
            List<EmployeeDetail> employees = _employeeService.GetAllEmployee();
            ViewBag.AlertMsg = "Employee Get successfully";
        }
        catch
        {
            ViewBag.AlertMsg = "Failed to GetAll employee ";
        }
            return View("GetAll", employees);
        }
    }
}










