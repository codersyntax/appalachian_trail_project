using GameLogic.Entities;
using GameStorage;
using GameStorage.GameValues;
using GameStorage.Model;
using GameUI;

namespace GameLogic
{
    public class GameManager
    {
        private GameUIAdapter m_GameUIAdapter;

        private GameDataAdapter m_GameDataAdapter;

        private Trail m_Trail;

        private Hiker m_Hiker;

        private SetupShopping m_SetupShopping;

        public void Initialize()
        {
            InitializeGameUIAdapter();
            InitializeGameStorageAdapter();
        }

        public void StartSetup()
        {
            SetupTrail();

            SetupHiker();

            SetupShopping();
        }

        public void SetupShopping()
        {
            m_SetupShopping = new SetupShopping(m_GameUIAdapter, m_Hiker);
            m_SetupShopping.StartShopping();
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
                        if(endGameUserResponse == 2)
                        {
                            System.Environment.Exit(0);
                        }
                        // TODO: handle high score or game exiting depending upon response
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
            // TODO:if userResponse is 1, save game and display high scores
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
                m_SetupShopping.StartShopping();
            }
            if(userResponse == (int)LocationResponse.Rest)
            {
                m_Hiker.CurrentDate = m_Hiker.CurrentDate.AddDays(1);
            }
            if(userResponse == (int)LocationResponse.Talk)
            {
                //talk to towns people
            }
            return userResponse;
        }

        private void ApplyGameLoopContinueDeductions()
        {
            var distanceCoveredInDay = (int)m_Hiker.CurrentPace * 4;

            m_Hiker.DistanceToNextLocation -= distanceCoveredInDay;

            m_Hiker.TotalMilesTraveled += distanceCoveredInDay;

            m_Hiker.CurrentDate = m_Hiker.CurrentDate.AddDays(1);

            m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.OuncesOfFood, (int)m_Hiker.CurrentFoodRation);

            CalculateHikerHealthBasedOnPace();

            CalculateHikerHealthBasedOnFoodRation();

            CalculateHikerHealthBasedOnWeatherAndGear();

            m_Hiker.CurrentHealthStatus = DetermineHealthStatus();
        }

        private void ApplyGameLoopRestDeductions()
        {
            m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.OuncesOfFood, (int)m_Hiker.CurrentFoodRation);

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
            bool hasSleepingBag = m_Hiker.Backpack.Items.ContainsKey(BackpackItem.SleepingBag);
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
            bool hasFood = m_Hiker.Backpack.Items.ContainsKey(BackpackItem.OuncesOfFood);

            if(hasFood && !hasSleepingBag)
            {
                return "You froze to death without a sleeping bag near " + m_Hiker.CurrentLocation.Name;
            }
            if(!hasFood)
            {
                return "You starved to death near " + m_Hiker.CurrentLocation.Name;
            }
            return "Unknown death situation";
        }
    }
}
