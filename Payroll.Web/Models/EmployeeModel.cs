using System.ComponentModel.DataAnnotations;

namespace Payroll.Web.Models
{
    public class EmployeeModel 
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(0, 10000000, ErrorMessage = "Please enter a valid Annual Salary")]
        public double AnnualSalary { get; set; }
        [Required]
        [Range(0, 50)]
        public double SuperRate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string Month { get; set; }
    }
}