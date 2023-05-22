using SGTest.Models;
using SGTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTest.Utils
{
    internal class Importer
    {
        private readonly IService _service;
        private string _filename;

        public Importer(IService service, string filename)
        {
            _service = service;
            _filename = filename;
        }

        public void Import()
        {
            int currentLine = 2;
            using StreamReader reader = new StreamReader(_filename);
            if (String.IsNullOrEmpty(reader.ReadLine())) Console.Error.WriteLine("Ошибка чтения файла");
            else
            {
                string[] parsedValue;
                string? readLine;
                while ((readLine = reader.ReadLine()) != null)
                {
                    if (String.IsNullOrEmpty(readLine) || String.IsNullOrWhiteSpace(readLine))
                    {
                        Console.Error.WriteLine(@"Ошибка чтения строки номер {0}, строка пропущена", currentLine);
                        continue;
                    }

                    parsedValue = readLine.Split("\t");
                    _service.SaveToDb(parsedValue, currentLine);

                    currentLine++;
                }
            }

            ExportAllData();
        }

        private void ExportAllData()
        {
            using var db = new SGTestContext();

            Console.WriteLine("id\tparent_id\tmanager_id\tname\t\tphone");
            foreach(var dp in db.Departments)
            {
                Console.WriteLine(String.Format("{0}\t{1}\t\t{2}\t\t{3}\t{4}", 
                    dp.Id, dp.ParentId, dp.manager_id, dp.Name, dp.Phone));
            }
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("id\tdepartment\tfullname\tlogin\tpassword\tjobtitle");
            foreach (var emp in db.Employees)
            {
                Console.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", 
                    emp.Id, emp.department, emp.FullName, emp.Login, emp.Password, emp.jobtitle));
            }
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("id\tname");
            foreach (var jt in db.JobTitles)
            {
                Console.WriteLine(String.Format("{0}\t{1}", jt.Id, jt.Name));
            }
            Console.WriteLine(new string('-', 100));
        }
    }
}
