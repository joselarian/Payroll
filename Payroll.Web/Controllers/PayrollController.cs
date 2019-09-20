using Microsoft.AspNetCore.Mvc;  
using Payroll.Web.Models;
using System; 

namespace Payroll.Web.Controllers
{
    public class PayrollController : Controller
    {
        [HttpGet]
        public ActionResult EmployeeInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeInfo(EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
                return View(employeeModel);
            var payModel = GeneratePayModel(employeeModel);
            return View("EmployeePayDetails", payModel);
        }

        private PayModel GeneratePayModel(EmployeeModel employeeModel)
        {
            var incometax = CalculateTax(employeeModel.AnnualSalary);
            var grossIncome = employeeModel.AnnualSalary / 12; 
            var superAmount = grossIncome * (employeeModel.SuperRate / 100);             
            var startDate = new DateTime(Convert.ToInt32(employeeModel.Month.Substring(0,4)), Convert.ToInt32(employeeModel.Month.Substring(5, 2)), 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var range = startDate.ToString("dd") + " " + startDate.ToString("MMMM")
                + " - " + endDate.ToString("dd") + " " + endDate.ToString("MMMM");

            return new PayModel
            {
                EmployeeName = employeeModel.FirstName + " " + employeeModel.LastName,
                PayPeriod = range, 
                GrossIncome = FormatNumber(grossIncome),
                IncomeTax = FormatNumber(incometax),
                NetIncome = FormatNumber(grossIncome - incometax),
                SuperAmount = FormatNumber(superAmount) 
            };
        }

        private string FormatNumber(double input)
        {
            var result = "$ " + Math.Round(input, MidpointRounding.AwayFromZero).ToString();
            return result.Replace(@"/(\d)(?=(\d{3})+\.)/g", "$1,");
        }

        private double CalculateTax(double income)
        {
            var tax = 0.0;
            if (income <= 18200)
            {
                tax = 0;
            }
            else if (income > 18200 && income <= 37000)
            {
                tax = ((0.19 * (income - 18200)) / 12);
            }
            else if (income > 37000 && income <= 87000)
            {
                tax = ((3572 + (0.325 * (income - 37000))) / 12);
            }
            else if (income > 87000 && income <= 180000)
            {
                tax = ((19822 + (0.37 * (income - 80000))) / 12);
            }
            else if (income > 180000)
            {
                tax = ((54232 + (0.45 * (income - 180000))) / 12);
            }
            return tax;
        }         
    }
}