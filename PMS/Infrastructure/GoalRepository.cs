using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS.Web.UI.Core.Interfaces;
using PMS.Web.UI.Core.Models;

namespace PMS.Infrastructure
{
    public class GoalRepository : IGoalRepository
    {
        private readonly AppDbContext _appDbContext;

        public GoalRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Goal> GetAll
        {
            get
            {
                return _appDbContext.Goals;
            }
        }

        public IEnumerable<Goal> GetAllReadonly
        {
            get
            {
                return _appDbContext.Goals.AsNoTracking();
            }
        }

        public Goal GetById(int id)
        {
            return _appDbContext.Goals.Where(x => x.IsDeleted == false).AsNoTracking().FirstOrDefault(x => x.GoalId == id);
        }

        //public IEnumerable<Goal> GetAllByUserId(int id)
        //{
        //    return _appDbContext.Goals.Where(x => x.IsDeleted == false && x.UserId == id).AsNoTracking()
        //}

        public Goal Create(Goal goal)
        {
            _appDbContext.Goals.Add(goal);
             _appDbContext.SaveChanges();
            return goal;
        }

        public Goal Update(Goal goal)
        {
             _appDbContext.SaveChanges();
            return goal;
        }

        public async Task<Goal> CreateAsync(Goal goal)
        {
            goal.DateCreated = DateTime.UtcNow;
            goal.DateModified = DateTime.UtcNow;
            _appDbContext.Goals.Add(goal);
            await _appDbContext.SaveChangesAsync();
            return goal;
        }

        public async Task<Goal> UpdateAsync(Goal goal)
        {
            //_appDbContext.Entry(goal).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return goal;
        } 
       
        public bool Delete(int id)
        {
            var goal = _appDbContext.Goals.FirstOrDefault(x => x.GoalId == id);
            if (goal != null)
            {
                goal.IsDeleted = true;
                goal.DateModified = DateTime.UtcNow;
                goal.ModifiedBy = goal.ModifiedBy;
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var goal = _appDbContext.Goals.FirstOrDefault(x => x.GoalId == id);
            if (goal != null)
            {
                goal.IsDeleted = true;
                goal.DateModified = DateTime.UtcNow;
                goal.ModifiedBy = goal.ModifiedBy;
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
