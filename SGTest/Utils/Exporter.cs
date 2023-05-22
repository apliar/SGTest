using Microsoft.EntityFrameworkCore;
using SGTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTest.Utils
{
    internal class Exporter
    {
        public void Export()
        {
            using var db = new SGTestContext();

            var departments = db.Departments
                .Where(d => d.ParentId == 0)
                .OrderBy(d => d.Name)
                .Include(d => d.Manager)
                    .ThenInclude(m => m!.JobTitle)
                .ToList();

            LogToConsole(departments, 1);
        }

        public void Export(int id)
        {
            using var db = new SGTestContext();
            var requestedDepartment = db.Departments
                .Include(d => d.Manager)
                    .ThenInclude(m => m!.JobTitle)
                .FirstOrDefault(d => d.Id == id);
            if(requestedDepartment == null)
            {
                Console.WriteLine("Подразделение с указанным id не найдено");
                return;
            }

            int nestingLevel = 1;
            var currentDepartment = requestedDepartment;
            var departmentsStack = new Stack<Department>();
            departmentsStack.Push(currentDepartment);
            while (currentDepartment.ParentId > 0)
            {
                currentDepartment = db.Departments.FirstOrDefault(d => d.Id == currentDepartment.ParentId);
                if(currentDepartment == null)
                {
                    Console.WriteLine("Произошла ошибка при чтении информации о подразделении");
                    return;
                }

                departmentsStack.Push(currentDepartment);
                nestingLevel++;
            }

            for(var i = 1; i <= nestingLevel; i++)
            {
                var department = departmentsStack.Pop();
                Console.WriteLine(new string('=', i) + String.Format(" {0} ID={1}", department.Name, department.Id));
            }

            var employees = db.Employees
                        .Where(e => e.department == requestedDepartment.Id)
                        .OrderBy(e => e.FullName)
                        .Include(e => e.JobTitle)
                        .ToList();

            if (requestedDepartment.Manager != null)
            {
                Console.WriteLine(new string(' ', nestingLevel - 1) + "* " + GetEmployeeInfo(requestedDepartment.Manager));
                employees.Remove(requestedDepartment.Manager);
            }
            foreach (var employee in employees)
            {
                Console.WriteLine(new string(' ', nestingLevel - 1) + "- " + GetEmployeeInfo(employee));
            }
        }

        private void LogToConsole(List<Department> departments, int nestingLevel)
        {
            foreach (var department in departments)
            {
                Console.WriteLine(new string('=', nestingLevel) + String.Format(" {0} ID={1}", department.Name, department.Id));
                using (var db = new SGTestContext())
                {
                    var employees = db.Employees
                        .Where(e => e.department == department.Id)
                        .OrderBy(e => e.FullName)
                        .Include(e => e.JobTitle)
                        .ToList();

                    if(department.Manager != null)
                    {
                        Console.WriteLine(new string(' ', nestingLevel - 1) + "* " + GetEmployeeInfo(department.Manager));
                        employees.Remove(department.Manager);
                    }
                    foreach(var employee in employees)
                    {
                        Console.WriteLine(new string(' ', nestingLevel - 1) + "- " + GetEmployeeInfo(employee));
                    }

                    var nestingDepartments = db.Departments
                        .Where(d => d.ParentId == department.Id)
                        .OrderBy(d => d.Name)
                        .Include(d => d.Manager)
                            .ThenInclude(m => m!.JobTitle)
                        .ToList();
                    LogToConsole(nestingDepartments, nestingLevel + 1);
                }
            }
        }

        private string GetEmployeeInfo(Employee employee)
        {
            var employeeInfo = String.Format("{0} ID={1} ", employee.FullName, employee.Id);
            if (employee.JobTitle != null)
            {
                employeeInfo += String.Format("({0} ID={1})", employee.JobTitle.Name, employee.JobTitle.Id);
            }

            return employeeInfo;
        }
    }
}
