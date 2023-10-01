using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Тренировка пароля";
            Console.CursorVisible = false;



            MainMenu();
        }

        static ConsoleColor colorFalse = ConsoleColor.Red;
        static ConsoleColor colorTrue = ConsoleColor.Green;
        static string str = " ";

        static public void MainMenu()
        {
            int select = 0;
            ConsoleColor colorConsole = ConsoleColor.Green;
            ConsoleColor colorSelect = ConsoleColor.Yellow;
            string[] menuList = { "Изменить пароль" ,"Начать"};
            Console.Clear();
            ConsoleKeyInfo key = Press();
            while (key.Key != ConsoleKey.Escape)
            {
                Console.Clear();

                for (int i = 0; i < menuList.Length; i++)
                {
                    if (i == select)
                    {
                        Console.ForegroundColor = colorConsole;
                        Console.Write("> ");
                        Console.ForegroundColor = colorSelect;
                        Console.WriteLine(menuList[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("  ");
                        Console.WriteLine(menuList[i]);
                    }
                }

                key = Console.ReadKey();

                switch (key.Key)
                {

                    case ConsoleKey.W:
                        if (select <= 0)
                        {
                            select = menuList.Length - 1;
                        }
                        else
                        {
                            select--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (select >= menuList.Length - 1)
                        {
                            select = 0;
                        }
                        else
                        {
                            select++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (select)
                        {
                            case 0:
                                Console.Clear();
                                Console.CursorVisible = true;
                                Console.WriteLine("Введите пароль: ");
                                str = Console.ReadLine();
                                Console.WriteLine("Успешно!");
                                Console.CursorVisible = false;
                                break;
                            case 1:
                                TestPass();
                                break;
                            default:
                                Press();
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        static ConsoleKeyInfo Press(bool x = true)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if(x)
            {
                Console.WriteLine("Нажмите любую кнопку");
            }
            Console.ResetColor();
            ConsoleKeyInfo key = Console.ReadKey();
            return key;
        }
        static void TestPass()
        {
            List<double> arr = new List<double>();
            List<double> trueVariable = new List<double>();
            Stopwatch sw = new Stopwatch();
            double nrt = 0;
            string text = "";
            ConsoleKeyInfo key =  Press();

            double sr = 0;
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Чтобы начать вводить пароль (длина: {str.Length}) \nПодсказка: {str}\nНачинайте вводить:");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("ESC для выхода в меню");
                Console.ResetColor();
                char first = Press(false).KeyChar;

                Console.Clear();
                sw.Start();

                while (text.Length < str.Length)
                {
                    if(key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    key = Console.ReadKey();
                    if(text.Length == 0)
                    {
                        text += first;
                    }
                    text += key.KeyChar;
                }
                Console.WriteLine();
                sw.Stop();
                nrt = Convert.ToDouble(sw.ElapsedMilliseconds)/1000;
                sw.Reset();
                Console.Clear();
                Console.WriteLine($"{str}");
                Color(str,text);
                string result = String.Format("{0,0}", nrt);
                if (str == text || text == str)
                {
                    arr.Add(nrt);
                }

                Console.Write("Время ");
                Console.ForegroundColor = ConsoleColor.Cyan;

                if (trueVariable.Count != 0)
                {
                    trueVariable.Average();
                }


                if (nrt == sr)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(result);
                    Console.ResetColor();
                } else if(nrt >= sr)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(result);
                    Console.ResetColor();
                } else if(nrt <= sr)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(result);
                    Console.ResetColor();
                }
                Console.ResetColor();
                Console.Write(" сек.");

                Console.Write(" | ");

                Console.Write("Лучший: ");
                Console.ForegroundColor = ConsoleColor.Green;

                if (arr.Count != 0)
                {
                    Console.Write(arr.Min());
                }
                Console.ResetColor();
                Console.Write(" сек.");

                Console.Write(" Средний: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (arr.Count != 0)
                {
                    Console.Write(arr.Max() - arr.Min());
                }
                Console.ResetColor();
                Console.Write(" сек.");

                Console.Write(" Худший: ");
                Console.ForegroundColor = ConsoleColor.Red;
                if (arr.Count != 0)
                {
                    Console.Write(arr.Max());
                }
                Console.ResetColor();
                Console.WriteLine(" сек.");

                text = "";
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadLine();
                Console.ResetColor();
            }
        }
        static void Color(string n1, string n2)
        {
            //debug

            if (n2.Length > n1.Length)
            {
                n2.Remove(n1.Length);
            } else if (n2.Length < n1.Length)
            {
                int index = n1.Length - n2.Length;
                for (int i = 0; n1.Length - n2.Length != 0; i++)
                {
                    n2 += '*';
                }
            }

            if(str == "")
            {
                Console.WriteLine("Пароль не введён!");
                return;
            }

            double num = 0;
            double correct = 100 / n1.Length;

            for (int i = 0; i < n1.Length; i++)
            {
                if (n1[i] == n2[i])
                {
                    if (n1[i] == ' ')
                    {
                        Console.BackgroundColor = colorTrue;
                        Console.Write(n2[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = colorTrue;
                        Console.Write(n2[i]);
                        Console.ResetColor();
                    }
                    num += correct;
                }
                else
                {
                    if (n2[i] == ' ')
                    {
                        Console.BackgroundColor = colorFalse;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('*');
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = colorFalse;
                        Console.Write(n2[i]);
                        Console.ResetColor();
                    }
                }
            }
            if (n1 == n2)
            {
                num = 100;
            }

            Console.WriteLine();
            Console.WriteLine($"Верно на {num}%");


        }
    }
}
