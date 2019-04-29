using GameStorage.GameValues;
using System;
using System.Threading;

namespace GameUI
{
    public class GameUIAdapter
    {
        public void Initialize()
        {
            SetGameTitle();
            Intro();
        }

        private void SetGameTitle()
        {
            Console.Title = GameUIConstants.ConsoleTitle;
        }

        private void Intro()
        {
            DisplayToUser("Hello traveler");
            Delay();
            DisplayToUser("And welcome to...");
            Delay();
            DisplayToUser(GameUIConstants.Title);
            Delay();
            DisplayToUser("You're about to embark on a 2,190 mile journey stretching from Maine to Georgia");
            Delay();
            DisplayToUser("You've got a long road ahead so let's get started");
            Delay();
        }

        public string GetName()
        {
            DisplayToUser("What's your name? ");
            return Console.ReadLine();
        }

        public Occupation GetOccupation()
        {
            DisplayToUser("What's your occupation? [Doctor, Carpenter, Student, Hippie] ");
            string occupation = Console.ReadLine();
            bool isValidOccupation = Enum.IsDefined(typeof(Occupation), occupation);
            if(isValidOccupation)
            {
                return (Occupation)Enum.Parse(typeof(Occupation), occupation);
            }
            while(!isValidOccupation)
            {
                DisplayToUser(occupation + " is not a valid response, please try again...");
                DisplayToUser("What's your occupation? [Doctor, Carpenter, Student, Hippie] ");
                occupation = Console.ReadLine();
                isValidOccupation = Enum.IsDefined(typeof(Occupation), occupation);
                if (isValidOccupation)
                {
                    return (Occupation)Enum.Parse(typeof(Occupation), occupation);
                }
            }
            return Occupation.Unknown;
        }

        public Month GetStartDate()
        {
            DisplayToUser("When would you like to begin your trek on the AT? [1-12] ");
            string startDate = Console.ReadLine();
            bool isValidStartDate = Enum.IsDefined(typeof(Month), startDate);
            Int32.TryParse(startDate, out int numValueOfStartDate);
            if (isValidStartDate || numValueOfStartDate != 0)
            {
                return (Month)Enum.Parse(typeof(Month), startDate);
            }
            while (!isValidStartDate)
            {
                DisplayToUser(startDate + " is not a valid response, please try again...");
                DisplayToUser("When would you like to begin your trek on the AT? [1-12] ");
                startDate = Console.ReadLine();
                isValidStartDate = Enum.IsDefined(typeof(Month), startDate);
                if (isValidStartDate)
                {
                    return (Month)Enum.Parse(typeof(Month), startDate);
                }
            }
            return Month.Unknown;
        }

        private void DisplayToUser(string message)
        {
            Console.WriteLine(message);
        }

        private void Delay(int time = 2000)
        {
            Thread.Sleep(time);
        }
    }
}
