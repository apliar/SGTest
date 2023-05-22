using SGTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTest.Services
{
    internal class EmployeeService : IService
    {
        public void SaveToDb(string[] parsedLine, int lineNumber)
        {
            if (parsedLine.Length != 5)
            {
                Console.Error.WriteLine(@"Ошибка чтения строки номер {0}, строка пропущена", lineNumber);
                return;
            }

            using (var db = new SGTestContext())
            {
                var employee = new Employee();

                var departmentName = StringCleaner.Clean(parsedLine[0]);
                if (departmentName == String.Empty)
                {
                    Console.Error.WriteLine(@"Ошибка чтения строки номер {0}, строка пропущена", lineNumber);
                    return;
                }
                else
                {
                    var department = db.Departments.FirstOrDefault(d => d.Name == departmentName);
                    if (department == null)
                    {
                        department = new Department() { Name = departmentName };
                        db.Departments.Add(department);
                        db.SaveChanges();
                    }

                    employee.department = department.Id;
                }

                employee.FullName = StringCleaner.Clean(StringCleaner.Clean(parsedLine[1]).Split(" "));
                if (employee.FullName == String.Empty)
                {
                    Console.Error.WriteLine(@"Ошибка чтения строки номер {0}, строка пропущена", lineNumber);
                    return;
                }

                employee.Login = StringCleaner.Clean(parsedLine[2]);
                employee.Password = StringCleaner.Clean(parsedLine[3]);

                var jobName = StringCleaner.Clean(parsedLine[4]);
                if (jobName != String.Empty)
                {
                    var job = db.JobTitles.FirstOrDefault(j => j.Name == jobName);
                    if (job == null)
                    {
                        job = new JobTitle() { Name = jobName };
                        db.JobTitles.Add(job);
                        db.SaveChanges();
                    }

                    employee.jobtitle = job.Id;
                }


                var alreadyHave = db.Employees.FirstOrDefault(e => e.FullName == employee.FullName);
                if (alreadyHave != null)
                {
                    if (!alreadyHave.Equals(employee))
                    {
                        alreadyHave.department = employee.department;
                        alreadyHave.Login = employee.Login;
                        alreadyHave.Password = employee.Password;
                        alreadyHave.jobtitle = employee.jobtitle;

                        db.Employees.Update(alreadyHave);
                    }
                }
                else
                {
                    db.Employees.Add(employee);
                }

                db.SaveChanges();
            }
        }
    }
}
