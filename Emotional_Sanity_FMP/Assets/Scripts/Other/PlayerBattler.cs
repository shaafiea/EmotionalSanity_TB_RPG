using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattler : MonoBehaviour
{
    PlayerBase _base;
    int level;

    public PlayerBattler(PlayerBase pBase, int pLevel)
    {
        _base = pBase;
        level = pLevel;
    }

    //This is the damage calculation for magical attacks 
    //Reference for magic attack damage calculation: https://bulbapedia.bulbagarden.net/wiki/Stat
    public int magicAttack
    {
        get { return Mathf.FloorToInt(((_base.MagicAttack * level) / 100f) + 5); }
    }

    //This is the damage calculation for weapon attacks
    //Reference for weapon attack damage calculation: https://megamitensei.fandom.com/wiki/Battle_Stats#Agility
    public int weaponAttack
    {
        get { return Mathf.FloorToInt(((_base.WeaponStrength * (level) / 5) / 100f) + weaponAttack); }
    }

}
