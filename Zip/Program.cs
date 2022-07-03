using System;
using System.IO.Compression;

namespace Zip
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args[1][0] != '/')
            {
                // без название архива
                if (args.Length == 2)
                    Console.WriteLine(CreateArchive(args[0], args[1]));

                // все параметры
                else if (args.Length == 3)
                    Console.WriteLine(CreateArchive(args[0], args[1], args[2]));
            }

            else
            {
                // чтения файла
                if (args[1] == "/r")
                {
                    var nameFull = ReadArchive(args[0]);

                    foreach (var name in nameFull)
                    {
                        Console.WriteLine(name);
                    }
                }

                else if (args[1] == "/o")
                {
                    Console.WriteLine( GetFromArchive(args[0], args[2]) );
                }
            }

            return 0;
        }


        /// <summary> Функция создание новых архиво </summary>
        /// <param name="Directory"> Принимает название директории обизательный параметр </param>
        /// <param name="Type"> Тип создаваемого архива по Default = .zip </param>
        /// <param name="NewArchive"> Название создаеваемого архива по Default = archive </param>
        /// <returns> Если архив создан успешно возврашает true. В ином случае false </returns>
        public static Boolean CreateArchive(String Directory, String Type = "zip", String NewArchive = "archive")
        {
            try
            {
                ZipFile.CreateFromDirectory(Directory, (NewArchive + "." + Type)); // args[0], (args[2] + "." + args[1])
                return true;
            } catch
            {
                return false;
            }
        }

        /// <summary> Функция разврхивировнаия </summary>
        /// <param name="Archive"> Название архива </param>
        /// <param name="Directory"> Дироектория для разархивацию </param>
        /// <returns> True Если разархивация получиться Flase Если не получается разархивация </returns>
        public static Boolean GetFromArchive( String Archive, String Directory )
        {
            try
            {
                ZipFile.ExtractToDirectory(Archive, Directory);
                return true;
            } catch
            {
                return false;
            }
        }

        /// <summary> Функция для чтения данных из арзива </summary>
        /// <param name="arhive"> Название архива </param>
        /// <returns> Возврашает массив с названиеями файла которые находятся в архиве. Возврашает null если архив пуст </returns>
        public static String[] ReadArchive(String arhive)
        {
            String[] fileName;
            try
            {
                // открытие архива в режиме чтения
                using (ZipArchive zipArchive = ZipFile.OpenRead(arhive))
                {
                    fileName = new String[zipArchive.Entries.Count];
                    for (int i = 0; i < zipArchive.Entries.Count; i++)
                    {
                        fileName[i] = zipArchive.Entries[i].FullName;
                    }
                    return fileName;
                }
            } catch
            {
                return fileName = new String[] { };
            }
        }

    }
}
