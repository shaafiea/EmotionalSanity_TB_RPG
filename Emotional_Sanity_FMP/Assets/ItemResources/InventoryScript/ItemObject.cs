using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Class because its the base class to create a item

public enum ItemType // Types of items we have in the game
{
    Potion,
    Equipment,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;

}
