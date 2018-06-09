using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Web.UI.Core.Models;

namespace PMS.Web.UI.Core.Interfaces
{
    public interface IGoalRepository
    {
        IEnumerable<Goal> GetAll { get; }
        IEnumerable<Goal> GetAllReadonly { get; }
        //IEnumerable<Goal> GetAllByUserId(int id);
        Goal GetById(int id);
        Goal Create(Goal goal);
        Task<Goal> CreateAsync(Goal goal);
        Goal Update(Goal goal);
        Task<Goal> UpdateAsync(Goal billing);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
    }
}
