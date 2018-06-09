using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Web.UI.Core.Models
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }
        //public int EmployeeId { get; set; }
        [Column(TypeName = "VARCHAR(1000)")]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR(500)")]
        public string MeasureOfPerformance { get; set; }
        public int Weightage { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Level1 { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Level2 { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Level3 { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Level4 { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Level5 { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Guideline { get; set; }
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
    }
}
