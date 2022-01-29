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
    public bool isBlocking;
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
        SP = maxSP / 2;
        isDead = false;
        isBlocking = false;
    }

    // Player/Enemy Attacks

    //Weapon Damage is just a simple damage script from the weapon stat and characters damage stat
    public void TakeWeaponDamage(int damage, float strength = 1)
    {
        if (isBlocking == false)
        {
            HP -= (damage * (int)strength) / (int)weapondefence;
            Debug.Log("Weapon Effective!");
            Debug.Log(HP);
        }

        if (isBlocking == true)
        {
            HP -= ((damage * (int)strength) / (int)weapondefence) * 25/100;
            Debug.Log("Blocked! Damage Reduction");
            Debug.Log(HP);
        }
        
    }

    public void TakeSpecialDamage(PlayerType ptype, int damage, float strength = 1)
    {
        //Weaknesses from special moves if a player or enemy has a weakness. Give a damage multiplier to the spell attack
        for (int i = 0; i < entityWeakness.Count; i++)
        {
            if (ptype == entityWeakness[i] && isBlocking == false)
            {
                HP -= (int)((damage * (int)strength) / (int)manadefence * 1.5);
                Debug.Log("Super Effective!");
                break;
            }

            if (ptype == entityWeakness[i] && isBlocking == true)
            {
                HP -= (int)(((damage * (int)strength) / (int)manadefence * 1.5) * 25/100);
                Debug.Log("Blocked Damage Reduction But still Super Effective!");
                break;
            }

        }

        if (isBlocking == false)
        {
            HP -= (damage * (int)strength) / (int)manadefence;
            Debug.Log("Spell Attack: Effective!");
        }

        if (isBlocking == true)
        {
            HP -= ((damage * (int)strength) / (int)manadefence) * 25/100;
            Debug.Log("Blocked! Spell Attack: Effective!");
        }
    }
}
