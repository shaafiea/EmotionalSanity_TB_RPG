using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    //Players are able to have 2 types
    [SerializeField] PlayerType _type1;
    [SerializeField] PlayerType _type2;

    public Dictionary<string, int> stats = null;

    private void Awake()
    {
        stats = new Dictionary<string, int>();
        stats.Add("Attack", 50);
        stats.Add("Defence", 50);
        stats.Add("Speed", 50);
        stats.Add("SpellMana", 50);
        stats.Add("SanityMana", 50);
        stats.Add("WeaponAttackBuff", 0);
    }

    //Player Typing
    public enum PlayerType
    {
        None,
        Normal,
        Fire,
        Water,
        Grass,
        Fighting,
    }

    public PlayerType Type1
    {
        get { return _type1; }
    }
    public PlayerType Type2
    {
        get { return _type2; }
    }

}
