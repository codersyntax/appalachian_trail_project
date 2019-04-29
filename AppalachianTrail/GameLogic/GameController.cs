using GameLogic.Entities;
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

            //TODO: Add in logic to support selecting occupation

            //TODO: Add in logic to start shopping process of filling out the hiker's backpack with supplies

            //TODO: Transition into game loop once setup is done
        }

        public void StartSetup()
        {
            SetupTrail();

            SetupHiker();
        }

        public void StartGameLoop()
        {

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
    }
}
