using System.Collections.Generic;

namespace GameLogic.Entities
{
    internal class Backpack
    {
        internal Dictionary<string, int> Items = new Dictionary<string, int>();

        internal void AddItemToBackpack(string itemName, int itemCount)
        {
            if (Items.ContainsKey(itemName))
            {
                Items.Add(itemName, itemCount);
            }
            else
            {
                Items[itemName] += itemCount;
            }
        }

        internal void UseItemFromBackpack(string itemName, int amountUsed)
        {
            if(Items.ContainsKey(itemName))
            {
                Items[itemName] -= amountUsed;
            }
        }
    }
}
