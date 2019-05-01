using GameLogic.Entities;
using GameStorage.GameValues;
using GameUI;

namespace GameLogic
{
    public class GameController
    {
        private GameUIAdapter m_GameUIAdapter = new GameUIAdapter();

        private Trail m_Trail;

        private Hiker m_Hiker;

        public void Initialize()
        {
            InitializeGameUIAdapter();
        }

        public void StartSetup()
        {
            SetupTrail();

            SetupHiker();

            SetupShopping();
        }

        public void SetupShopping()
        {
            SetupShopping setupShopping = new SetupShopping(m_GameUIAdapter, m_Hiker);
            setupShopping.Initialize();
        }

        public void StartShopping()
        {
            m_Hiker.Backpack.AddItemToBackpack(BackpackItem.OuncesOfFood, 1000);
        }

        public void StartGameLoop()
        {
            Location newLocation = m_Trail.GetNextLocation(m_Hiker.CurrentLocation);

            while (newLocation != null)
            {
                m_Hiker.DistanceToNextLocation = m_Trail.GetDistanceToNextLocation(m_Hiker.CurrentLocation);

                while (m_Hiker.DistanceToNextLocation > 0)
                {
                    UpdateGameUIToShowTravel();

                    var distanceCoveredInDay = (int)m_Hiker.CurrentPace * 7;

                    m_Hiker.DistanceToNextLocation -= distanceCoveredInDay;

                    m_Hiker.TotalMilesTraveled += distanceCoveredInDay;

                    m_Hiker.CurrentDate = m_Hiker.CurrentDate.AddDays(1);

                    m_Hiker.Backpack.UseItemFromBackpack(BackpackItem.OuncesOfFood, 5);
                }

                m_Hiker.CurrentLocation = newLocation;

                m_Hiker.DistanceToNextLocation = 0;

                UpdateGameUIToArriveAtLocation(newLocation);

                newLocation = m_Trail.GetNextLocation(m_Hiker.CurrentLocation);

            }

            m_GameUIAdapter.DisplayGameWin();
        }

        private void InitializeGameUIAdapter()
        {
            m_GameUIAdapter.Initialize();
        }

        private void SetupTrail()
        {
            m_Trail = new Trail();
        }

        private void SetupHiker()
        {
            m_Hiker = new Hiker(m_GameUIAdapter.GetName(), m_GameUIAdapter.GetOccupation(), m_GameUIAdapter.GetStartDate(), m_Trail.GetFirstTrailLocation());
        }

        private void UpdateGameUIToShowTravel()
        {
            m_GameUIAdapter.DisplayTrailSegmentProgression(m_Hiker.CurrentDate, m_Hiker.CurrentLocation.AverageWeatherForEachMonth[m_Hiker.CurrentDate.Month], m_Hiker.CurrentHealthStatus, m_Hiker.Backpack.GetCountOfItems(BackpackItem.OuncesOfFood), m_Hiker.DistanceToNextLocation, m_Hiker.TotalMilesTraveled);
        }

        private void UpdateGameUIToArriveAtLocation(Location newLocation)
        {
            m_GameUIAdapter.DisplayLocationMenu(newLocation.Name);
        }
    }
}
