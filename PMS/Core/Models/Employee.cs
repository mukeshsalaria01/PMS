using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace PMS.Core.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string FirstName { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string LastName { get; set; }
        [Column(TypeName = "VARCHAR(10)")]
        public string Number { get; set; }
        [Column(TypeName = "VARCHAR(10)")]
        public string Grade { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string AppraisalPeriod { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string Department { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? DateCreated { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string CreatedBy { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? DateModified { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string ModifiedBy { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
    }
}
