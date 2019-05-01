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
            DisplayToUser("When would you like to begin your trek on the AT? (Please specify full month or # of month ex. March or 3) ");
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
                DisplayToUser("When would you like to begin your trek on the AT? (Please specify full month or # of month ex. March or 3) ");
                startDate = Console.ReadLine();
                isValidStartDate = Enum.IsDefined(typeof(Month), startDate);
                if (isValidStartDate)
                {
                    return (Month)Enum.Parse(typeof(Month), startDate);
                }
            }
            return Month.Unknown;
        }

        public void DisplayTrailSegmentProgression(DateTime currentDate, Weather currentWeather, HealthStatus currentHealthStatus, int amountOfFood, int distanceToNextLocation, int totalDistanceTraveled)
        {
            ClearUserView();
            DisplayToUser("Date: " + currentDate.ToLongDateString());
            DisplayToUser("Weather: " + currentWeather.ToString());
            DisplayToUser("Health: " + currentHealthStatus.ToString());
            DisplayToUser("Food: " + amountOfFood.ToString());
            DisplayToUser("Next landmark: " + distanceToNextLocation.ToString());
            DisplayToUser("Miles traveled: " + totalDistanceTraveled.ToString());
            DisplayToUser("Continue?");
            Console.ReadLine();
        }

        public void DisplayLocationMenu(string newLocationName)
        {
            ClearUserView();
            DisplayToUser("You have arrived at: " + newLocationName);
            DisplayToUser("Would you like to purchase some supplies?");
            Console.ReadLine();
        }

        public void DisplayGameWin()
        {
            DisplayToUser("Congrats traveller! You made it to Georgia! Would you like to record your high score? ");
            Console.ReadLine();
        }

        private void DisplayToUser(string message)
        {
            Console.WriteLine(message);
        }

        private void ClearUserView()
        {
            Console.Clear();
        }

        private void Delay(int time = 2000)
        {
            Thread.Sleep(time);
        }

        public void Shopping (int wallet)
        {
            DisplayToUser("Welcome to the supply store. What would you like to purchase?");
            DisplayToUser("We have the following items in stock.");
            DisplayToUser(GameUIConstants.ShopItems);
            DisplayToUser("You currently have" + wallet + "to spend.");
            DisplayToUser("Select an item to purchase");
            DisplayToUser("How many would you like to purchase?");
            DisplayToUser("Would you like to purchase anything else?");
            
        }
    }
}
