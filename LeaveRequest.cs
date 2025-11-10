using System.ComponentModel.DataAnnotations;

namespace LeaveApplication.Models
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }

        [MaxLength(300)]
        public string Reason { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
