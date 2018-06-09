using System;

namespace PMS.Web.UI.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Grade { get; set; }
        public string AppraisalPeriod { get; set; }
        public string Department { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
    }
}
