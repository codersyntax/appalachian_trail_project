using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Intro();
            GetName();
        }

        public static void Intro()
        {
            Console.WriteLine("Hello traveler");
            Thread.Sleep(2000);
            Console.WriteLine("And welcome to...");
            Thread.Sleep(2000);
            Console.Title = "ASCII Art";
            string title = @"
            
  _______ _                                       _            _     _               _______        _ _ 
 |__   __| |              /\                     | |          | |   (_)             |__   __|      (_) |
    | |  | |__   ___     /  \   _ __  _ __   __ _| | __ _  ___| |__  _  __ _ _ __      | |_ __ __ _ _| |
    | |  | '_ \ / _ \   / /\ \ | '_ \| '_ \ / _` | |/ _` |/ __| '_ \| |/ _` | '_ \     | | '__/ _` | | |
    | |  | | | |  __/  / ____ \| |_) | |_) | (_| | | (_| | (__| | | | | (_| | | | |    | | | | (_| | | |
    |_|  |_| |_|\___| /_/    \_\ .__/| .__/ \__,_|_|\__,_|\___|_| |_|_|\__,_|_| |_|    |_|_|  \__,_|_|_|
                               | |   | |                                                                
                               |_|   |_|                                                                
";
            Console.WriteLine(title);
            Thread.Sleep(2000);
        }

        public static void GetName()
        {
            Console.WriteLine("You're about to embark on a 2,190 mile journey stretching from Maine to Georgia");
            Thread.Sleep(2000);
            Console.WriteLine("You've got a long road ahead so let's get started");
            Thread.Sleep(2000);
            Console.WriteLine("What's your name?");
            Console.Read();
        }
    }
}