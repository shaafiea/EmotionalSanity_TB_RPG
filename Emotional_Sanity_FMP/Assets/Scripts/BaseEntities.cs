using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseEntities : MonoBehaviour
{

    [Header("Base Stats")]
    public float HP = 0;
    public float MP = 0;
    public float SP = 0;
    public string entityName;
    public int maxHP = 0;
    public int maxMP = 0;
    public int maxSP = 0;
    public float weaponstrength = 1;
    public float manastrength = 1;
    public float weapondefence = 1;
    public float manadefence;
    public int damage = 10;
    public bool isDead;
    public enum PlayerType
    {
        None,
        Normal,
        Fire,
        Water,
        Grass,
    }

    public List<PlayerType> entityType; // Player Types to show in UI of what they are strong at
    public List<PlayerType> entityWeakness; //Player Weakness for damage scaling and know what they are weak against
    public List<EntityMoves> spellmoves;

    private void Awake()
    {
        gameObject.name = entityName;
        HP = maxHP;
        MP = maxMP;
        SP = maxSP;
        isDead = false;
    }

    // Player/Enemy Attacks

    //Weapon Damage is just a simple damage script from the weapon stat and characters damage stat
    public void TakeWeaponDamage(int damage, float strength = 1)
    {
        HP -= (damage * (int)strength) / (int)weapondefence;
    }

    public void TakeSpecialDamage(PlayerType ptype, int damage, float strength = 1)
    {
        //Weaknesses from special moves if a player or enemy has a weakness. Give a damage multiplier to the spell attack
        for (int i = 0; i < entityWeakness.Count; i++)
        {
            if (ptype == entityWeakness[i])
            {
                HP -= (int)((damage * (int)strength) / (int)manadefence * 1.5);
                break;
            }
            
        }
        HP -= (damage * (int)strength) / (int)manadefence;
    }
}
