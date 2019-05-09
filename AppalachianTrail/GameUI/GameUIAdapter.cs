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
            DisplayToUser(GameUIConstants.HelloTraveler);
            Delay();
            DisplayToUser(GameUIConstants.Welcome);
            Delay();
            DisplayToUser(GameUIConstants.Title);
            Delay();
            DisplayToUser(GameUIConstants.Intro);
            Delay();
            DisplayToUser(GameUIConstants.Start);
            Delay();
        }

        public string GetName()
        {
            AskUserChoice(GameUIConstants.PlayerName);
            return Console.ReadLine();
        }

        public Occupation GetOccupation()
        {
            AskUserChoice(GameUIConstants.Occupation);
            string occupation = Console.ReadLine();
            bool isValidOccupation = Enum.IsDefined(typeof(Occupation), occupation);
            if(isValidOccupation)
            {
                return (Occupation)Enum.Parse(typeof(Occupation), occupation);
            }
            while(!isValidOccupation)
            {
                DisplayToUser(occupation + GameUIConstants.NotVaild);
                DisplayToUser(GameUIConstants.Occupation);
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
            AskUserChoice(GameUIConstants.UserStartPoint);
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
                DisplayToUser(startDate + GameUIConstants.NotVaildStartDate);
                AskUserChoice(GameUIConstants.UserStartPoint);
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
            DisplayToUser(GameUIConstants.Date + currentDate.ToLongDateString());
            DisplayToUser(GameUIConstants.Weather + currentWeather.ToString());
            DisplayToUser(GameUIConstants.Health + currentHealthStatus.ToString());
            DisplayToUser(GameUIConstants.Food + amountOfFood.ToString());
            DisplayToUser(GameUIConstants.Landmark + distanceToNextLocation.ToString());
            DisplayToUser(GameUIConstants.Miles + totalDistanceTraveled.ToString());
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
            DisplayToUser(GameUIConstants.ContinueTrail);
            DisplayToUser(GameUIConstants.Rest);
            DisplayToUser(GameUIConstants.ChangePace);
            DisplayToUser(GameUIConstants.ChangeFoodRatio);
            AskUserChoice(GameUIConstants.Choice);
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
        }

        public int DisplayLocationMenu(string newLocationName)
        {
            ClearUserView();
            DisplayToUser(GameUIConstants.Welcome + newLocationName + "!\n");
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
            DisplayToUser(GameUIConstants.PurchaseSupplies);
            DisplayToUser(GameUIConstants.Rest);
            DisplayToUser(GameUIConstants.SpeakTownsfolk);
            DisplayToUser(GameUIConstants.ContinueTrailhead);
            AskUserChoice(GameUIConstants.Choice);
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
        }

        public int DisplayGameWin()
        {
            DisplayToUser(GameUIConstants.EndGame);
            int userAnswer;
            bool isValidAnswer = DisplayHighScoreOptions(out userAnswer);
            while (!isValidAnswer)
            {
                isValidAnswer = DisplayHighScoreOptions(out userAnswer);
            }
            return userAnswer;
        }

        public int DisplayGameLoss(string reasonOfDeath)
        {
            DisplayToUser(reasonOfDeath + GameUIConstants.RecordHighScore);
            int userAnswer;
            bool isValidAnswer = DisplayHighScoreOptions(out userAnswer);
            while (!isValidAnswer)
            {
                isValidAnswer = DisplayHighScoreOptions(out userAnswer);
            }
            return userAnswer;
        }

        private bool DisplayHighScoreOptions(out int numValueOfUserAnswer)
        {
            DisplayToUser(GameUIConstants.EnterHighScore);
            DisplayToUser(GameUIConstants.Quit);
            AskUserChoice(GameUIConstants.Choice);
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
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
            DisplayToUser(GameUIConstants.WelcomeSupplyStore);
            DisplayToUser(GameUIConstants.SupplyStoreItems);
            DisplayToUser(GameUIConstants.ShopItems);
            DisplayToUser(GameUIConstants.Currently + wallet + GameUIConstants.Spend);
            DisplayCurrentShoppingCart(shoppingCart);
            DisplayHikerBackpack(hikerBackpack);
            AskUserChoice(GameUIConstants.PurchaseQuestion);
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
            DisplayToUser(GameUIConstants.CurrentCart);
            foreach (var item in shoppingCart)
            {
                DisplayToUser("\t" + item.Key.ToString() + " : " + item.Value.ToString());
            }
            DisplayToUser("\n");
        }

        private void DisplayHikerBackpack(Dictionary<BackpackItem, int> hikerBackpack)
        {
            DisplayToUser(GameUIConstants.CurrentBackpack);
            foreach (var item in hikerBackpack)
            {
                DisplayToUser("\t" + item.Key.ToString() + " : " + item.Value.ToString());
            }
            DisplayToUser("\n");
        }

        public BackpackItem PurchaseShoppingItem(out int itemPurchaseCount)
        {
            AskUserChoice(GameUIConstants.SelectItem);
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
            AskUserChoice(GameUIConstants.PurchaseSize);
            string purchaseAmountString = Console.ReadLine();
            Int32.TryParse(purchaseAmountString, out itemPurchaseCount);
            return purchasedItem;
        }

        public void DisplayInsufficientFunds()
        {
            DisplayToUser(GameUIConstants.InsufficientFunds);
            Console.ReadLine();
        }

        public Pace GetPaceChange(Pace currentPace)
        {
            DisplayToUser(GameUIConstants.PaceQuestion);
            DisplayToUser(GameUIConstants.PaceOptions);
            DisplayToUser(GameUIConstants.CurrentPace + currentPace.ToString());
            AskUserChoice(GameUIConstants.Choice);
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
            DisplayToUser(GameUIConstants.FoodRationQuestion);
            DisplayToUser(GameUIConstants.FoodRationOption);
            DisplayToUser(GameUIConstants.CurrentFoodRation + currentRation);
            AskUserChoice(GameUIConstants.Choice);
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
