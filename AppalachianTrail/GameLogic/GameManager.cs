using GameLogic.Entities;
using GameStorage;
using GameStorage.GameValues;
using GameStorage.Model;
using GameUI;

namespace GameLogic
{
    public class GameManager
    {
        private IGameUIAdapter m_GameUIAdapter;

        private IGameDataAdapter m_GameDataAdapter;

        private Trail m_Trail;

        private Hiker m_Hiker;

        private Shopping m_Shopping;

        private System.Random m_RandomEventChance;

        public void Initialize()
        {
            InitializeGameUIAdapter();
            InitializeGameStorageAdapter();
            InitializeRandomEventChance();
        }

        public void StartSetup()
        {
            SetupTrail();

            SetupHiker();

            SetupShopping();
        }

        public void SetupShopping()
        {
            m_Shopping = new Shopping((GameUIAdapter)m_GameUIAdapter, m_Hiker);
            m_Shopping.StartShopping();
        }

        public void StartGameLoop()
        {
            int userLocationResponse = UpdateGameUIToArriveAtLocation(m_Hiker.CurrentLocation);

            while (userLocationResponse != (int)LocationResponse.Continue)
            {
                userLocationResponse = UpdateGameUIToArriveAtLocation(m_Hiker.CurrentLocation);
            }

            Location newLocation = m_Trail.GetNextLocation(m_Hiker.CurrentLocation);

            while (newLocation != null)
            {
                m_Hiker.DistanceToNextLocation = m_Trail.GetDistanceToNextLocation(m_Hiker.CurrentLocation);

                while (m_Hiker.DistanceToNextLocation > 0)
                {
                    DetermineIfRandomEvent(m_RandomEventChance.Next(0, 30));

                    int userTravelResponse = UpdateGameUIToShowTravel();

                    while(userTravelResponse != (int)TravelResponse.Continue)
                    {
                        if (userTravelResponse == (int)TravelResponse.Rest)
                        {
                            ApplyGameLoopRestDeductions();
                        }
                        if (userTravelResponse == (int)TravelResponse.ChangePace)
                        {
                            DeterminePaceChange();
                            userTravelResponse = UpdateGameUIToShowTravel();
                        }
                        if (userTravelResponse == (int)TravelResponse.ChangeRation)
                        {
                            DetermineRationChange();
                            userTravelResponse = UpdateGameUIToShowTravel();
                        }
                    }

                    if (userTravelResponse == (int)TravelResponse.Continue)
                    {
                        ApplyGameLoopContinueDeductions();
                    }

                    if (m_Hiker.CurrentHealthStatus == HealthStatus.Dead)
                    {
                        int endGameUserResponse = m_GameUIAdapter.DisplayGameLoss(DetermineReasonForDeath());
                        if(endGameUserResponse == 1)
                        {
                            m_GameDataAdapter.WriteHighScore(m_Hiker.Name, m_Hiker.GameScore);
                            m_GameUIAdapter.DisplayHighScoreMenu(m_GameDataAdapter.ReadHighScoreDataFile());
                        }
                        if(endGameUserResponse == 2)
                        {
                            System.Environment.Exit(0);
                        }
                    }
                }

                m_Hiker.CurrentLocation = newLocation;

                m_Hiker.DistanceToNextLocation = 0;

                userLocationResponse = UpdateGameUIToArriveAtLocation(newLocation);

                while (userLocationResponse != (int)LocationResponse.Continue)
                {
                    userLocationResponse = UpdateGameUIToArriveAtLocation(newLocation);
                }

                newLocation = m_Trail.GetNextLocation(m_Hiker.CurrentLocation);
            }

            int gameWinUserResponse = m_GameUIAdapter.DisplayGameWin();
            if(gameWinUserResponse == 1)
            {
                m_GameDataAdapter.WriteHighScore(m_Hiker.Name, 0);
                m_GameUIAdapter.DisplayHighScoreMenu(m_GameDataAdapter.ReadHighScoreDataFile());
            }
            if (gameWinUserResponse == 2)
            {
                System.Environment.Exit(0);
            }
        }

        private void InitializeGameUIAdapter()
        {
            m_GameUIAdapter = new GameUIAdapter();
            m_GameUIAdapter.Initialize();
        }

        private void InitializeGameStorageAdapter()
        {
            m_GameDataAdapter = new GameDataAdapter();
        }

        private void InitializeRandomEventChance()
        {
            m_RandomEventChance = new System.Random();
        }

        private void SetupTrail()
        {
            m_Trail = new Trail();
            m_Trail.Map = m_GameDataAdapter.GetMap();
        }

