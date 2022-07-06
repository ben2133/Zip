using System;
using System.IO.Compression;

namespace Zip
{
    class Program
    {
        /// <summary> Функция создание новых архиво </summary>
        /// <param name="Directory"> Принимает название директории обизательный параметр </param>
        /// <param name="Type"> Тип создаваемого архива по Default = .zip </param>
        /// <param name="NewArchive"> Название создаеваемого архива по Default = archive </param>
        /// <returns> Если архив создан успешно возврашает true. В ином случае false </returns>
        public static string CreateArchive(string Directory, string Type = "zip", string NewArchive = "ouput_archive")
        {
            try
            {
                ZipFile.CreateFromDirectory(Directory, (NewArchive + "." + Type)); // args[1], (args[3] + "." + args[2])
                return NewArchive + "." + Type + "\nArchive created";
            }
            catch
            {
                return "Error with create new archive";
            }
        }

        /// <summary> Функция разврхивировнаия </summary>
        /// <param name="Archive"> Название архива </param>
        /// <param name="Directory"> Дироектория для разархивацию </param>
        /// <returns> True Если разархивация получиться Flase Если не получается разархивация </returns>
        public static string GetFromArchive(string Archive, string Directory = "UnZip")
        {
            try
            {
                ZipFile.ExtractToDirectory(Archive, Directory);
                return $"Unzip file succes от directory {Directory} !!!";
            }
            catch
            {
                return "Unzip failed!!!";
            }
        }

        /// <summary> Функция для чтения данных из арзива </summary>
        /// <param name="arhive"> Название архива </param>
        /// <returns> Возврашает массив с названиеями файла которые находятся в архиве. Возврашает null если архив пуст </returns>
        public static string[] ReadArchive(string arhive)
        {
            try
            {
                // открытие архива в режиме чтения
                using (ZipArchive zipArchive = ZipFile.OpenRead(arhive))
                {
                    string[] fileName = new string[zipArchive.Entries.Count];

                    for (int i = 0; i < zipArchive.Entries.Count; i++)
                    {
                        fileName[i] = zipArchive.Entries[i].FullName;
                    }

                    return fileName;
                }
            }
            catch
            {
                return new string[] { };
            }
        }

        static void Main(string[] args)
        { 
            // Help:
            if (args[0] == "-h")
                Console.WriteLine("\nCreating new archive:\n" +
                    "\tzip /c [filename] or [directory path] - Create new archive\n" +
                    "\tzip /c [filename] or [directory path] [archive type] - Create new archive\n" +
                    "\tzip /c [filename] or [directory path] [archive type] [new archive name] - Create new archive\n\n" +
                    "Read file list from archive:\n" +
                    "\tzip /r [filename] - Get list of element in the archive\n\n" +
                    "Unziping File:\n" +
                    "\tzip /u [archive file name] - Unziping archive file\n" +
                    "\tzip /u [archive file name] [unziping directory] - Unziping archive file");

            // New archive:
            else if (args[0] == "/c")
            {
                switch (args.Length) {
                    // без название и без типа архива
                    case 2:
                        Console.WriteLine(CreateArchive(args[1]));
                        break;
                    // с типом файла
                    case 3:
                        Console.WriteLine(CreateArchive(args[1], args[2]));
                        break;
                    // с насзвание и с типом файла
                    case 4:
                        Console.WriteLine(CreateArchive(args[1], args[2], args[3]));
                        break;
                }
            }

            // Reading list of file archive:
            else if (args[0] == "/r")
            {
                foreach (string name in ReadArchive(args[1]))
                {
                    Console.WriteLine(name);
                }
            }

            // Unzip archive:
            else if (args[0] == "/u")
            {
                switch (args.Length)
                {
                    case 2:
                        Console.WriteLine(GetFromArchive(args[1]));
                        break;
                    case 3:
                        Console.WriteLine(GetFromArchive(args[1], args[2]));
                        break;
                }
            }
        }
    }
}