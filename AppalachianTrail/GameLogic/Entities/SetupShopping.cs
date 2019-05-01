using GameStorage.GameValues;
using GameUI;
using System.Collections.Generic;

namespace GameLogic.Entities
{
    public class SetupShopping : Shopping
    {
        public Dictionary<BackpackItem, int> ShoppingCart = new Dictionary<BackpackItem, int>();

        public SetupShopping(GameUIAdapter gameAdapter, Hiker hiker)
        {
            gameAdapter.StartShopping(hiker.Wallet);
        }
    }
}
