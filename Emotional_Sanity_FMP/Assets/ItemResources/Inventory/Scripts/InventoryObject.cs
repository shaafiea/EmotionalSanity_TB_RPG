using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>(); // Store type of items

    // Adding items to our inventory slot
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        // For loop to see if the item we have is already in the inventory system
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }
}

[System.Serializable] // Allows the item to show up in the editor
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    //Inventory system will ask for item and amount
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    // Add amount to the inventory slot
    public void AddAmount(int value)
    {
        amount += value;
    }

}
