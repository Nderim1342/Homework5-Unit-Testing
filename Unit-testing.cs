using System;
using System.Collections.Generic;
using System.Linq;

namespace BonusSystem
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public double Salary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Department
    {
        public int DepartmentId { get; set; }
        public double Sales { get; set; }
    }

    public class BonusModule
    {
        private List<Employee> employees = new List<Employee>();
        private List<Department> departments = new List<Department>();

        public void AddDepartment(int departmentId, double sales)
        {
            departments.Add(new Department { DepartmentId = departmentId, Sales = sales });
        }

        public void AddEmployee(int employeeId, double salary, bool isManager, int departmentId)
        {
            employees.Add(new Employee { EmployeeId = employeeId, Salary = salary, IsManager = isManager, DepartmentId = departmentId });
        }

        public int ApplyBonus()
        {
            if (!departments.Any() || !employees.Any())
            {
                return 1; // Error code 1: Table is empty
            }

            var maxSalesDept = departments.OrderByDescending(d => d.Sales).FirstOrDefault();

            var eligibleEmployees = employees.Where(e => e.DepartmentId == maxSalesDept.DepartmentId).ToList();

            if (!eligibleEmployees.Any())
            {
                return 2; // Error code 2: No eligible employees in max-sales department
            }

            foreach (var employee in eligibleEmployees)
            {
                if (employee.Salary >= 15000 || employee.IsManager)
                {
                    employee.Salary += 100; // Manager or salary >= 15000 gets $100
                }
                else
                {
                    employee.Salary += 200; // Regular employee gets $200
                }
            }

            return 0; // Success
        }

        public List<Employee> GetEmployees() => employees;
    }
}
