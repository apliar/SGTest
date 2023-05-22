Тестовое задание для STARKOV-Group

-SQL скрипт для создания структуры БД находится в файле script.sql
-Настройки доступа к БД находятся в файле App.config
-Файлы TSV находятся в папке /data. При стандартной компиляции (bin-Debug-net6.0) файлы будут браться из этой папки.
Для изменения пути к файлам - изменить переменную filePath в файле Program.cs строка 35
-Пакеты используемые в проекте:
	Microsoft.EntityFrameworkCore (7.0.5)
	Npgsql.EntityFrameworkCore.PostgreSQL (7.0.4)
	System.Configuration.ConfigurationManager (7.0.0)

Инструкция для запуска:
1.Импорт - при запуске программы в командной строке указывается 3 параметра: import (filename.tsv) (importType:department/employee/jobtitle)
2.Вывод - при запуске программы в командной строке указывается параметр export, в качестве второго параметра можно указать ID подразделения