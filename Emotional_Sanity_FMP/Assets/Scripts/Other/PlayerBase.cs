using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Makes it so players can be created as assets (Scriptiable Objects)
[CreateAssetMenu(fileName = "PlayerBattle", menuName = "PlayerBattle/Create a Battle Character For the Player Team")]
public class PlayerBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private GameObject playerPrefab;

    //Characters are able to have 2 types
    [SerializeField] CharacterType _type1;
    [SerializeField] CharacterType _type2;

    //Character Stats
    public int _maxHp;
    public int _maxMP;
    public int _maxSB;
    public int _weaponAttack;
    public int _magicAttack;
    public int _defense;
    public int _speed;

    // Character types (Determine what the character is good for)
    public enum CharacterType
    {
        None,
        Normal,
        Fire,
        Water,
        Grass,
        Magic,
    }
}
