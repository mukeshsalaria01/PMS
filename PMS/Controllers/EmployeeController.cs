using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.UI.Core.Interfaces;
using PMS.Web.UI.Core.Models;
using PMS.Web.UI.ViewModels;

namespace PMS.Web.UI.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {

        #region Data Members
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public EmployeeController(IEmployeeRepository employeeRepository, UserManager<IdentityUser> userManager)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
        }

        #endregion

        #region ActionResults

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Datatable

        /// <summary>
        /// Patient datatable
        /// </summary>
        /// <returns></returns>
        public JsonResult EmployeeDataTable(JqueryDataTableViewModel param)
        {
            try
            {
                var sortDirection = Request.Query["sSortDir_0"];//asc or desc
                var sortColumnIndex = Convert.ToInt32(Request.Query["iSortCol_0"]);//Get Sorting Index of Column 
                IEnumerable<Employee> qry;

                // incase no app cat is not there we will saw them todays 
                qry = _employeeRepository.GetAllReadonly;
                //Get total count
                var totalrecords = qry.Count();

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    qry = qry.Where(c => c.FirstName != null && c.FirstName.ToLower().Contains(param.sSearch.ToLower())
                                         || c.LastName != null &&
                                         c.LastName.Contains(param.sSearch.ToLower())
                                         || c.Number != null &&
                                         c.Number.Contains(param.sSearch.ToLower())
                                         || c.Grade != null &&
                                         c.Grade.Contains(param.sSearch.ToLower())
                                         || c.Department != null &&
                                         c.Department.Contains(param.sSearch.ToLower())
                                         );
                }
                if (sortColumnIndex == 1)//fname
                {
                    if (sortDirection == "desc")
                    {
                        //Run if sort direction is Descending
                        qry = qry.OrderByDescending(a => a.FirstName);
                    }
                    else if (sortDirection == "asc")
                    {
                        //Run if sort direction is Ascending
                        qry = qry.OrderBy(a => a.FirstName);
                    }
                }
                if (sortColumnIndex == 2)//email
                {
                    if (sortDirection == "desc")
                    {
                        //Run if sort direction is Descending
                        qry = qry.OrderByDescending(a => a.LastName);
                    }
                    else if (sortDirection == "asc")
                    {
                        //Run if sort direction is Ascending
                        qry = qry.OrderBy(a => a.LastName);
                    }
                }

                //Pagination
                if (param.iDisplayLength != -1)
                    qry = qry.Skip(param.iDisplayStart).Take(param.iDisplayLength);

                var result = from c in qry.ToList()
                             select new[]
                             {
                             c.EmployeeId.ToString(),
                             c.FirstName + " " + c.LastName,
                             c.Number,
                             c.Grade,
                             c.AppraisalPeriod,
                             c.Department
                         };

                return Json(new
                {
                    aaData = result,
                    param.sEcho,
                    iTotalRecords = totalrecords,
                    iTotalDisplayRecords = totalrecords,
                });

            }
            catch (Exception ex)
            {
                //Rollbar.RollbarLocator.RollbarInstance.Error(ex.Message);
                return Json(new { Response = ex.InnerException.Message });
            }
        }

        #endregion
    }
}