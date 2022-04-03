using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseEntities : MonoBehaviour
{
    //Basic Stats for All Fightable Characters
    [Header("Base Stats")]
    public float HP = 0;
    public float MP = 0;
    public float SP = 0;
    public string entityName;
    public string enemyOrder;
    public int maxHP = 0;
    public int maxMP = 0;
    public int maxSP = 0;
    public int accuracy = 0;
    public float weaponstrength = 1;
    public float manastrength = 1;
    public float weapondefence = 1;
    public float manadefence;
    public float weaponsanity = 5;
    public float blocksanity = 10;
    public int damage = 10;
    public int speed = 0;
    public bool isBlocking;
    public bool turnblock;
    public bool isDead;
    public enum PlayerType
    {
        None,
        Normal,
        Fire,
        Water,
        Grass,
        Ice,
        Rock,
    }

    public List<PlayerType> entityType; // Player Types to show in UI of what they are strong at
    public List<PlayerType> entityWeakness; //Player Weakness for damage scaling and know what they are weak against
    public List<EntityMoves> spellmoves;

    private void Awake()
    {
        //Always set the hp, mp, and sp to max if the player has started a new game!
        gameObject.name = entityName;
        HP = maxHP;
        MP = maxMP;
        SP = maxSP / 2;
        isDead = false;
        isBlocking = false;
    }

    // Player/Enemy Attacks

    //Weapon Damage is just a simple damage script from the weapon stat and characters damage stat 
    //If the player are blocking do less damage than usual
    public void TakeWeaponDamage(int damage, float strength = 1)
    {
            if (isBlocking == false)
            {
                if (SP <= 25)
                {
                    HP -= ((damage * (int)strength) / (int)weapondefence) * 1.5f;
                    if (SP < 0)
                    {
                        SP = 0;
                    }
                Debug.Log("Weapon Effective! + YOUR SANITY IS LOW THEREFORE YOU TOOK MORE DAMAGE THAN USUAL");
                    //Debug.Log(HP);
                } else if (SP >= 75)
                {
                    HP -= ((damage * (int)strength) / (int)weapondefence) * 0.7f;
                    Debug.Log("Weapon Effective! + YOUR SANITY IS HIGH THEREFORE YOU TOOK LESS DAMAGE THAN USUAL");
                    //Debug.Log(HP);
                } else
                {
                    HP -= (damage * (int)strength) / (int)weapondefence;
                    Debug.Log("Weapon Effective!");
                }
            }

            if (isBlocking == true)
            {
                if (SP <= 25)
                {
                    HP -= (((damage * (int)strength) / (int)weapondefence) * 75 / 100) * 1.5f;
                    if (SP < 0)
                    {
                        SP = 0;
                    }
                Debug.Log("Blocked!: Weapon Effective! + YOUR SANITY IS LOW THEREFORE YOU TOOK MORE DAMAGE THAN USUAL");
                    //Debug.Log(HP);
                }
                else if (SP >= 75)
                {
                    HP -= (((damage * (int)strength) / (int)weapondefence) * 75 / 100) * 0.7f;
                    Debug.Log("Blocked!: Weapon Effective! + YOUR SANITY IS HIGH THEREFORE YOU TOOK LESS DAMAGE THAN USUAL");
                    //Debug.Log(HP);
                }
                else
                {
                    HP -= ((damage * (int)strength) / (int)weapondefence) * 75 / 100;
                    Debug.Log("Blocked!: Weapon Effective!");
                }
            
        }
    }

    public void TakeSpecialDamage(PlayerType ptype, int damage, int sanitytaken, float strength = 1)
    {
        //Weaknesses from special moves if a player or enemy has a weakness. Give a damage multiplier to the spell attack
        for (int i = 0; i < entityWeakness.Count; i++)
        {
                if (ptype == entityWeakness[i] && isBlocking == false)
                {
                    if (SP <= 25)
                    {
                        HP -= (int)((((damage * (int)strength) / (int)manadefence * 1.5)) * 1.5f);
                        SP -= sanitytaken;
                        if (SP < 0)
                        {
                            SP = 0;
                        }
                    Debug.Log("Super Effective! + YOUR SANITY IS LOW SO YOU TOOK MORE DAMAGE THAN USUAL");
                       
                    } else if (SP >= 75)
                    {
                        HP -= (int)((((damage * (int)strength) / (int)manadefence * 1.5)) * 0.8f);
                        SP -= sanitytaken;
                        Debug.Log("Super Effective! + YOUR SANITY IS HIGH SO YOU TOOK LESS DAMAGE THAN USUAL");

                    } else
                    {
                        HP -= (int)((damage * (int)strength) / (int)manadefence * 1.5);
                        SP -= sanitytaken;
                        Debug.Log("Super Effective!");
                    }
                    break;
                }

                if (ptype == entityWeakness[i] && isBlocking == true)
                {
                    if (SP <= 25)
                    {
                        HP -= (int)(((damage * (int)strength) / (int)manadefence * 1.5) * 75 / 100) * 1.5f;
                        SP -= sanitytaken;
                        if (SP < 0)
                        {
                            SP = 0;
                        }
                        Debug.Log("Blocked Damage Reduction But still Super Effective! + SANITY IS LOW SO YOU TOOK MORE DAMAGE THAN USUAL");
                    } else if (SP >= 75)
                    {
                        HP -= (int)(((damage * (int)strength) / (int)manadefence * 1.5) * 75 / 100) * 0.7f;
                        SP -= sanitytaken;
                        Debug.Log("Blocked Damage Reduction But still Super Effective! + SANIITY IS HIGH SO YOU DROPPED TOOK LESS DAMAGE THAN USUAL");
                    } else
                    {
                        HP -= (int)(((damage * (int)strength) / (int)manadefence * 1.5) * 75 / 100);
                        SP -= sanitytaken;
                        Debug.Log("Blocked Damage Reduction But still Super Effective!");
                    }
                    break;
                }

        }

        if (isBlocking == false)
        {
            if (SP <= 25)
            {
                HP -= (int)(((damage * (int)strength) / (int)manadefence) * 1.5f);
                SP -= sanitytaken;
                if (SP < 0)
                {
                    SP = 0;
                }
                Debug.Log("Spell Attack: Effective! + YOUR SANITY IS LOW SO YOU TOOK MORE DAMAGE THAN USUAL");
            }
            else if (SP >= 75)
            {
                HP -= (int)(((damage * (int)strength) / (int)manadefence) * 0.8f);
                SP -= sanitytaken;
                Debug.Log("Spell Attack: Effective! + YOUR SANITY IS HIGH SO YOU TOOK LESS DAMAGE THAN USUAL");
            }
            else
            {
                HP -= (damage * (int)strength) / (int)manadefence;
                SP -= sanitytaken;
                Debug.Log("Spell Attack: Effective!");
            }
        }

        if (isBlocking == true)
        {
            HP -= ((damage * (int)strength) / (int)manadefence) * 75/100;
            SP -= sanitytaken;
            Debug.Log("Blocked! Spell Attack: Effective!");
        }
    }

    public void TakeHeal(int damage)
    {
        //Although we are using damage here we are actually healing since the healing spell is minus damage
        HP -= damage;
    }

    //Take away sanity if the player attacks with a weapon
    public void WeaponSanity()
    {
        SP -= weaponsanity;
    }

    //Add Sanity if the player is blocking
    public void BlockSanity()
    {
        SP += blocksanity;
    }

    //Regens alot of Sanity
    public void SanityRegen()
    {
        SP = SP + 15;
        MP = MP - 10;
    }
}
