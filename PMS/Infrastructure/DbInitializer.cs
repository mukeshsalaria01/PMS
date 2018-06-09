using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Web.UI.Core.Models;

namespace PMS.Infrastructure
{
    public static class DbInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider, IConfiguration configuration, AppDbContext context)
        {
            //adding customs roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin" };

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //creating a super user who could maintain the web app
            var powerUser = new IdentityUser
            {
                UserName = configuration.GetSection("AppSettings")["AdminEmail"],
                Email = configuration.GetSection("AppSettings")["AdminEmail"]
            };

            string userPassword = configuration.GetSection("AppSettings")["AdminPassword"];
            var user = await userManager.FindByEmailAsync(configuration.GetSection("AppSettings")["AdminEmail"]);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await userManager.AddToRoleAsync(powerUser, "Admin");
                }
            }

            //creating a CSR who could maintain patients
            //var csrUser = new IdentityUser
            //{
            //    UserName = configuration.GetSection("AppSettings")["CSREmail"],
            //    Email = configuration.GetSection("AppSettings")["CSREmail"]
            //};

            //string csrPassword = configuration.GetSection("AppSettings")["CSRPassword"];
            //var csrUserResp = await userManager.FindByEmailAsync(configuration.GetSection("AppSettings")["CSREmail"]);

            //if (csrUserResp == null)
            //{
            //    var createCSRUser = await userManager.CreateAsync(csrUser, csrPassword);
            //    if (createCSRUser.Succeeded)
            //    {
            //        //here we tie the new user to the "Admin" role 
            //        await userManager.AddToRoleAsync(powerUser, "CSR");
            //    }
            //}


            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            // Employees
            var employees = new Employee[]
            {
                new Employee{FirstName= "Ankush",LastName="Singal",Number = "1498", Grade = "L7", AppraisalPeriod = "01.04.2017 to 31.03.2018			", Department = "Infotech"},
                new Employee{FirstName= "Rohini",LastName="Singh",Number = "1405", Grade = "L7", AppraisalPeriod = "01.04.2017 to 31.03.2018			", Department = "Infotech"},
                new Employee{FirstName= "Narayan",LastName="Sah",Number = "1618", Grade = "L8", AppraisalPeriod = "01.04.2017 to 31.03.2018			", Department = "Infotech"}
            };
            foreach (Employee s in employees)
            {
                context.Employees.Add(s);
            }
            context.SaveChanges();

            // Goals
            var goals = new Goal[]
            {
                new Goal{Name= "AMC & hardware setup",MeasureOfPerformance= "Timeliness",Weightage = 20,Level1 = "", Level2 = "",Level3 = "daily", Level4 = "", Level5 = "Monthly",Guideline = "timely renewal of AMC"},
                new Goal{Name= "Prepartion of IT Policy and its implementation",MeasureOfPerformance= "Timeliness",Weightage = 10,Level1 = "", Level2 = "",Level3 = "By the end of Aug'16", Level4 = "", Level5 = "By the end of 10th Aug'16", Guideline = "- Preparation and Finalizaton of IT policy for Hardware maintenance, software maintenance, entitlements over hardware items etc"},
                new Goal{Name= "Inventory Management and related compliances",MeasureOfPerformance= "Timeliness",Weightage = 10,Level1 = "", Level2 = "",Level3 = "daily", Level4 = "By the end of Sep'16", Level5 = "By the end of Aug'16", Guideline = "- Consisting inventorty of IT Hardware Items and replacement plan, Periodical Data Back up, Hardware Scanning, Records and  dispoals of e-waste items, filing of relevant PPCB returns etc."},
            };
            foreach (Goal c in goals)
            {
                context.Goals.Add(c);
            }
            context.SaveChanges();
        }
    }
}
