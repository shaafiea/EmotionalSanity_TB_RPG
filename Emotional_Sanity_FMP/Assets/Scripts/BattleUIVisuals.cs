using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIVisuals : MonoBehaviour
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
        enemy.TakeWeaponDamage(player.damage,player.weaponstrength);
        Debug.Log("Enemy Weapon Hurt");
    }

    public void Spell()
    {
        enemy.TakeSpecialDamage(enemy.entityWeakness[0],(player.spellmoves[0].damage),player.manastrength);
        Debug.Log(player.spellmoves[0].damage * player.manastrength / enemy.manadefence);
        Debug.Log("Enemy Spell Hurt");
    }

}
