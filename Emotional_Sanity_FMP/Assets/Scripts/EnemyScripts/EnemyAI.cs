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

    //Team Target
    public BaseEntities target;

    //EnemyBools
    bool isAttacking = false;
    bool enemyTurn = false;
    bool blocking = false;

    //States
    public enum EnemyState
    {
        Attack,
        Block,
        Spell
    }

    public EnemyState state;

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

        for (int i = 0; i < tbbs.enemies.Count; i++)
        {
            //If it is the players(AI) turn turn off the blocking state when it is the players turn
            if (tbbs.currentTurns == TurnBasedBattleSystem.turns.enemies && tbbs.enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && tbbs.enemyIndex == i && blocking == true)
            {
                blocking = false;
                enemyStats.isBlocking = false;
                tbbs.e1_anim.SetBool("isBlocking", false);
                tbbs.e2_anim.SetBool("isBlocking", false);
                tbbs.e3_anim.SetBool("isBlocking", false);
                Debug.Log("Enemy Turn Enemy team block is now: " + this.gameObject + enemyStats.isBlocking);
                if (tbbs.e1_state.enemyStats.HP > 0)
                {
                    tbbs.e1_anim.Play("Idle");
                }
                if (tbbs.e2_state.enemyStats.HP > 0)
                {
                    tbbs.e2_anim.Play("Idle");
                }
                if (tbbs.e3_state.enemyStats.HP > 0)
                {
                    tbbs.e3_anim.Play("Idle");
                }

            }
        }
    }

    public void AITarget()
    {
        target = tbbs.players[Random.Range(0, tbbs.players.Count)].GetComponent<BaseEntities>();
    }

    //Basic Attacking Scripts to test damage
    public void EnemyWeaponAttack()
    {
        target.TakeWeaponDamage(enemyStats.damage, enemyStats.weaponstrength);
        //ttbs.EndEnemyTurn();
    }

    public void EnemyBlock()
    {
        enemyStats.BlockSanity();
        blocking = true;
        enemyStats.isBlocking = true;
        tbbs.EndEnemyTurn();
        Debug.Log(this.gameObject + "Has Blocked " + enemyStats.isBlocking);
    }

    public void EnemySpell()
    {
        target.TakeSpecialDamage(target.entityWeakness[0], (enemyStats.spellmoves[0].damage), enemyStats.manastrength);
        Debug.Log("Enemy Used Their Attack!");
        Debug.Log("Enemy Spell Hurt");
        tbbs.EndEnemyTurn();
    }

    public void EWalkBack()
    {
        Debug.Log("Walkback is called");
        for (int i = 0; i < tbbs.enemies.Count; i++)
        {
            if (tbbs.enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && tbbs.enemyIndex == i)
            {
                tbbs.e1_isAttacking = false;
                Debug.Log("Enemy 1 Has Attacked is Retreating");
            }

            if (tbbs.enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy2" && tbbs.enemyIndex == i)
            {
                tbbs.e2_isAttacking = false;
                Debug.Log("Enemy 2 Has Attacked is Retreating");
            }

            if (tbbs.enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy3" && tbbs.enemyIndex == i)
            {
                tbbs.e3_isAttacking = false;
                Debug.Log("Enemy 3 Has Attacked is Retreating");
            }
        }
    }
}
