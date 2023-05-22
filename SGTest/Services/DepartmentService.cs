using SGTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTest.Services
{
    internal class DepartmentService : IService
    {
        public void SaveToDb(string[] parsedLine, int lineNumber)
        {
            if (parsedLine.Length != 4)
            {
                Console.Error.WriteLine(@"Ошибка чтения строки номер {0}, строка пропущена", lineNumber);
                return;
            }

            using (var db = new SGTestContext())
            {
                var department = new Department();

                department.Name = StringCleaner.Clean(parsedLine[0]);
                if (department.Name == String.Empty)
                {
                    Console.Error.WriteLine(@"Ошибка чтения строки номер {0}, строка пропущена", lineNumber);
                    return;
                }

                var parentName = StringCleaner.Clean(parsedLine[1]);
                if (parentName == String.Empty) department.ParentId = 0;
                else
                {
                    var parent = db.Departments.FirstOrDefault(d => d.Name == parentName);
                    if (parent == null)
                    {
                        parent = new Department() { Name = parentName };
                        db.Departments.Add(parent);
                        db.SaveChanges();
                    }

                    department.ParentId = parent.Id;
                }

                var managerName = StringCleaner.Clean(StringCleaner.Clean(parsedLine[2]).Split(" "));
                if (managerName != String.Empty)
                {
                    var manager = db.Employees.FirstOrDefault(e => e.FullName == managerName);
                    if (manager == null)
                    {
                        manager = new Employee() { FullName = managerName };
                        db.Employees.Add(manager);
                        db.SaveChanges();
                    }

                    department.manager_id = manager.Id;
                }

                department.Phone = StringCleaner.Clean(parsedLine[3]);

                var alreadyHave = db.Departments
                    .FirstOrDefault(d => d.Name == department.Name);
                if (alreadyHave != null)
                {
                    if (!alreadyHave.Equals(department))
                    {
                        alreadyHave.manager_id = department.manager_id;
                        alreadyHave.Phone = department.Phone;
                        alreadyHave.ParentId = department.ParentId;

                        db.Departments.Update(alreadyHave);
                    }
                }
                else
                {
                    db.Departments.Add(department);
                }

                db.SaveChanges();
            }
        }
    }
}
