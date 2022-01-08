using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        //Call this after seralization, set the item ids 
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].id = i;
            GetItem.Add(i, Items[i]);
        }

  
    }

    public void OnBeforeSerialize()
    {
        //Call this before seralization, to clear items out
        GetItem = new Dictionary<int, ItemObject>();
    }
}