        private void SetupHiker()
        {
            m_Hiker = new Hiker(m_GameUIAdapter.GetName(), m_GameUIAdapter.GetOccupation(), m_GameUIAdapter.GetStartDate(), m_Trail.GetFirstTrailLocation());
        }

        private int UpdateGameUIToShowTravel()
        {
            return m_GameUIAdapter.DisplayTrailSegmentProgression(m_Hiker.CurrentDate, m_Hiker.CurrentLocation.AverageWeatherForEachMonth[m_Hiker.CurrentDate.Month], m_Hiker.CurrentHealthStatus, m_Hiker.Backpack.GetCountOfItems(BackpackItem.OuncesOfFood), m_Hiker.DistanceToNextLocation, m_Hiker.TotalMilesTraveled);
        }

        private int UpdateGameUIToArriveAtLocation(Location newLocation)
        {
            int userResponse = m_GameUIAdapter.DisplayLocationMenu(newLocation.Name);

            if(userResponse == (int)LocationResponse.Shop)
            {
                m_Shopping.StartShopping();
            }
            if(userResponse == (int)LocationResponse.Rest)
            {
                m_Hiker.CurrentDate = m_Hiker.CurrentDate.AddDays(1);
            }
            //if(userResponse == (int)LocationResponse.Talk)
            //{
            //    //talk to towns people
            //}
            return userResponse;
        }

        private void ApplyGameLoopContinueDeductions()
        {
            var distanceCoveredInDay = (int)m_Hiker.CurrentPace * 4;

            m_Hiker.DistanceToNextLocation -= distanceCoveredInDay;

            m_Hiker.TotalMilesTraveled += distanceCoveredInDay;

            m_Hiker.CurrentDate = m_Hiker.CurrentDate.AddDays(1);

            m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.OuncesOfFood, (int)m_Hiker.CurrentFoodRation);

            m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.WaterBottle, (int)m_Hiker.CurrentFoodRation);

            CalculateHikerHealthBasedOnPace();

            CalculateHikerHealthBasedOnFoodRation();

            CalculateHikerHealthBasedOnWeatherAndGear();

            m_Hiker.CurrentHealthStatus = DetermineHealthStatus();

