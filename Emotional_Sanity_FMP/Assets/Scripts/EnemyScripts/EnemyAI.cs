using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] BaseEntities enemyStats;
    [SerializeField] private List<BaseEntities> playersOnScreen;
    [SerializeField] BattleUIVisuals BUIV;
    public TurnBasedBattleSystem tbbs;

    // Start is called before the first frame update
    void Awake()
    {
        enemyStats = GetComponent<BaseEntities>();
        tbbs = GameObject.Find("TBBSystem").GetComponent<TurnBasedBattleSystem>();
        BUIV = GameObject.Find("BattleUIManager").GetComponent<BattleUIVisuals>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Basic Attacking Scripts to test damage
    public void EnemyWeaponAttack()
    {
        playersOnScreen[Random.Range(0, playersOnScreen.Count)].TakeWeaponDamage(enemyStats.damage, enemyStats.weaponstrength);
        tbbs.EndEnemyTurn();
    }

    public void Spell()
    {
        /*enemy.TakeSpecialDamage(player.entityWeakness[0], (enemy.spellmoves[0].damage), player.manastrength);
        Debug.Log("Enemy Used Their Attack!");
        Debug.Log(enemy.spellmoves[0].damage * enemy.manastrength / player.manadefence);
        Debug.Log("Enemy Spell Hurt");*/
    }


}
