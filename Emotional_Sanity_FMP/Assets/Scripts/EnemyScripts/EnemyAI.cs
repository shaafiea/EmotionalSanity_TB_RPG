using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] BaseEntities enemy;
    [SerializeField] BaseEntities player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Basic Attacking Scripts to test damage
    public void Attack()
    {
        player.TakeWeaponDamage(enemy.damage, enemy.weaponstrength);
        Debug.Log("Enemy Weapon Hurt");
    }

    public void Spell()
    {
        enemy.TakeSpecialDamage(player.entityWeakness[0], (enemy.spellmoves[0].damage), player.manastrength);
        Debug.Log("Enemy Used Their Attack!");
        Debug.Log(enemy.spellmoves[0].damage * enemy.manastrength / player.manadefence);
        Debug.Log("Enemy Spell Hurt");
    }


}
