using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_5
{
    class MAIN
    {
        enum Pos
        {
            П,
            С,
            А,
            None
        }



        public const int WATCH_TABLE = 1;
        public const int ADD_RAW = 2;
        public const int REMOVE_RAW = 3;
        public const int UPDATE_RAW = 4;
        public const int FIND_RAW = 5;
        public const int SHOW_LOG = 6;
        public const int EXIT = 7;

        public struct HRD
        {
            public string Name; // Фамилия
            public string Postion;//Долнжость
            public int Yers;//Год рождения
            public string Salary;//Оклад

            internal void ShowTable(string name, string Postion, int Yers, string Salary)
            {
                Console.Write("{0,10}", name);
                Console.Write("{0,10}", Postion);
                Console.Write("{0,10}", Yers);
                Console.Write("{0,10}", Salary);
                Console.WriteLine();
            }
        }

        //Списки
        static List<HRD> list = new List<HRD>(50);



        //Список опираций
        enum Operations
        {
            ADD,
            DELETE,
            UPDATE,
            LOOK,
            SEARCH
        };

        //ЛОГИРОВАНИЕ
        struct Logging
        {
            static List<Logging> log = new List<Logging>();
            public DateTime time;
            public Operations action;
            public String data;

            public static Logging Add(DateTime dt, Operations operation, string s)
            {
                log.Add(new Logging(dt, operation, s));
                return log[log.Count - 1];
            }

            public Logging(DateTime Time, Operations Operations, String Date)
            {
                time = Time;
                action = Operations;
                data = Date;
            }

            public static void ShowInfo()
            {

                foreach (Logging l in log)
                {
                    l.PrintLog();
                }
            }
            public void PrintLog()
            {
                Console.Write("{0,10}", time);
                Console.Write("{0,20}  ", action);
                Console.WriteLine("{0,10}", data);
            }



        }


        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Выберите пункт");
                Console.WriteLine("1 - Просмотр таблицы");
                Console.WriteLine("2 - добавить запись");
                Console.WriteLine("3 - Удалить запись");
                Console.WriteLine("4 - обновить запись");
                Console.WriteLine("5 - поиск записей");
                Console.WriteLine("6 - просмотреть лог");
                Console.WriteLine("7 - Выход");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case WATCH_TABLE:
                        Console.WriteLine("{0,10} {1,10} {2,10} {3,10}", "Фамилия", "Должность", "Год рождения", "Оклад");
                        for (int list_item = 0; list_item < list.Count; list_item++)
                        {
                            HRD t = list[list_item];
                            Console.WriteLine("----------------------------------------------------------------------------");
                            t.ShowTable(t.Name, t.Postion, t.Yers, t.Salary);

                        }
                        Logging.Add(DateTime.Now, Operations.LOOK, "Просмотрена таблица");
                        break;

                    case ADD_RAW:
                        HRD t1;
                        Console.WriteLine("Введите Фамилию");
                        t1.Name = Console.ReadLine();
                        Console.WriteLine("ВведитеДолжность");
                        t1.Postion = Console.ReadLine();

                    Found1:
                        Console.WriteLine("Введите год рождения");
                        try
                        {
                            int blabla = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                            t1.Yers = blabla;
                            if ((blabla < 1895) || (blabla > 2030))
                            {
                                Console.WriteLine("Error. (Введите повторно)");
                                goto Found1;
                            }
                        }
                        catch (FormatException)
                        {
                            t1.Yers = 000;
                            Console.WriteLine("Error. (Введите повторно)");
                            goto Found1;
                        }
                        Pos pro;
                    Found3:
                        Console.WriteLine("Введите оклад");
                        try
                        {
                            string blabla3 = Console.ReadLine();
                            t1.Salary = blabla3;
                            pro = (Pos)Enum.Parse(typeof(Pos), blabla3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error. (Введите повторно)");
                            pro = Pos.None;
                            goto Found3;
                        }

                        list.Add(t1);
                        Console.WriteLine("Строка была добавлена!");
                        Console.WriteLine();
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка добавлена в таблицу!");
                        break;
                    case REMOVE_RAW:
                        Console.WriteLine("Введите номер строки, которую хотите удалить");
                        int number = int.Parse(Console.ReadLine());
                        try
                        {
                            list.RemoveAt(number - 1);
                        }
                        catch (Exception e) { Console.WriteLine("Строки с таким номером нет!"); }
                        Console.WriteLine();
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка удалена!");
                        break;
                    case UPDATE_RAW:
                        Console.WriteLine("Введите номер строки, которую хотите изменить");
                        int UpdateIndex = int.Parse(Console.ReadLine());
                        try
                        {
                            HRD t2 = list[UpdateIndex - 1];

                            //Выводим старые значения

                            t2.ShowTable(t2.Name, t2.Postion, t2.Yers, t2.Salary);


                            //Вводим новые значения

                            Console.WriteLine("Введите новое фамилию");
                            t2.Name = Console.ReadLine();
                            Console.WriteLine("Введите новую должность");
                            t2.Postion = Console.ReadLine();
                            Console.WriteLine("Введите новый год рождения");
                        Found2:
                            t2.Yers = int.Parse(Console.ReadLine());
                            try
                            {
                                int blabla2 = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                                t2.Yers = blabla2;
                                if ((blabla2 < 1895) || (blabla2 > 2030))
                                {
                                    Console.WriteLine("Error. (Введите повторно)");
                                    goto Found2;
                                }
                            }
                            catch (FormatException)
                            {
                                t2.Yers = 000;
                                Console.WriteLine("Error. (Введите повторно)");
                                goto Found2;
                            }
                            Console.WriteLine("Введите новый Тип");
                            Pos pro2;
                        Found4:
                            try
                            {
                                string blabla4 = Console.ReadLine();
                                t2.Salary = blabla4;
                                pro2 = (Pos)Enum.Parse(typeof(Pos), blabla4);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error. (Введите повторно)");
                                pro2 = Pos.None;
                                goto Found4;
                            }
                            list[UpdateIndex - 1] = t2;

                        }
                        catch (Exception e) { Console.WriteLine("Нет строки с таким номером!"); }
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка обновлена!");
                        break;
                    case FIND_RAW:
                        Console.WriteLine("Введите фамилию");
                        string text = Console.ReadLine();
                        HRD FindRaw;
                        for (int item_list = 0; item_list < list.Count; item_list++)
                        {
                            FindRaw = list[item_list];
                            if (FindRaw.Name.ToLower().Equals(text.ToLower()))
                            {
                                Console.Write("{0,10}", FindRaw.Name);
                                Console.Write("{0,10}", FindRaw.Postion);
                                Console.Write("{0,10}", FindRaw.Yers);
                                Console.Write("{0,10}", FindRaw.Salary);
                                Console.WriteLine();
                            }
                        }
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка найдена!");
                        break;
                    case SHOW_LOG:
                        Logging.Add(DateTime.Now, Operations.ADD, "Логи просмотрены!");
                        Logging.ShowInfo();
                        break;
                    case EXIT:
                        break;
                }
            } while (choice != 7);
        }
    }
}

