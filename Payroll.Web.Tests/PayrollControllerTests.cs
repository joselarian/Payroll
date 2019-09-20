using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Web.Controllers;
using Payroll.Web.Models; 

namespace Payroll.Web.Tests
{
    [TestClass]
    public class PayrollControllerTests
    { 

        [TestMethod]
        public void TestEmployeeInfoReturnViewName()
        {
            //Arrange
            var controller = new PayrollController();
            var employeeModel = new EmployeeModel
            {
                AnnualSalary = 10000,
                FirstName = "Test1",
                LastName = "Test2",
                Month = "2019-11",
                SuperRate = 9.5
            };

            //Act
            var result = controller.EmployeeInfo(employeeModel) as ViewResult;

            //Assert
            Assert.AreEqual("EmployeePayDetails", result.ViewName);
        }

        [TestMethod]
        public void TestEmployeeInfoReturnPayModel()
        {
            //Arrange
            var controller = new PayrollController();
            var employeeModel = new EmployeeModel
            {
                AnnualSalary = 100000,
                FirstName = "Test1",
                LastName = "Test2",
                Month = "2019-11",
                SuperRate = 9.5
            };

            //Act
            var result = controller.EmployeeInfo(employeeModel) as ViewResult;
            var outputModel = result.Model as PayModel;

            //Assert
            Assert.AreEqual("Test1 Test2", outputModel.EmployeeName);
            Assert.AreEqual("$ 8333", outputModel.GrossIncome);
            Assert.AreEqual("$ 792", outputModel.SuperAmount);
            Assert.AreEqual("$ 2269", outputModel.IncomeTax);
            Assert.AreEqual("$ 6065", outputModel.NetIncome);
            Assert.AreEqual("01 November - 30 November", outputModel.PayPeriod); 
        }


        [TestMethod]
        public void ValidateInValidDataReturnsNullOutputModel()
        {
            //Arrange
            var controller = new PayrollController();
            controller.ModelState.AddModelError("AnnualSalary", "Please enter a valid Annual Salary");
            var employeeModel = new EmployeeModel();

            //Act
            var result = controller.EmployeeInfo(employeeModel) as ViewResult;
            var outputModel = result.Model as PayModel;

            //Assert
            Assert.IsNull(outputModel);
        }

        [TestMethod]
        public void ValidateModelNegativeAnnualSalaryExpectValidationErrors()
        {
            //Arrange
            var model = new EmployeeModel()
            {
                AnnualSalary = -100000,
                FirstName = "Test1",
                LastName = "Test2",
                Month = "2019-11",
                SuperRate = 9.5
            };

            //Act
            var results = TestModelHelper.Validate(model);

            //Assert
            Assert.AreNotEqual(0, results.Count);
            Assert.AreEqual("Please enter a valid Annual Salary", results[0].ErrorMessage);
        }

        [TestMethod]
        public void ValidateModelInValidSuperRateExpectValidationErrors()
        {
            //Arrange
            var model = new EmployeeModel()
            {
                AnnualSalary = 100000,
                FirstName = "Test1",
                LastName = "Test2",
                Month = "2019-11",
                SuperRate = 51
            };

            //Act
            var results = TestModelHelper.Validate(model);

            //Assert
            Assert.AreNotEqual(0, results.Count);
            Assert.AreEqual("The field SuperRate must be between 0 and 50.", results[0].ErrorMessage);
        }

    }
}
