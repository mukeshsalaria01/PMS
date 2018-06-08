using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PMS.Core.Interfaces;
using PMS.Core.Models;

namespace PMS.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Employee> GetAll
        {
            get
            {
                return _appDbContext.Employees;
            }
        }

        public IEnumerable<Employee> GetAllReadonly
        {
            get
            {
                return _appDbContext.Employees.AsNoTracking();
            }
        }

        public Employee GetById(int id)
        {
            return _appDbContext.Employees.Where(x => x.IsDeleted == false).AsNoTracking().FirstOrDefault(x => x.EmployeeId == id);
        }
    }
}
