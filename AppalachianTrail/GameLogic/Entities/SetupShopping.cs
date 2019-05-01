using GameStorage.GameValues;
using GameUI;
using System.Collections.Generic;

namespace GameLogic.Entities
{
    public class SetupShopping : Shopping
    {
        public Dictionary<BackpackItem, int> ShoppingCart = new Dictionary<BackpackItem, int>();

        private GameUIAdapter m_GameUIAdapter;

        private Hiker m_Hiker;

        public SetupShopping(GameUIAdapter gameAdapter, Hiker hiker)
        {
            m_GameUIAdapter = gameAdapter;
            m_Hiker = hiker;
        }

        public void Initialize()
        {
            m_GameUIAdapter.Shopping(m_Hiker.Wallet);
        }
    }
}
