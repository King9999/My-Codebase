using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is used for any games that has loot that drops from enemies or treasure chests. This is typically used
 * for games that would have a lot of items, such as RPGs. The loot table works in 2 steps:
 
1. The game determines which table is accessed
2. The game pulls an item from the chosen table

In both steps, the tables and items have weight, that determines the rarity of accessing that content. This code should be 
modified to fit whatever game you're working on. For example, maybe you only need 1 list of items.

HOW WEIGHT WORKS
------
In order for the weights to work properly, the values must be in descending order (i.e. 300, 200, 100). The higher the weight
of an item is, the less rare it is. When you want to roll for an item, you add up the total weight of all items
in a table, then get a random value between 0 and the total weight. The value must be less than or equal to an item's weight to 
indicate success. If the roll is not successful, subtract the weight of the item from the current value, 
then check the next item's weight.

Note that the table is a scriptable object since it doesn't need to be a game object that would be visible in the game. But
there would need to be a reference to the object somewhere in your game.
 */
namespace MMurray.GenericCode
{
    [CreateAssetMenu(menuName = "Loot Table", fileName = "masterLootTable")]
    public class LootTable : ScriptableObject
    {
        public int[] tableWeight;               //determines which category of items to access. 
        public List<LootItem> consumables;      //single-use items
        public List<LootItem> equipment;       
        public List<LootItem> valuables;        //items that are sold

        //table indexes
        private const int VALUABLES = 0;
        private const int CONSUMABLES = 1;
        private const int EQUIPMENT = 2;
        private const int DUNGEON_MODS = 3;
        private const int DATA_LOGS = 4;

        public List<LootItem> GetTable()
        {
            //check which table is going to be accessed
            int totalWeight = 0;
            int tableIndex = 0;
            for (int i = 0; i < tableWeight.Length; i++)
            {
                totalWeight += tableWeight[i];
            }

            int randValue = UnityEngine.Random.Range(0, totalWeight);
            Debug.Log("randValue: " + randValue);

            int j = 0;
            bool tableFound = false;

            while (!tableFound && j < tableWeight.Length)
            //for (int i = 0; i < tableWeight.Length; i++)
            {
                if (randValue <= tableWeight[j])
                {
                    //create this item
                    tableIndex = j;
                    tableFound = true;
                    Debug.Log("Acessing table " + j + ", rand value is " + randValue);
                }
                else
                {
                    randValue -= tableWeight[j];
                    Debug.Log("Rand value is now " + randValue);
                    j++;
                }
            }

            //access list of items based on tableIndex
            List<LootItem> chosenTable = new List<LootItem>();
            switch (tableIndex)
            {
                case VALUABLES:
                    chosenTable = valuables;
                    break;

                case CONSUMABLES:
                    chosenTable = consumables;
                    break;

                case EQUIPMENT:
                    chosenTable = equipment;
                    break;

            }

            return chosenTable;

        }

        public Item GetItem(List<LootItem> table)
        {
            if (table.Count <= 0)
                return null;

            //get total weight of all items in the table
            int totalWeight = 0;
            for (int i = 0; i < table.Count; i++)
            {
                totalWeight += table[i].itemWeight;
            }

            Debug.Log("---Getting random value from GetItem---");
            int randValue = UnityEngine.Random.Range(0, totalWeight);
            Debug.Log("randValue: " + randValue);

            int j = 0;
            bool itemFound = false;
            int itemIndex = 0;

            while (!itemFound && j < table.Count)
            {
                if (randValue <= table[j].itemWeight)
                {
                    //create this item
                    itemIndex = j;
                    itemFound = true;
                    Debug.Log("Acessing item " + j + ", rand value is " + randValue);
                }
                else
                {
                    randValue -= table[j].itemWeight;
                    Debug.Log("Rand value is now " + randValue);
                    j++;
                }
            }

            if (itemFound)
                return table[itemIndex].item;
            else
                return null;

        }

    }


    [Serializable]
    public class LootItem
    {
        public Item item;
        public int itemWeight;

    }
}
