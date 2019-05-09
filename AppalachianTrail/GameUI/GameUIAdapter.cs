using GameStorage.GameValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameUI
{
    public class GameUIAdapter : IGameUIAdapter
    {
        private GameUIConstants m_GameUIConstants;
        public void Initialize()
        {
            m_GameUIConstants = new EnglishGameUIConstants();
            SetGameTitle();
            Intro();
        }

        private void SetGameTitle()
        {
            Console.Title = m_GameUIConstants.ConsoleTitle;
        }

        private void Intro()
        {
            DisplayToUser(m_GameUIConstants.HelloTraveler);
            Delay();
            DisplayToUser(m_GameUIConstants.Welcome);
            Delay();
            DisplayToUser(m_GameUIConstants.Title);
            Delay();
            DisplayToUser(m_GameUIConstants.Intro);
            Delay();
            DisplayToUser(m_GameUIConstants.Start);
            Delay();
        }

        public string GetName()
        {
            AskUserChoice(m_GameUIConstants.PlayerName);
            return Console.ReadLine();
        }

        public Occupation GetOccupation()
        {
            AskUserChoice(m_GameUIConstants.Occupation);
            string occupation = Console.ReadLine();
            bool isValidOccupation = Enum.IsDefined(typeof(Occupation), occupation);
            if(isValidOccupation)
            {
                return (Occupation)Enum.Parse(typeof(Occupation), occupation);
            }
            while(!isValidOccupation)
            {
                DisplayToUser(occupation + m_GameUIConstants.NotVaild);
                DisplayToUser(m_GameUIConstants.Occupation);
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
            AskUserChoice(m_GameUIConstants.UserStartPoint);
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
                DisplayToUser(startDate + m_GameUIConstants.NotVaildStartDate);
                AskUserChoice(m_GameUIConstants.UserStartPoint);
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
            DisplayToUser(m_GameUIConstants.Date + currentDate.ToLongDateString());
            DisplayToUser(m_GameUIConstants.Weather + currentWeather.ToString());
            DisplayToUser(m_GameUIConstants.Health + currentHealthStatus.ToString());
            DisplayToUser(m_GameUIConstants.Food + amountOfFood.ToString());
            DisplayToUser(m_GameUIConstants.Landmark + distanceToNextLocation.ToString());
            DisplayToUser(m_GameUIConstants.Miles + totalDistanceTraveled.ToString());
            int userAnswer;
            bool isValidAnswer = DisplayTravelOptions(out userAnswer);
            while (!isValidAnswer)
            {
                isValidAnswer = DisplayTravelOptions(out userAnswer);
            }
            return userAnswer;
        }

        public int GetResponseOnWhetherToFightBear()
        {
            ClearUserView();
            DisplayToUser(m_GameUIConstants.RunIntoBear);
            int userAnswer;
            bool isValidAnswer = DisplayApproachingBearNotification(out userAnswer);
            while (!isValidAnswer)
            {
                isValidAnswer = DisplayApproachingBearNotification(out userAnswer);
            }
            return userAnswer;
        }

        private bool DisplayApproachingBearNotification(out int numValueOfUserAnswer)
        {
            AskUserChoice(m_GameUIConstants.Choice);
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
        }

        public void DisplayBearFightResolution()
        {
            DisplayToUser(m_GameUIConstants.FoughtOffBear);
            Console.ReadLine();
        }


        private bool DisplayTravelOptions(out int numValueOfUserAnswer)
        {
            DisplayToUser(m_GameUIConstants.ContinueTrail);
            DisplayToUser(m_GameUIConstants.Rest);
            DisplayToUser(m_GameUIConstants.ChangePace);
            DisplayToUser(m_GameUIConstants.ChangeFoodRatio);
            AskUserChoice(m_GameUIConstants.Choice);
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
        }

        public int DisplayLocationMenu(string newLocationName)
        {
            ClearUserView();
            DisplayToUser(m_GameUIConstants.Welcome + newLocationName + "!\n");
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
            DisplayToUser(m_GameUIConstants.PurchaseSupplies);
            DisplayToUser(m_GameUIConstants.Rest);
            //DisplayToUser(m_GameUIConstants.SpeakTownsfolk);
            DisplayToUser(m_GameUIConstants.ContinueTrailhead);
            AskUserChoice(m_GameUIConstants.Choice);
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
        }

        public int DisplayGameWin()
        {
            DisplayToUser(m_GameUIConstants.EndGame);
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
            DisplayToUser(reasonOfDeath + m_GameUIConstants.RecordHighScore);
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
            DisplayToUser(m_GameUIConstants.EnterHighScore);
            DisplayToUser(m_GameUIConstants.Quit);
            AskUserChoice(m_GameUIConstants.Choice);
            string userAnswer = Console.ReadLine();
            bool isValidAnswer = Int32.TryParse(userAnswer, out numValueOfUserAnswer);
            return isValidAnswer;
        }

        public void DisplayHighScoreMenu(string[] highScores)
        {
            List<KeyValuePair<string, int>> HighScores = new List<KeyValuePair<string, int>>();
            foreach (string score in highScores)
            {
                string[] scoreArray = score.Split('/');
                HighScores.Add(new KeyValuePair<string, int>(scoreArray[0], Int32.Parse(scoreArray[1])));
            }
            var sortedHighScores = HighScores.OrderBy(x => x.Value);
            ClearUserView();
            DisplayToUser(m_GameUIConstants.HighScoresTitle);
            foreach (var score in sortedHighScores)
            {
                DisplayToUser("\n\t\tHiker: " + score.Key + "\n\t\tScore: " + score.Value + "\n");
            }
            Console.ReadLine();
            Environment.Exit(0);
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
            DisplayToUser(m_GameUIConstants.WelcomeSupplyStore);
            DisplayToUser(m_GameUIConstants.SupplyStoreItems);
            DisplayToUser(m_GameUIConstants.ShopItems);
            DisplayToUser(m_GameUIConstants.Currently + wallet + m_GameUIConstants.Spend);
            DisplayCurrentShoppingCart(shoppingCart);
            DisplayHikerBackpack(hikerBackpack);
            AskUserChoice(m_GameUIConstants.PurchaseQuestion);
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
            DisplayToUser(m_GameUIConstants.CurrentCart);
            foreach (var item in shoppingCart)
            {
                DisplayToUser("\t" + item.Key.ToString() + " : " + item.Value.ToString());
            }
            DisplayToUser("\n");
        }

        private void DisplayHikerBackpack(Dictionary<BackpackItem, int> hikerBackpack)
        {
            DisplayToUser(m_GameUIConstants.CurrentBackpack);
            foreach (var item in hikerBackpack)
            {
                DisplayToUser("\t" + item.Key.ToString() + " : " + item.Value.ToString());
            }
            DisplayToUser("\n");
        }

        public BackpackItem PurchaseShoppingItem(out int itemPurchaseCount)
        {
            AskUserChoice(m_GameUIConstants.SelectItem);
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
            AskUserChoice(m_GameUIConstants.PurchaseSize);
            string purchaseAmountString = Console.ReadLine();
            Int32.TryParse(purchaseAmountString, out itemPurchaseCount);
            return purchasedItem;
        }

        public void DisplayInsufficientFunds()
        {
            DisplayToUser(m_GameUIConstants.InsufficientFunds);
            Console.ReadLine();
        }

        public Pace GetPaceChange(Pace currentPace)
        {
            DisplayToUser(m_GameUIConstants.PaceQuestion);
            DisplayToUser(m_GameUIConstants.PaceOptions);
            DisplayToUser(m_GameUIConstants.CurrentPace + currentPace.ToString());
            AskUserChoice(m_GameUIConstants.Choice);
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
            DisplayToUser(m_GameUIConstants.FoodRationQuestion);
            DisplayToUser(m_GameUIConstants.FoodRationOption);
            DisplayToUser(m_GameUIConstants.CurrentFoodRation + currentRation);
            AskUserChoice(m_GameUIConstants.Choice);
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

        public void FallOffLedge()
        {
            DisplayToUser(m_GameUIConstants.FellOffLedge);
            Console.ReadLine();
        }
    }
}
