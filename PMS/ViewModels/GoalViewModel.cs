using System;

namespace PMS.Web.UI.ViewModels
{
    public class GoalViewModel
    {
        public int GoalId { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

    }
}
