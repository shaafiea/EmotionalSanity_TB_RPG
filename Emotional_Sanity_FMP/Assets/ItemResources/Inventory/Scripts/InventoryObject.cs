using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject { 


    public ItemDatabaseObject database;
    public string savepath;
    public Inventory Container;

    // Adding items to our inventory slot
    public void AddItem(Item _item, int _amount)
    {
       //Check and see if an item has buffs
        if (_item.buffs.Length > 0) //if the item has buffs
        {
            //add it to the container as a seperate item (allows the same items to hold different values)
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
            return;
        }

        // For loop to see if the item we have is already in the inventory system
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item.Id == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
      
    }

    [ContextMenu("Save")]
    public void Save()
    {
        /*string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savepath));
        bf.Serialize(file, saveData);*/

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savepath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();

    }

    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savepath)))
        {
            /*  BinaryFormatter bf = new BinaryFormatter();
              FileStream file = File.Open(string.Concat(Application.persistentDataPath, savepath), FileMode.Open);
              JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
              file.Close();*/

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savepath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Items = new List<InventorySlot>(); // Store type of items
}

[System.Serializable] // Allows the item to show up in the editor
public class InventorySlot
{
    public Item item;
    public int amount;
    public int ID;

    //Inventory system will ask for item and amount
    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    // Add amount to the inventory slot
    public void AddAmount(int value)
    {
        amount += value;
    }

}
