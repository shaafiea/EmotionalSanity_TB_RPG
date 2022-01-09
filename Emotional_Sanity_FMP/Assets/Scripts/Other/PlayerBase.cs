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
    [SerializeField] private int _curHp;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _curMP;
    [SerializeField] private int _maxMP;
    [SerializeField] private int _curSanitybar;
    [SerializeField] private int _maxSanitybar;
    [SerializeField] private int _weaponStrength;
    [SerializeField] private int _weaponAttack;
    [SerializeField] private int _magicAttack;
    [SerializeField] private int _defense;
    [SerializeField] private int _speed;

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

    //Creating a property to call the names of private variables
    public string Name
    {
        get { return name;}
    }

    public string Description
    {
        get { return description; }
    }

    public GameObject PlayerPrefab
    {
        get { return playerPrefab; }
    }
    public CharacterType Type1
    {
        get { return _type1; }
    }
    public CharacterType Type2
    {
        get { return _type2; }
    }

    public int CurHP
    {
        get { return _curHp; }
    }

    public int MaxHP
    {
        get { return _maxHp; }
    }

    public int CurMP
    {
        get { return _curMP; }
    }

    public int MaxMP
    {
        get { return _maxMP; }
    }

    public int CurSanityBar
    {
        get { return _curSanitybar; }
    }

    public int MaxSanityBar
    {
        get { return _maxSanitybar; }
    }

    public int WeaponStrength
    {
        get { return _weaponStrength; }
    }

    public int WeaponAttack
    {
        get { return _weaponAttack; }
    }
    public int MagicAttack
    {
        get { return _magicAttack; }
    }
    public int Defense
    {
        get { return _defense; }
    }
    public int Speed
    {
        get { return _speed; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
