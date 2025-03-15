using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
   public List<inventorySlot> Container = new List<inventorySlot>();
    public void AddItem(ItemObjects _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item) 
            {
                Container[i].addAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem) 
        { 
         Container.Add(new inventorySlot(_item, _amount));
        }
    }
}

[System.Serializable]
public class inventorySlot
{
    public ItemObjects item;
    public int Amount;
    public inventorySlot(ItemObjects _Item, int _Amount)
    {
        item = _Item;
        Amount = _Amount;
    }

    public void addAmount(int value)
    {
        Amount += value;
    }
}
