using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellMove", menuName = "SpellMove/Create a Spell")]
public class EntityMoves : ScriptableObject
{
    public enum SpellType
    {
        None,
        Normal,
        Fire,
        Water,
        Grass,
    }

    public string entityName;
    public string entityDescription;
    public int damage = 10;
    public int mpUsed = 10;
    public int spUsed = 10;
    public SpellType spellType;
}
