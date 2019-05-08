using GameStorage.GameValues;
using System;
using System.Collections.Generic;
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
            DisplayToUser("\nHello traveler");
            Delay();
            DisplayToUser("And welcome to...");
            Delay();
            DisplayToUser(GameUIConstants.Title);
            Delay();
            DisplayToUser("You're about to embark on a 2,190 mile journey stretching from Georgia to Maine");
            Delay();
            DisplayToUser("You've got a long road ahead so let's get started");
            Delay();
        }

        public string GetName()
        {
            AskUserChoice("What's your name? ");
            return Console.ReadLine();
        }

        public Occupation GetOccupation()
        {
            AskUserChoice("What's your occupation? [Doctor, Carpenter, Student, Hippie]: ");
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
            AskUserChoice("When would you like to begin your trek on the AT? \n\t\t(Please specify full month or # of month ex. March or 3): ");
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
                AskUserChoice("When would you like to begin your trek on the AT? (Please specify full month or # of month ex. March or 3) ");
                startDate = Console.ReadLine();
                isValidStartDate = Enum.IsDefined(typeof(Month), startDate);
                if (isValidStartDate)
                {
                    return (Month)Enum.Parse(typeof(Month), startDate);
                }
            }
            return Month.Unknown;
        }

        public int DisplayTrailSegmentProgression(DateTime currentDate, Weather currentWeather, HealthStatus currentHealthStatus, int amountOfFood, int distanceToNextLocation, int totalDistanceTraveled)
        {
            ClearUserView();
            DisplayToUser("\n\tDate: " + currentDate.ToLongDateString());
            DisplayToUser("\tWeather: " + currentWeather.ToString());
            DisplayToUser("\tHealth: " + currentHealthStatus.ToString());
            DisplayToUser("\tFood: " + amountOfFood.ToString());
            DisplayToUser("\tNext landmark: " + distanceToNextLocation.ToString());
            DisplayToUser("\tMiles traveled: " + totalDistanceTraveled.ToString());
            int userAnswer;
            bool isValidAnswer = DisplayTravelOptions(out userAnswer);
            while (!isValidAnswer)
            {
                isValidAnswer = DisplayTravelOptions(out userAnswer);
            }
            return userAnswer;
        }


        private bool DisplayTravelOptions(out int numValueOfUserAnswer)
        {
            DisplayToUser("Enter 1 to continue on the trail");
            DisplayToUser("Enter 2 to rest");
            DisplayToUser("Enter 3 to change pace");
            DisplayToUser("Enter 4 to change food rations");
            AskUserChoice("Your choice: ");
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
        }

        public int DisplayLocationMenu(string newLocationName)
        {
            ClearUserView();
            DisplayToUser("\n\tWelcome to " + newLocationName + "!\n");
            int userAnswer;
            bool isValidAnswer = DisplayLocationOptions(out userAnswer);
            while (!isValidAnswer)
            {
                isValidAnswer = DisplayLocationOptions(out userAnswer);
            }
            return userAnswer;
        }

        private bool DisplayLocationOptions(out int numValueOfUserAnswer)
        {
            DisplayToUser("Enter 1 to purchase some supplies");
            DisplayToUser("Enter 2 to rest");
            DisplayToUser("Enter 3 to speak with the townsfolk");
            DisplayToUser("Enter 4 to continue to the next trailhead");
            AskUserChoice("Your choice: ");
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
        }

        public void DisplayGameWin()
        {
            DisplayToUser("Congrats traveller! You made it to Rangeley, ME! Would you like to record your high score? ");
            Console.ReadLine();
        }

        private void DisplayToUser(string message)
        {
            Console.WriteLine("\t" + message);
        }

        private void AskUserChoice(string message)
        {
            Console.Write("\n\t\t" + message);
        }

        private void ClearUserView()
        {
            Console.Clear();
        }

        private void Delay(int time = 2000)
        {
            Thread.Sleep(time);
        }

        public bool StartShopping (int wallet, Dictionary<BackpackItem, int> hikerBackpack, Dictionary<BackpackItem, int> shoppingCart)
        {
            ClearUserView();
            DisplayToUser("Welcome to the supply store. What would you like to purchase?");
            DisplayToUser("We have the following items in stock.");
            DisplayToUser(GameUIConstants.ShopItems);
            DisplayToUser("You currently have " + wallet + " to spend.");
            DisplayCurrentShoppingCart(shoppingCart);
            DisplayHikerBackpack(hikerBackpack);
            AskUserChoice("Would you like to purchase anything? (Y/N): ");
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

        private void DisplayCurrentShoppingCart(Dictionary<BackpackItem, int> shoppingCart)
        {
            DisplayToUser("\n\tYour current shopping cart items");
            foreach (var item in shoppingCart)
            {
                DisplayToUser("\t" + item.Key.ToString() + " : " + item.Value.ToString());
            }
            DisplayToUser("\n");
        }

        private void DisplayHikerBackpack(Dictionary<BackpackItem, int> hikerBackpack)
        {
            DisplayToUser("\n\tYour current back pack items");
            foreach (var item in hikerBackpack)
            {
                DisplayToUser("\t" + item.Key.ToString() + " : " + item.Value.ToString());
            }
            DisplayToUser("\n");
        }

        public BackpackItem PurchaseShoppingItem(out int itemPurchaseCount)
        {
            AskUserChoice("Select an item to purchase [1-5]: ");
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
            AskUserChoice("How many would you like to purchase?: ");
            string purchaseAmountString = Console.ReadLine();
            Int32.TryParse(purchaseAmountString, out itemPurchaseCount);
            return purchasedItem;
        }

        public void DisplayInsufficientFunds()
        {
            DisplayToUser("You don't have enough money for that");
            Console.ReadLine();
        }

        public Pace GetPaceChange(Pace currentPace)
        {
            DisplayToUser("What pace would you like to hike at?");
            DisplayToUser("[1] Strenuous / [2] Steady / [3] Slow");
            DisplayToUser("Your current pace: " + currentPace.ToString());
            AskUserChoice("Your choice: ");
            string userResponse = Console.ReadLine();
            Pace pace;
            switch (userResponse)
            {
                case "1":
                    pace = Pace.Strenuous;
                    break;
                case "2":
                    pace = Pace.Steady;
                    break;
                case "3":
                    pace = Pace.Slow;
                    break;
                default:
                    pace = Pace.None;
                    break;
            }
            return pace;
        }

        public Ration GetRationChange(Ration currentRation)
        {
            DisplayToUser("How much food rations would you like to consume?");
            DisplayToUser("[1] Filling / [2] Meager / [3] Bare bones");
            DisplayToUser("Your current food rations: " + currentRation);
            AskUserChoice("Your choice: ");
            string userResponse = Console.ReadLine();
            Ration ration;
            switch (userResponse)
            {
                case "1":
                    ration = Ration.Filling;
                    break;
                case "2":
                    ration = Ration.Meager;
                    break;
                case "3":
                    ration = Ration.BareBones;
                    break;
                default:
                    ration = Ration.None;
                    break;
            }
            return ration;
        }
    }
}
