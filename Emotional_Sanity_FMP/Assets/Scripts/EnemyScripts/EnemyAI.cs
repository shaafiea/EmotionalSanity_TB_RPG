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

    public void AITarget()
    {
        target = tbbs.players[Random.Range(0, tbbs.players.Count)].GetComponent<BaseEntities>();
    }

    //Basic Attacking Scripts to test damage
    public void EnemyWeaponAttack()
    {
        AITarget();
        target.TakeWeaponDamage(enemyStats.damage, enemyStats.weaponstrength);
        //ttbs.EndEnemyTurn();
    }
    //Basic Attacking Scripts to test damage
    public void EnemyWeaponAttack2()
    {
        AITarget();
        target.TakeWeaponDamage(enemyStats.damage, enemyStats.weaponstrength);
        //tbbs.EndEnemyTurn();
    }

    public void EnemyBlock()
    {
        blocking = true;
        enemyStats.isBlocking = true;
        //tbbs.EndEnemyTurn();
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
        for (int i = 0; i < tbbs.enemies.Count; i++)
        {
            if (tbbs.enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Grass)" && tbbs.enemyIndex == i)
            {
                tbbs.e1_isAttacking = false;
                tbbs.e1_anim.Play("Walk");
            }

            if (tbbs.enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Water)" && tbbs.enemyIndex == i)
            {
                tbbs.e2_isAttacking = false;
                tbbs.e2_anim.Play("Walk");
            }

            if (tbbs.enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Fire)" && tbbs.enemyIndex == i)
            {
                tbbs.e3_isAttacking = false;
                tbbs.e3_anim.Play("Walk");
            }
        }
    }
}
