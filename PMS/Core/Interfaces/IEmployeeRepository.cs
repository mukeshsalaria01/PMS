using System.Collections.Generic;
using PMS.Web.UI.Core.Models;

namespace PMS.Web.UI.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll { get; }
        IEnumerable<Employee> GetAllReadonly { get; }
        Employee GetById(int id);
    }
}