            CalculateHikerGameScore();
        }

        private void ApplyGameLoopRestDeductions()
        {
            m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.OuncesOfFood, (int)m_Hiker.CurrentFoodRation - 1);

            m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.WaterBottle, (int)m_Hiker.CurrentFoodRation - 1);

            m_Hiker.CurrentDate = m_Hiker.CurrentDate.AddDays(1);

            m_Hiker.HealthMeter += 3;

            CalculateHikerHealthBasedOnFoodRation();
        }

        private void DeterminePaceChange()
        {
            Pace pace = m_GameUIAdapter.GetPaceChange(m_Hiker.CurrentPace);
            while (pace == Pace.None)
            {
                pace = m_GameUIAdapter.GetPaceChange(m_Hiker.CurrentPace);
            }
            m_Hiker.CurrentPace = pace;
        }

        private void DetermineRationChange()
        {
            Ration ration = m_GameUIAdapter.GetRationChange(m_Hiker.CurrentFoodRation);
            while (ration == Ration.None)
            {
                ration = m_GameUIAdapter.GetRationChange(m_Hiker.CurrentFoodRation);
            }
            m_Hiker.CurrentFoodRation = ration;
        }

        private void CalculateHikerHealthBasedOnFoodRation()
        {
            switch(m_Hiker.CurrentFoodRation)
            {
                case Ration.Filling:
                    if (m_Hiker.HealthMeter != 100)
                    {
                        m_Hiker.HealthMeter += 1;
                    }
                    break;
                case Ration.Meager:
                    m_Hiker.HealthMeter -= 1;
                    break;
                case Ration.BareBones:
                    m_Hiker.HealthMeter -= 5;
                    break;
            }
        }

        private void CalculateHikerHealthBasedOnPace()
        {
            switch (m_Hiker.CurrentPace)
            {
                case Pace.Strenuous:
                    m_Hiker.HealthMeter -= 3;
                    break;
                case Pace.Steady:
                    m_Hiker.HealthMeter -= 1;
                    break;
                case Pace.Slow:
                    if (m_Hiker.HealthMeter != 100)
                    {
                        m_Hiker.HealthMeter += 1;
                    }
                    break;
            }
        }

        private HealthStatus DetermineHealthStatus()
        {
            if (m_Hiker.Backpack.GetCountOfItems(BackpackItem.OuncesOfFood) <= 0)
            {
                return HealthStatus.Dead;
            }
            if (m_Hiker.Backpack.GetCountOfItems(BackpackItem.WaterBottle) <= 0)
            {
                return HealthStatus.Dead;
            }
            if (m_Hiker.HealthMeter > 80)
            {
                return HealthStatus.Good;
            }
            else if (m_Hiker.HealthMeter > 60)
            {
                return HealthStatus.Fair;
            }
            else if (m_Hiker.HealthMeter > 40)
            {
                return HealthStatus.Weak;
            }
            else if(m_Hiker.HealthMeter > 20)
            {
                return HealthStatus.Sick;
            }
            else
            {
                return HealthStatus.Dead;
            }
        }

        private void CalculateHikerHealthBasedOnWeatherAndGear()
        {
            Weather LocationWeather = m_Hiker.CurrentLocation.AverageWeatherForEachMonth[m_Hiker.CurrentDate.Month];
            bool hasSleepingBag = m_Hiker.Backpack.GetCountOfItems(BackpackItem.SleepingBag) > 0;
            if (LocationWeather == Weather.Cold && !hasSleepingBag)
            {
                m_Hiker.HealthMeter = 0;
            }
            else if(LocationWeather == Weather.Chilly && !hasSleepingBag)
            {
                m_Hiker.HealthMeter -= 20;
            }
        }

        private string DetermineReasonForDeath()
        {
            bool hasSleepingBag = m_Hiker.Backpack.Items.ContainsKey(BackpackItem.SleepingBag);
            bool hasFood = m_Hiker.Backpack.GetCountOfItems(BackpackItem.OuncesOfFood) > 0;
            bool hasWater = m_Hiker.Backpack.GetCountOfItems(BackpackItem.WaterBottle) > 0;
            if (hasFood && !hasSleepingBag)
            {
                return "You froze to death without a sleeping bag near " + m_Hiker.CurrentLocation.Name;
            }
            if (!hasFood)
            {
                return "You starved to death near " + m_Hiker.CurrentLocation.Name;
            }
            if (!hasWater)
            {
                return "You died of dehydration near " + m_Hiker.CurrentLocation.Name;
            }
            return "Unknown death situation";
        }

        private void CalculateHikerGameScore()
        {
            if (m_Hiker.HealthMeter > 80)
            {
                m_Hiker.GameScore += 10;
            }
            else if (m_Hiker.HealthMeter > 60)
            {
                m_Hiker.GameScore += 6;
            }
            else if (m_Hiker.HealthMeter > 40)
            {
                m_Hiker.GameScore += 4;
            }
            else if (m_Hiker.HealthMeter > 20)
            {
                m_Hiker.GameScore += 2;
            }
        }

        private void DetermineIfRandomEvent(int eventType)
        {
            switch(eventType)
            {
                case 4:
                    int userResponse = m_GameUIAdapter.GetResponseOnWhetherToFightBear();
                    if(userResponse == 0)
                    {
                        m_Hiker.CurrentDate = m_Hiker.CurrentDate.AddDays(1);

                        m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.OuncesOfFood, (int)m_Hiker.CurrentFoodRation);

                        m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.WaterBottle, (int)m_Hiker.CurrentFoodRation);

                        CalculateHikerHealthBasedOnPace();

                        CalculateHikerHealthBasedOnFoodRation();

                        CalculateHikerHealthBasedOnWeatherAndGear();

                        m_Hiker.CurrentHealthStatus = DetermineHealthStatus();

                        CalculateHikerGameScore();
                    }
                    if(userResponse == 1)
                    {
                        int bearFightSuccess = m_RandomEventChance.Next(0, 1);
                        if(bearFightSuccess == 1)
                        {
                            m_GameUIAdapter.DisplayBearFightResolution();
                        }
                        else
                        {
                            int endGameUserResponse = m_GameUIAdapter.DisplayGameLoss("You were mauled by a bear. Leonardo Di Caprio would be proud.");
                            if (endGameUserResponse == 1)
                            {
                                m_GameDataAdapter.WriteHighScore(m_Hiker.Name, m_Hiker.GameScore);
                                m_GameUIAdapter.DisplayHighScoreMenu(m_GameDataAdapter.ReadHighScoreDataFile());
                            }
                            if (endGameUserResponse == 2)
                            {
                                System.Environment.Exit(0);
                            }
                        }
                    }
                    break;
                case 8:
                    if (m_Hiker.Backpack.GetCountOfItems(BackpackItem.SleepingBag) > 0)
                    {
                        m_GameUIAdapter.FallOffLedge();
                        m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.SleepingBag, 1);
                        m_Hiker.HealthMeter -= 15;
                        DetermineHealthStatus();
                    }
                    break;
            }
        }
    }
}
