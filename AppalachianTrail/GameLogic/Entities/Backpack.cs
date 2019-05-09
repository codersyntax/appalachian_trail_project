using GameStorage.GameValues;
using System.Collections.Generic;

namespace GameLogic.Entities
{
    internal class Backpack
    {
        internal Dictionary<BackpackItem, int> Items = new Dictionary<BackpackItem, int>();

        internal int GetCountOfItems(BackpackItem itemName)
        {
            if (Items.ContainsKey(itemName))
            {
                return Items[itemName];
            }
            else
            {
                return 0;
            }
        }

        internal void AddItemToBackpack(BackpackItem itemName, int itemCount)
        {
            if (!Items.ContainsKey(itemName))
            {
                Items.Add(itemName, itemCount);
            }
            else
            {
                Items[itemName] += itemCount;
            }
        }

        internal void UseItemFromBackpack(BackpackItem itemName, int amountUsed)
        {
            if(Items.ContainsKey(itemName))
            {
                Items[itemName] -= amountUsed;
                if(Items[itemName] <= 0)
                {
                    Items.Remove(itemName);
                }
            }
        }
    }
}
