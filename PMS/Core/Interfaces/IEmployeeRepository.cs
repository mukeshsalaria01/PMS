using System.Collections.Generic;
using PMS.Core.Models;

namespace PMS.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll { get; }
        IEnumerable<Employee> GetAllReadonly { get; }
        Employee GetById(int id);
    }
}
