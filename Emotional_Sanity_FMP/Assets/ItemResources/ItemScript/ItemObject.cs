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

public enum Attributes
{
    Speed,
    Strength,
    Defence,
    Stamina,
    SanityStamina
}
public abstract class ItemObject : ScriptableObject
{
    public int id; // The Id of the item
    public Sprite uiDisplay; //display of the object
    public ItemType type;  // Type of item
    [TextArea(15, 20)]
    public string description; // Description of the item
    public ItemBuff[] buffs; // buffs for each tiem
    
    //Function to easily make new items
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.id;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max) //When an item is grabbed apply buffs and give a value bewteen the min and max values
            {
                attribute = item.buffs[i].attribute //give a value to each attribute in each element
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value; // Spefific value of the buff
    public int min; // The lowest possible roll
    public int max; // The highest possible roll

    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue()
    {
        //Give items a random value bewteen a certain ammount
        value = UnityEngine.Random.Range(min, max);
    }
}
