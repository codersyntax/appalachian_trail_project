using GameStorage.GameValues;
using GameUI;
using System.Collections.Generic;

namespace GameLogic.Entities
{
    public class Shopping
    {
        public Dictionary<BackpackItem, int> ShoppingCart = new Dictionary<BackpackItem, int>();

        private GameUIAdapter m_GameUIAdapter;

        private Hiker m_Hiker;

        public Shopping(GameUIAdapter gameAdapter, Hiker hiker)
        {
            m_GameUIAdapter = gameAdapter;
            m_Hiker = hiker;
        }

        public void StartShopping()
        {
            while(m_GameUIAdapter.StartShopping(m_Hiker.Wallet, m_Hiker.Backpack.Items, ShoppingCart))
            {
                GetShoppingItem();
            }
            foreach (var item in ShoppingCart)
            {
                m_Hiker.Backpack.AddItemToBackpack(item.Key, item.Value);
            }
            ShoppingCart.Clear();
        }

        public void GetShoppingItem()
        {
            int purchaseAmount = 0;
            BackpackItem itemPurchased = m_GameUIAdapter.PurchaseShoppingItem(out purchaseAmount);
            int purchaseTotal = (int)itemPurchased * purchaseAmount;
            if (purchaseTotal > m_Hiker.Wallet)
            {
                m_GameUIAdapter.DisplayInsufficientFunds();
            }
            else
            {
                m_Hiker.Wallet -= (int)itemPurchased * purchaseAmount;
                if(ShoppingCart.ContainsKey(itemPurchased))
                {
                    ShoppingCart[itemPurchased] += purchaseAmount;
                }
                else
                {
                    ShoppingCart.Add(itemPurchased, purchaseAmount);
                }
            }
        }
    }
}
