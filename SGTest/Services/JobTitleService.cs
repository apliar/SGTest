using SGTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTest.Services
{
    internal class JobTitleService : IService
    {
        public void SaveToDb(string[] parsedLine, int lineNumber)
        {
            if (parsedLine.Length > 1)
            {
                Console.Error.WriteLine(@"Ошибка чтения строки номер {0}, строка пропущена", lineNumber);
                return;
            }

            var job = new JobTitle { Name = StringCleaner.Clean(parsedLine[0]) };
            using (var db = new SGTestContext())
            {
                var alreadyHave = db.JobTitles.FirstOrDefault(jt => jt.Name == job.Name);
                if (alreadyHave != null) return;

                db.JobTitles.Add(job);
                db.SaveChanges();
            }
        }
    }
}
