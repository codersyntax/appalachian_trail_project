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
            int numValueOfStartDate = 0;
            Int32.TryParse(startDate, out numValueOfStartDate);
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
            int userAnswer;
            bool isValidAnswer = DisplayLocationOptions(out userAnswer);
            while (!isValidAnswer)
            {
                isValidAnswer = DisplayLocationOptions(out userAnswer);
            }
            Console.ReadLine();
        }

        private bool DisplayLocationOptions(out int numValueOfUserAnswer)
        {
            DisplayToUser("Enter 1 to purchase some supplies");
            DisplayToUser("Enter 2 to rest");
            DisplayToUser("Enter 3 to speak with the townsfolk");
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
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

        public bool StartShopping (int wallet)
        {
            ClearUserView();
            DisplayToUser("Welcome to the supply store. What would you like to purchase?");
            DisplayToUser("We have the following items in stock.");
            DisplayToUser(GameUIConstants.ShopItems);
            DisplayToUser("You currently have " + wallet + " to spend.");
            DisplayToUser("Would you like to purchase anything? (Y/N)");
            var purchaseResponse = Console.ReadLine().ToLower();
            if(purchaseResponse == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BackpackItem PurchaseShoppingItem(out int itemPurchaseCount)
        {
            DisplayToUser("Select an item to purchase");
            string purchasedItemString = Console.ReadLine();
            BackpackItem purchasedItem;
            switch(purchasedItemString)
            {
                case "1":
                    purchasedItem = BackpackItem.WaterBottle;
                    break;
                case "2":
                    purchasedItem = BackpackItem.SetsOfClothing;
                    break;
                case "3":
                    purchasedItem = BackpackItem.Tent;
                    break;
                case "4":
                    purchasedItem = BackpackItem.OuncesOfFood;
                    break;
                case "5":
                    purchasedItem = BackpackItem.SleepingBag;
                    break;
                default:
                    purchasedItem = BackpackItem.None;
                    break;
            }
            DisplayToUser("How many would you like to purchase?");
            string purchaseAmountString = Console.ReadLine();
            Int32.TryParse(purchaseAmountString, out itemPurchaseCount);
            return purchasedItem;
        }
    }
}
