using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace failManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте, вас приветствует файловый менеджер Алеша");
            // Вызываем метод с меню.
            StarterPack();
            string Drive = "";
            do
            {
                Console.WriteLine("Алеша готов к работе");
                Console.WriteLine("");
                // Просим у пользоватея ввести номер команды из меню и проверяем его на корректность.
                string inputStr = Console.ReadLine();
                // Проверяем корректность ввода.
                if (int.TryParse(inputStr, out int input))
                {
                    // Вызываем наш основной метод, который делает большую часть работы.
                    WhatCanAleshaDoWithThis(input, Drive);
                }
                else 
                {
                    Console.WriteLine("Пожалуйста, введите корректное значение.(если Алеша разазлится, то отформатирует жесткий диск)");
                    Console.WriteLine("");
                }
                // Осуществляем повтор решения.
                Console.WriteLine("Нажмите Enter, чтобы попробовать еще раз");
            } while (Console.ReadKey(true).Key == ConsoleKey.Enter);
        }

        // Метод получает номер команды и в зависимости от команды выполняет нужные действия.
        public static void WhatCanAleshaDoWithThis(int input, string Drive)
        {
            // Длинный метод обусловлен switch, все равно от этого метода выполняется только часть, так что он не перегружен. Простите пожалуйста :).
            switch (input)
            {
                case 1:
                    {
                        int count = 0;
                        // Записываем в массив строк диски, которые есть на компьютере.
                        string[] Drives = Environment.GetLogicalDrives();
                        if (Drives[0] != "")
                        {
                            // Выводим список дисков.
                            foreach (string i in Drives)
                            {
                                count += 1;
                                Console.WriteLine(count + ". " + i);
                            }
                            Console.WriteLine("Выберите, с каким диском вы хотите работать, введите его номер");
                            // С помощью метода получаем диск.
                            Drive = Verify(Console.ReadLine(), Drives);
                            if (Drive == "!")
                                Console.WriteLine("Введенные вами данные некорректны");
                            else
                            {
                                Console.WriteLine("сейчас вы находитесь в "+ Drive);
                            }
                        }
                        else
                            Console.WriteLine("Ошибка, не найдено ни одного диска");
                        break;
                    }
                case 2:
                    {
                        // Проверяем выбрал ли пользователь диск для работы.
                        if (Drive != "")
                        {
                            // Получаем строку с адресом и проверяем ее.
                            string TryDrive = OutputDirectories(Drive);
                            if (TryDrive == ".")
                            {
                                // Загадка мак или виндоус, на размышление дается 1 секунда.
                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                                {
                                    Drive = Drive.Substring(0, Drive.LastIndexOf("\\"));
                                    if (!(Drive.Contains("\\")))
                                        Drive += "\\";
                                }
                                else
                                {
                                    Drive = Drive.Substring(0, Drive.LastIndexOf("/"));
                                    if (!(Drive.Contains("/")))
                                        Drive += "/";
                                }
                                Console.WriteLine("Теперь вы находитесь в " + Drive);
                            }
                            else if (TryDrive != "!")
                            {
                                Drive = TryDrive;
                                Console.WriteLine("Теперь вы находитесь в " + Drive);
                            }
                            else
                            {
                                Console.WriteLine("Некорректные данные");
                                Console.WriteLine("Вы все еще в "+ Drive);
                                Console.WriteLine("Можете вернуться к выбору дисков через команду 1 и попробовать снова");

                            }
                        }
                        else
                            Console.WriteLine("Пожалуйста, выберите сначала диск, с которым хотите работать(пункт 1 в меню)");
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Выберите кодировку, в которой вы хотите, чтобы выводился текст(UTF8, Unicode, UTF7, ASCII)");
                        string encoding = Console.ReadLine();
                        Console.WriteLine("Введите имя файла для чтения(если вы находитесь в директории с ним)");
                        string nameFile = Console.ReadLine();
                        // Загадка мак или виндоус, на размышление дается 1 секунда.
                        string Separator;
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            Separator = "\\";
                        }
                        else
                        {
                            Separator = "/";
                        }
                        try
                        {
                            // Смотрим что ввел пользователь и выводим файл в нужной кодировке.
                            switch (encoding)
                            {
                                case "UTF8":
                                    {
                                        Console.WriteLine("В файле написано:");
                                        Console.WriteLine(File.ReadAllText(Drive + Separator + nameFile, System.Text.Encoding.UTF8));
                                        break;
                                    }
                                case "Unicode":
                                    {
                                        Console.WriteLine("В файле написано:");
                                        Console.WriteLine(File.ReadAllText(Drive + Separator + nameFile, System.Text.Encoding.Unicode));
                                        break;
                                    }
                                case "UTF7":
                                    {
                                        Console.WriteLine("В файле написано:");
                                        Console.WriteLine(File.ReadAllText(Drive + Separator + nameFile, System.Text.Encoding.UTF7));
                                        break;
                                    }
                                case "ASCII":
                                    {
                                        Console.WriteLine("В файле написано:");
                                        Console.WriteLine(File.ReadAllText(Drive + Separator + nameFile, System.Text.Encoding.ASCII));
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Введенные вами данные некорректны");
                                        break;
                                    }

                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Ошибочка");
                        }

                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Введите имя файла, который вы хотите скопировать");
                        string inputNameOut = Console.ReadLine();
                        Console.WriteLine("Введите имя файла, куда вы хотите скопировать(если такого файла не существует, то он автоматически создастся)");
                        string inputNameIn = Console.ReadLine();
                        string Separator;
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            Separator = "\\";
                        }
                        else
                        {
                            Separator = "/";
                        }
                        try
                        {
                            // создаем файл, если его нет.
                            if (!(File.Exists(Drive + Separator + inputNameIn)))
                            {
                                string way = Drive + Separator + inputNameIn;
                                FileInfo MyFile = new FileInfo(@way);
                                FileStream fs = MyFile.Create();
                                fs.Close();
                            }
                            string DriveToCopyOut = Drive + Separator + inputNameOut;
                            string DriveToCopyIn = Drive + Separator + inputNameIn;
                            // Копируем файл.
                            File.Copy(@DriveToCopyOut, @DriveToCopyIn, true);
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("Ошибка");
                        }

                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Если вы находитесь в папке, где лежит файл, который вы хотите переместить, то введите его имя");
                        string inputNameOut = Console.ReadLine();
                        string Separator;
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            Separator = "\\";
                        }
                        else
                        {
                            Separator = "/";
                        }
                        string DriveToCopyOut = Drive + Separator + inputNameOut;
                        Console.WriteLine("Теперь перейдите в директорию, в которой вы хотите, чтобы оказался ваш файл");
                        Drive = "";
                        int count = 0;
                        // Записываем в массив строк диски, которые есть на компьютере.
                        string[] Drives = Environment.GetLogicalDrives();
                        if (Drives[0] != "")
                        {
                            foreach (string i in Drives)
                            {
                                // Выводим список дисков.
                                count += 1;
                                Console.WriteLine(count + ". " + i);
                            }
                            Console.WriteLine("Выберите, с каким диском вы хотите работать, введите его номер");
                            Drive = Verify(Console.ReadLine(), Drives);
                            if (Drive == "!")
                                Console.WriteLine("Введенные вами данные некорректны");
                            else
                            {
                                Console.WriteLine("сейчас вы находитесь в " + Drive);
                            }
                        }
                        else
                            Console.WriteLine("Ошибка, не найдено ни одного диска");
                        Console.WriteLine("Если вы оказались в нужной вам папке, то введите 'there', если нет, то любую другую строку");
                        string OUT = Console.ReadLine();

                        while (OUT != "there")
                        {
                            // Производим выбор директории.
                            if (Drive != "")
                            {
                                string TryDrive = OutputDirectories(Drive);
                                if (TryDrive == ".")
                                {
                                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                                    {
                                        Drive = Drive.Substring(0, Drive.LastIndexOf("\\"));
                                        if (!(Drive.Contains("\\")))
                                            Drive += "\\";
                                    }
                                    else
                                    {
                                        Drive = Drive.Substring(0, Drive.LastIndexOf("/"));
                                        if (!(Drive.Contains("/")))
                                            Drive += "/";
                                    }
                                    Console.WriteLine("Теперь вы находитесь в " + Drive);
                                }
                                else if (TryDrive != "!")
                                {
                                    Drive = TryDrive;
                                    Console.WriteLine("Теперь вы находитесь в " + Drive);
                                }
                                else
                                {
                                    Console.WriteLine("Некорректные данные");
                                    Console.WriteLine("Вы все еще в " + Drive);

                                }
                            }
                            Console.WriteLine("Если вы оказались в нужной вам папке, то введите 'there', если нет, то любую другую строку");
                            OUT = Console.ReadLine();
                        }
                        // Создаем файл с дурацким названием, туда двигаем файл, потом меняем имя у передвинутого файла на изначальное и готово.
                        string inputNameIn = "sdvnsfbkllbn";
                        try
                        {
                            if (!(File.Exists(Drive + Separator + inputNameIn)))
                            {
                                File.WriteAllText(Drive + Separator + inputNameIn, "");
                            }
                            string DriveToCopyIn = Drive + Separator + inputNameIn;
                            File.Move(@DriveToCopyOut, @DriveToCopyIn, true);
                            DriveToCopyOut = Drive + Separator + inputNameOut;
                            // Тут меняем имя.
                            File.Move(@DriveToCopyIn, @DriveToCopyOut, true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                case 6:
                    {
                        if (Drive != "")
                        {
                            Console.WriteLine("Если вы находитесь в папке, где лежит файл, который вы хотите удалить, то введите его имя");
                            Console.WriteLine("Если хотите вернуться, то введите \"back\"");
                            string inputName = Console.ReadLine();
                            if (inputName != "back")
                            {
                                try
                                {
                                    // Проверяем операционную систему и удаляем файл.
                                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                                    {
                                        string Adress = Drive + "\\" + inputName;
                                        File.Delete(@Adress);
                                    }

                                    else
                                    {
                                        string Adress = Drive + "/" + inputName;
                                        File.Delete(@Adress);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Ошибка");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Пожалуйста, выберите сначала диск, с которым хотите работать(пункт 1 в меню)");
                        }
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("Выберите кодировку, в которой будет содан текстовый файл, введите ее номер в списке");
                        Console.WriteLine("1.UTF8"); 
                        Console.WriteLine("2.Unicode");
                        Console.WriteLine("3.UTF7");
                        Console.WriteLine("4.ASC11");
                        string Separator;
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            Separator = "\\";
                        }
                        else
                        {
                            Separator = "/";
                        }
                        string NumbStr = Console.ReadLine();
                        if (int.TryParse(NumbStr, out int Numb))
                        {
                            try
                            {
                                if (Numb == 1)
                                {
                                    Console.WriteLine("Введите имя файла");
                                    string name = Console.ReadLine();
                                    // Открываем файл.
                                    StreamWriter sw = new StreamWriter(Drive+ Separator + name, true, Encoding.UTF8);
                                    Console.WriteLine("Введите текст");
                                    // Вписываем туда разные вещи.
                                    sw.Write(Console.ReadLine());
                                    //Закрываем файл.
                                    sw.Close();
                                }
                                else if (Numb == 2)
                                {
                                    Console.WriteLine("Введите имя файла");
                                    string name = Console.ReadLine();
                                    // Открываем файл.
                                    StreamWriter sw = new StreamWriter(Drive + Separator + name, true, Encoding.Unicode);
                                    Console.WriteLine("Введите текст");
                                    // Вписываем туда разные вещи.
                                    sw.Write(Console.ReadLine());
                                    //Закрываем файл.
                                    sw.Close();
                                }
                                else if (Numb == 3)
                                {
                                    Console.WriteLine("Введите имя файла");
                                    string name = Console.ReadLine();
                                    // Открываем файл.
                                    StreamWriter sw = new StreamWriter(Drive + Separator + name, true, Encoding.UTF7);
                                    Console.WriteLine("Введите текст");
                                    // Вписываем туда разные вещи.
                                    sw.Write(Console.ReadLine());
                                    //Закрываем файл.
                                    sw.Close();
                                }
                                else if (Numb == 4)
                                {
                                    Console.WriteLine("Введите имя файла");
                                    string name = Console.ReadLine();
                                    // Открываем файл.
                                    StreamWriter sw = new StreamWriter(Drive + Separator + name, true, Encoding.ASCII);
                                    Console.WriteLine("Введите текст");
                                    // Вписываем туда разные вещи.
                                    sw.Write(Console.ReadLine());
                                    //Закрываем файл.
                                    sw.Close();
                                }
                                else
                                {
                                    Console.WriteLine("Алеша не видит введенный вами номер в списке, хотите узнать почему?");
                                    Console.WriteLine("                                             потому что его там нет");
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Ошибка");
                            }
                        }
                        else
                            Console.WriteLine("Пожалуйста, введите число из списка");

                        break;
                    }
                case 8:
                    {
                        string Separator;
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                            Separator = "\\";
                        else
                            Separator = "/";
                        Console.WriteLine("Для успешной конкатенации файлов они должны быть расположены в одной директории и вообще существовать");
                        Console.WriteLine("Введите имя первого файла");
                        string firstFile = Console.ReadLine();
                        Console.WriteLine("Введите имя второго файла");
                        string secondFile = Console.ReadLine();
                        Console.WriteLine("Введите имя файла, куда нужно записать результат");
                        string lastFile = Console.ReadLine();
                        try
                        {
                            // В результирующий файл записываем содержимое первого файла.
                            File.WriteAllLines(Drive +Separator+lastFile, File.ReadAllLines(Drive + Separator + firstFile));
                            // В результирующий файл добавляем содержимое второго файла.
                            File.AppendAllLines(Drive + Separator + lastFile, File.ReadAllLines(Drive + Separator + secondFile));
                            Console.WriteLine("Результат:");
                            Console.WriteLine(File.ReadAllText(Drive + Separator + lastFile));
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка");
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Алеша не видит введенный вами номер в меню, хотите узнать почему?");
                        Console.WriteLine("                                           потому что его там нет");
                        break;
                    }
            }
            Console.WriteLine("введите \"меню\", если хотите узнать с чем Алеша может помочь");
            Console.WriteLine("введите \"выход\", если хотите Алеша уже сделал все, что вам было нужно");
            Console.WriteLine("");
            Console.WriteLine("Введите следующую команду");
            string inputStr = Console.ReadLine();
            // Обеспечиваем повтор путем вызова метода еще раз.
            if (int.TryParse(inputStr, out int inputMetod))
            {
                WhatCanAleshaDoWithThis(inputMetod, Drive);
            }
            else if (inputStr == "меню")
            {
                StarterPack();
                WhatCanAleshaDoWithThis(inputMetod, Drive);
            }
            else if (inputStr == "выход")
                Drive = "";
            else
            {
                Console.WriteLine("Пожалуйста, введите корректное значение.(если Алеша разазлится, то отформатирует жесткий диск)");
                WhatCanAleshaDoWithThis(inputMetod, Drive);
            }
        }
        // Метод выводит список директорий и просит выбрать из них что-либо.
        private static string OutputDirectories(string Drive)
        {
            try
            {
                string[] directories = Directory.GetDirectories(Drive);
                int count = 0;
                foreach (var item in directories)
                {
                    count += 1;
                    Console.WriteLine(count + ". " + item);
                }
                Console.WriteLine("Введите номер директории в списке, чтобы прейти к ней или \"down\", чтобы вернуться в пердыдущую директорию");
                string inputStr = Console.ReadLine();
                // Проверяем ввод с помощью метода.
                return Verify(inputStr, directories);
            }
            catch
            {
                return "!";
            }
        }
        // Метод проверяет ввод.
        private static string Verify(string inputStr, string[] Drives)
        {
            if ((int.TryParse(inputStr, out int input)) && (input < Drives.Length + 1))
            {
                if (input > 0)
                    return Drives[input - 1];
                else
                    return "!";
            }
            else if (inputStr == "down")
                return ".";
            else
            {
                return "!";
            }
        }
        // Метод выводит меню.
        public static void StarterPack()
        {
            Console.WriteLine("Выберите услугу и введите ее номер в меню, представленном ниже");
            Console.WriteLine("");
            Console.WriteLine("Алеша поможет, если вас интересует:");
            Console.WriteLine("1. просмотр списка дисков компьютера и выбор диска;");
            Console.WriteLine("2. просмотр списка файлов в директории и переход в другую директорию (выбор папки);");
            Console.WriteLine("3. вывод содержимого текстового файла в консоль в выбранной кодировке;");
            Console.WriteLine("4. копирование файла;");
            Console.WriteLine("5. перемещение файла в выбранную директорию;");
            Console.WriteLine("6. удаление файла;");
            Console.WriteLine("7. создание простого текстового файла в выбранной кодировке;");
            Console.WriteLine("8. конкатенация содержимого двух или более текстовых файлов и вывод результата в консоль в кодировке UTF-8.");
        }
    }
}
