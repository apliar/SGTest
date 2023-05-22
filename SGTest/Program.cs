using SGTest.Services;
using SGTest.Utils;

namespace SGTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Отсутствуют аргументы командной строки");
                return;
            }
            else if(args.Length > 3)
            {
                Console.WriteLine("Указано неверное количество аргументов");
                return;
            }

            switch (args[0])
            {
                case "import":
                    if (args.Length > 3)
                    {
                        Console.WriteLine("Указано неверное количество аргументов");
                        return;
                    }

                    if (args.Length == 1)
                    {
                        Console.WriteLine("Укажите имя файла (.tsv)");
                        return;
                    }
                    var filePath = "../../../data/";

                    var fileName = filePath + args[1];

                    if(args.Length == 2)
                    {
                        Console.WriteLine("Укажите тип импорта (department/employee/jobtitle)");
                        return;
                    }
                    var type = args[2];

                    switch (type)
                    {
                        case "department":
                            var depImport = new Importer(new DepartmentService(), fileName);
                            depImport.Import();
                            break;
                        case "employee":
                            var empImport = new Importer(new EmployeeService(), fileName);
                            empImport.Import();
                            break;
                        case "jobtitle":
                            var jobImport = new Importer(new JobTitleService(), fileName);
                            jobImport.Import();
                            break;
                    }
                    break;
                case "export":
                    if (args.Length > 2)
                    {
                        Console.WriteLine("Указано неверное количество аргументов");
                        return;
                    }

                    if(args.Length == 1)
                    {
                        var exp = new Exporter();
                        exp.Export();
                    }
                    else if(args.Length == 2)
                    {
                        if(int.TryParse(args[1], out var id))
                        {
                            var exp = new Exporter();
                            exp.Export(id);
                        }
                        else
                        {
                            Console.WriteLine("Неверно указан id подразделения");
                            return;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Первый аргумент указывает на режим работы программы: import/export");
                    break;
            }
        }
    }
}