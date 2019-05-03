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
            while(m_GameUIAdapter.StartShopping(m_Hiker.Wallet))
            {
                GetShoppingItem();
            }
        }

        public void GetShoppingItem()
        {
            int purchaseAmount = 0;
            BackpackItem itemPurchased = m_GameUIAdapter.PurchaseShoppingItem(out purchaseAmount);
            m_Hiker.Wallet -= (int)itemPurchased * purchaseAmount;
            ShoppingCart.Add(itemPurchased, purchaseAmount);
        }
    }
}
