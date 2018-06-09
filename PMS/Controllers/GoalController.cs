using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.UI.Core.Interfaces;
using PMS.Web.UI.Core.Models;
using PMS.Web.UI.ViewModels;

namespace PMS.Web.UI.Controllers
{
    [Authorize]
    public class GoalController : Controller
    {
        #region Data Members
        private readonly IGoalRepository _goalRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public GoalController(IGoalRepository goalRepository, UserManager<IdentityUser> userManager)
        {
            _goalRepository = goalRepository;
            _userManager = userManager;
        }

        #endregion

        #region ActionResults

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Add New goal
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View("~/Views/Shared/_AddEditGoal.cshtml");
        }

        /// <summary>
        /// Edit goal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var patient = _goalRepository.GetById(id);
            var model = Mapper.Map<Goal, GoalViewModel>(patient);
            return View("~/Views/Shared/_AddEditGoal.cshtml", model);
        }

        /// <summary>
        /// Update Record
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEdit(GoalViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserId(HttpContext.User);
                if (vm.GoalId > 0)
                {
                    var goal = _goalRepository.GetById(vm.GoalId);
                    if (goal == null) return RedirectToAction("Index");
                    vm.ModifiedBy = user;
                    vm.CreatedBy = goal.CreatedBy;
                    vm.DateCreated = goal.DateCreated;
                    goal = Mapper.Map<GoalViewModel, Goal>(vm);
                    await _goalRepository.UpdateAsync(goal);
                }
                else
                {
                    vm.CreatedBy = user;
                    vm.ModifiedBy = user;
                    var goal = Mapper.Map<GoalViewModel, Goal>(vm);
                    await _goalRepository.CreateAsync(goal);
                }
                return RedirectToAction("Index");
            }
            return View("~/Views/Shared/_AddEditGoal.cshtml", vm);
        }

        #endregion

        #region Datatable

        /// <summary>
        /// Patient datatable
        /// </summary>
        /// <returns></returns>
        public JsonResult GoalDataTable(JqueryDataTableViewModel param)
        {
            try
            {
                var sortDirection = Request.Query["sSortDir_0"];//asc or desc
                var sortColumnIndex = Convert.ToInt32(Request.Query["iSortCol_0"]);//Get Sorting Index of Column 
                IEnumerable<Goal> qry;

                // incase no app cat is not there we will saw them todays 
                qry = _goalRepository.GetAllReadonly;
                //Get total count
                var totalrecords = qry.Count();

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    qry = qry.Where(c => c.Name != null && c.Name.ToLower().Contains(param.sSearch.ToLower())
                                         || c.MeasureOfPerformance != null &&
                                         c.MeasureOfPerformance.Contains(param.sSearch.ToLower())
                                         || c.Weightage.ToString().Contains(param.sSearch.ToLower())
                                         || c.Level1 != null &&
                                         c.Level1.Contains(param.sSearch.ToLower())
                                         || c.Level2 != null &&
                                         c.Level2.Contains(param.sSearch.ToLower())
                                         || c.Level2 != null &&
                                         c.Level2.Contains(param.sSearch.ToLower())
                                         || c.Level3 != null &&
                                         c.Level3.Contains(param.sSearch.ToLower())
                                         || c.Level4 != null &&
                                         c.Level4.Contains(param.sSearch.ToLower())
                                         || c.Level5 != null &&
                                         c.Level5.Contains(param.sSearch.ToLower())
                                         || c.Guideline != null &&
                                         c.Guideline.Contains(param.sSearch.ToLower())
                                         );
                }
                if (sortColumnIndex == 1)//fname
                {
                    if (sortDirection == "desc")
                    {
                        //Run if sort direction is Descending
                        qry = qry.OrderByDescending(a => a.Name);
                    }
                    else if (sortDirection == "asc")
                    {
                        //Run if sort direction is Ascending
                        qry = qry.OrderBy(a => a.Name);
                    }
                }
                if (sortColumnIndex == 2)//email
                {
                    if (sortDirection == "desc")
                    {
                        //Run if sort direction is Descending
                        qry = qry.OrderByDescending(a => a.MeasureOfPerformance);
                    }
                    else if (sortDirection == "asc")
                    {
                        //Run if sort direction is Ascending
                        qry = qry.OrderBy(a => a.MeasureOfPerformance);
                    }
                }


                //Pagination
                if (param.iDisplayLength != -1)
                    qry = qry.Skip(param.iDisplayStart).Take(param.iDisplayLength);

                var result = from c in qry.ToList()
                             select new[]
                             {
                             c.GoalId.ToString(),
                             c.Name,
                             c.MeasureOfPerformance,
                             c.Weightage.ToString(),
                             c.Level5,
                             c.Level3,
                             c.Guideline
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