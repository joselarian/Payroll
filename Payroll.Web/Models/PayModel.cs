namespace Payroll.Web.Models
{
    public class PayModel
    {
        public string EmployeeName { get; set; } 
        public string SuperAmount { get; set; }
        public string GrossIncome { get; set; }
        public string IncomeTax { get; set; }
        public string NetIncome { get; set; }
        public string PayPeriod { get; set; }
    }
}