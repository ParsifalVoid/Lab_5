using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Лаба_5
{
    class Program
    {
        public enum Type { И, А, Т, Неизвестен }

        struct TV_series
        {
            public string title;
            public string Name;
            public int rating;
            public Type type;

            public void Logging_Info()
            {
                Console.WriteLine($"-----------------------------------------------------------------------------");
                Console.WriteLine($"| {title,-16} | {Name,-16} | {rating,-16} | {type,-16} |");
                Console.WriteLine($"-----------------------------------------------------------------------------");
            }
        }
        struct Log
        {
            public string title;
            public string Name;
            public int rating;
            public string Operation;
            public DateTime Time;

            public void Logging()
            {
                Console.WriteLine($"{title,-16}, {Time,-16}, {Operation,-16}");
            }
        }

        static void Main(string[] args)
        {
            TV_series My_Game;
            My_Game.title = "Своя игра";
            My_Game.Name = "П. Кулешов";
            My_Game.rating = 5;
            My_Game.type = Type.И;

            TV_series Sunday_Evening;
            Sunday_Evening.title = "Воскресный вечер";
            Sunday_Evening.Name = "В. Соловьев";
            Sunday_Evening.rating = 5;
            Sunday_Evening.type = Type.А;

            TV_series Let_Them_Talk;
            Let_Them_Talk.title = "Пусть говорят";
            Let_Them_Talk.Name = "А. Малахов";
            Let_Them_Talk.rating = 4;
            Let_Them_Talk.type = Type.Т;

            var table = new List<TV_series>();

            table.Add(My_Game);
            table.Add(Sunday_Evening);
            table.Add(Let_Them_Talk);

            var Log = new List<Log>();
            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now;
            TimeSpan TimeInterval_1 = time2 - time1;

            StreamWriter FileLog= new StreamWriter("C:\\Users\\Пользователь\\Desktop\\LogLab.txt");

            string Menu = "\n1 - Просмотр таблицы\n2 - Добавить запись\n3 - Удалить запись\n4 - Обновить запись\n5 - Поиск записей\n6 - Просмотреть лог\n7 - Выход";
            bool OptionError = true;

            do
            {
                Console.WriteLine(Menu);
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        {
                            for (int i = 0; i < table.Count; i++)
                            {
                                table[i].Logging_Info();
                            }
                        }
                        break;

                    case 2:
                        {
                            Console.WriteLine("Введите название телепередачи: ");
                            string title = Console.ReadLine();

                            Console.WriteLine("Введите имя ведущего: ");
                            string Name = Console.ReadLine();
                            if (Name == string.Empty)
                            {
                                Name = "Неизвестно";
                            }

                            Console.WriteLine("Введите рейтинг телепередачи: ");
                            int rating = 0;
                            bool ratingError = false;
                            do
                            {
                                int Rating = Convert.ToInt32(Console.ReadLine());
                                if (Rating <= 5 && Rating >= 0)
                                {
                                    rating = Rating;
                                    ratingError = false;
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный номер рейтинга: (1, 2, 3, 4, 5)");
                                    ratingError = true;
                                }
                            } while (ratingError == true);

                            Console.WriteLine("Введите жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                            var type = Type.Неизвестен;
                            bool typeError = false;
                            do
                            {
                                string Type_all = Console.ReadLine();
                                if (Type_all == "И")
                                {
                                    type = Type.И;
                                    typeError = false;
                                }
                                else if (Type_all == "А" || Type_all == "A")
                                {
                                    type = Type.А;
                                    typeError = false;
                                }
                                else if (Type_all == "T" || Type_all == "Т")
                                {
                                    type = Type.Т;
                                    typeError = false;
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                                    typeError = true;
                                }
                            } while (typeError == true);

                            TV_series NewTV_series;
                            NewTV_series.title = title;
                            NewTV_series.Name = Name;
                            NewTV_series.rating = rating;
                            NewTV_series.type = type;
                            table.Add(NewTV_series);

                            Log NewLog;
                            NewLog.title = title;
                            NewLog.Name = Name;
                            NewLog.rating = rating;
                            NewLog.Time = DateTime.Now;
                            NewLog.Operation = "Запись добавлена";
                            Log.Add(NewLog);
                            
                            FileLog.Write(title);
                            FileLog.Write(" ");
                            FileLog.Write(Name);
                            FileLog.Write(" ");
                            FileLog.Write(rating);
                            FileLog.Write(" ");
                            FileLog.Write(DateTime.Now);
                            FileLog.Write(" - ");
                            FileLog.Write("Запись добавлена");

                            time1 = DateTime.Now;
                            TimeSpan TimeInterval_2 = time1 - time2;

                            if (TimeInterval_1 < TimeInterval_2)
                            {
                                TimeInterval_1 = TimeInterval_2;
                            }
                            time2 = NewLog.Time;
                        }
                        break;

                    case 3:
                        {
                            Console.WriteLine("Введите номер записи: ");
                            bool deleteError = false;

                            do
                            {
                                int number_delete = Convert.ToInt32(Console.ReadLine());
                                if (number_delete > 0 && number_delete <= table.Count)
                                {
                                    Log NewDelete;
                                    NewDelete.title = table[number_delete - 1].title;
                                    NewDelete.Name = table[number_delete - 1].Name;
                                    NewDelete.rating = table[number_delete - 1].rating;
                                    NewDelete.Time = DateTime.Now;
                                    NewDelete.Operation = "Запись удалена";
                                    Log.Add(NewDelete);
                                    table.RemoveAt(number_delete - 1);

                                    FileLog.Write(NewDelete.title);
                                    FileLog.Write(" ");
                                    FileLog.Write(NewDelete.Name);
                                    FileLog.Write(" ");
                                    FileLog.Write(NewDelete.rating);
                                    FileLog.Write(" ");
                                    FileLog.Write(DateTime.Now);
                                    FileLog.Write(" - ");
                                    FileLog.Write("Запись удалена");

                                    time1 = DateTime.Now;
                                    TimeSpan TimeInterval_2 = time1 - time2;

                                    if (TimeInterval_1 < TimeInterval_2)
                                    {
                                        TimeInterval_1 = TimeInterval_2;
                                    }
                                    time2 = NewDelete.Time;
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный номер )");
                                    deleteError = true;
                                }
                            } while (deleteError == true);
                        }
                        break;

                    case 4:
                        {
                            Console.WriteLine("Введите номер записи: ");
                            bool changeError = false;

                            do
                            {
                                int number_change = Convert.ToInt32(Console.ReadLine());
                                if (number_change > 0 && number_change <= table.Count)
                                {
                                    TV_series NewTV_series;

                                    string OldTitle = table[number_change - 1].title;
                                    Console.WriteLine("Введите новое название телепередачи: ");
                                    string title = Console.ReadLine();
                                    NewTV_series.title = title;

                                    if (title == string.Empty)
                                    {
                                        title = OldTitle;
                                    }
                                    string OldName = table[number_change - 1].Name;
                                    Console.WriteLine("Введите новое название ведущего: ");
                                    string Name = Console.ReadLine();
                                    NewTV_series.Name = Name;

                                    if (Name == string.Empty)
                                    {
                                        Name = OldName;
                                    }

                                    int OldRating = table[number_change - 1].rating;
                                    Console.WriteLine("Введите рейтинг новой телепередачи: ");
                                    int rating = Convert.ToInt32(Console.ReadLine());
                                    NewTV_series.rating = rating;

                                    if (rating <= 5 && rating >= 0)
                                    {
                                        changeError = false;
                                    }

                                    else if (rating > 5 && rating < 0)
                                    {
                                        rating = OldRating;
                                        changeError = false;
                                    }

                                    else
                                    {
                                        Console.WriteLine("Введите правильный рейтинг (1, 2, 3, 4, 5");
                                        changeError = true;
                                    }

                                    var OldType = table[number_change - 1].type;
                                    Console.WriteLine("Введите жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                                    var type = Type.Неизвестен;
                                    bool typeError = false;
                                    NewTV_series.type = type;
                                    do
                                    {

                                        string Type_all = Console.ReadLine();
                                        if (Type_all == "И")
                                        {
                                            type = Type.И;
                                            typeError = false;
                                        }
                                        else if (Type_all == "А" || Type_all == "A")
                                        {
                                            type = Type.А;
                                            typeError = false;
                                        }
                                        else if (Type_all == "T" || Type_all == "Т")
                                        {
                                            type = Type.Т;
                                            typeError = false;
                                        }
                                        else if (Type_all == string.Empty)
                                        {
                                            type = OldType;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Введите правильный жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                                            typeError = true;
                                        }
                                    } while (typeError == true);

                                    table.RemoveAt(number_change - 1);
                                    table.Insert(number_change - 1, NewTV_series);

                                    Log NewLog;
                                    NewLog.title = title;
                                    NewLog.Name = Name;
                                    NewLog.rating = rating;
                                    NewLog.Time = DateTime.Now;
                                    NewLog.Operation = "Запись обновлена";
                                    Log.Add(NewLog);

                                    FileLog.Write(title);
                                    FileLog.Write(" ");
                                    FileLog.Write(Name);
                                    FileLog.Write(" ");
                                    FileLog.Write(rating);
                                    FileLog.Write(" ");
                                    FileLog.Write(DateTime.Now);
                                    FileLog.Write(" - ");
                                    FileLog.Write("Запись обновлена");

                                    time1 = DateTime.Now;
                                    TimeSpan TimeInterval_2 = time1 - time2;

                                    if (TimeInterval_1 < TimeInterval_2)
                                    {
                                        TimeInterval_1 = TimeInterval_2;
                                    }
                                    time2 = NewLog.Time;

                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный номер");
                                }

                            } while (changeError == true);
                        }
                        break;

                    case 5:
                        {
                            Console.WriteLine("Введите жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                            bool searchError = false;

                            do
                            {
                                char number_search = Convert.ToChar(Console.ReadLine());
                                if (number_search == 'И')
                                {
                                    var records = table.FindAll(i => i.type == Type.И);
                                    foreach (var record in records)
                                    {
                                        record.Logging_Info();
                                    }
                                    searchError = false;
                                }
                                else if (number_search == 'А' || number_search == 'A')
                                {
                                    var records = table.FindAll(i => i.type == Type.А);
                                    foreach (var record in records)
                                    {
                                        record.Logging_Info();
                                    }
                                    searchError = false;
                                }
                                else if (number_search == 'T' || number_search == 'Т')
                                {
                                    var records = table.FindAll(i => i.type == Type.Т);
                                    foreach (var record in records)
                                    {
                                        record.Logging_Info();
                                    }
                                    searchError = false;
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильный жанр телепередачи: И - игровая; А - аналитическая; Т – ток-шоу: ");
                                    searchError = true;
                                }
                            } while (searchError == true);
                        }
                        break;

                    case 6:
                        {
                            for (int i = 0; i < Log.Count; i++)
                            {
                                Log[i].Logging();
                            }
                            Console.WriteLine();
                            Console.WriteLine(TimeInterval_1 + " - Самый долгий период бездействия пользователя");
                        }
                        break;

                    case 7:
                        {
                            OptionError = false;
                        }
                        break;

                    default:
                        Console.WriteLine("Введите правильную команду");
                        OptionError = true;
                        break;
                }
            } while (OptionError == true);
            FileLog.Write(TimeInterval_1 + " - Самый долгий период бездействия пользователя");
            FileLog.Close();
        }
    }
}
            

