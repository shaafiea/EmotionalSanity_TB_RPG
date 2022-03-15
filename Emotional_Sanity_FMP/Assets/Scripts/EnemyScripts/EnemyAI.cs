using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] BaseEntities enemyStats;
    [SerializeField] private List<BaseEntities> playersOnScreen;
    [SerializeField] BattleUIVisuals BUIV;
    public TurnBasedBattleSystem tbbs;

    //EnemyBools
    bool isAttacking = false;
    bool enemyTurn = false;
    bool blocking = false;

    // Start is called before the first frame update
    void Awake()
    {
        enemyStats = GetComponent<BaseEntities>();
        tbbs = GameObject.Find("TBBSystem").GetComponent<TurnBasedBattleSystem>();
        BUIV = GameObject.Find("BattleUIManager").GetComponent<BattleUIVisuals>();
        BUIV.enemies.Add(this.gameObject.GetComponent<BaseEntities>());
    }

    // Update is called once per frame
    void Update()
    {
        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.enemies && tbbs.enemyIndex != 0 && blocking == true)
        {
            //would be where their anim goes.
            Debug.Log("Not enemies turn");
        }

        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.enemies && tbbs.enemyIndex == 0 && blocking == true)
        {
            blocking = false;
            enemyStats.isBlocking = false;
            Debug.Log("Enemies Turn Blocking state is now : " + this.gameObject + enemyStats.isBlocking);
        }

        if (enemyStats.HP <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    //Basic Attacking Scripts to test damage
    public void EnemyWeaponAttack()
    {
        playersOnScreen[Random.Range(0, playersOnScreen.Count)].TakeWeaponDamage(enemyStats.damage, enemyStats.weaponstrength);
        tbbs.EndEnemyTurn();
    }

    public void EnemyBlock()
    {
        blocking = true;
        enemyStats.isBlocking = true;
        tbbs.EndEnemyTurn();
    }

    public void EnemySpell()
    {
        int randomPlayer = Random.Range(0, playersOnScreen.Count);

        playersOnScreen[randomPlayer].TakeSpecialDamage(playersOnScreen[randomPlayer].entityWeakness[0], (enemyStats.spellmoves[0].damage), enemyStats.manastrength);
        Debug.Log("Enemy Used Their Attack!");
        Debug.Log("Enemy Spell Hurt");
        tbbs.EndEnemyTurn();
    }


}
