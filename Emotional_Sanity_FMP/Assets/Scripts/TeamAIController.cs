using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TeamAIController : MonoBehaviour
{

    //AI Target
    public BaseEntities target;

    [SerializeField] private BaseEntities aiStats;
    public TurnBasedBattleSystem tbbs;

    //AI Bools
    bool isP2 = false;
    bool isP3 = false;
    bool isP4 = false;
    bool isAttacking = false;
    bool playerTurn = false;
    bool blocking = false;
    [SerializeField] float blockCountdown;
    //States
    public enum AIState
    {
        Attack,
        Block,
        Spell,
        Random
    }

    public AIState state;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Current HP of " + gameObject + ": " + teamMateStats.CurHP);
        aiStats = GetComponent<BaseEntities>();
        tbbs = GameObject.Find("TBBSystem").GetComponent<TurnBasedBattleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (blockCountdown > 0)
        {
            blockCountdown -= Time.deltaTime;
        }
        //If it is the players(AI) turn turn off the blocking state when it is the players turn
        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players && tbbs.playerIndex == 0 && blocking == true)
        {
            blocking = false;
            aiStats.isBlocking = false;
            Debug.Log("Players Turn AI teammate block is now: " + this.gameObject + aiStats.isBlocking);
            tbbs.k_anim.Play("Idle");
            tbbs.s_anim.Play("Idle");
            tbbs.b_anim.Play("Idle");
        }
    }


    public void AITarget()
    {
        target = tbbs.enemies[Random.Range(0, tbbs.enemies.Count)].GetComponent<BaseEntities>();
    }
    //Do damage to the Enemy Targeted
    public void AIWeaponAttack()
    {
        target.TakeWeaponDamage(aiStats.damage, aiStats.weaponstrength);
        Debug.Log(this.gameObject + " Has Attacked!");
        //tbbs.EndPlayerTurn();
    }
    //Do damage to the Enemy Targeted
    public void AIWeaponAttack2()
    {
        tbbs.enemies[Random.Range(0, tbbs.enemies.Count)].GetComponent<BaseEntities>().TakeWeaponDamage(aiStats.damage, aiStats.weaponstrength);
        Debug.Log(this.gameObject + " Has Attacked!");
        tbbs.EndPlayerTurn();
    }
    //Enable Blocking
    public void AIBlock()
    {
        blockCountdown = 2;
        blocking = true;
        aiStats.isBlocking = true;
        Debug.Log(this.gameObject + "Has Blocked " + aiStats.isBlocking);
    }
    
    public void EndTurnAfterAnim()
    {
        tbbs.EndPlayerTurn();
    }

    public void WalkBack()
    {
        Debug.Log("Walk To Original Position");
        tbbs.p2_isAttacking = false;
        tbbs.k_anim.Play("Walk");
    }


    void FootL()
    {

    }

    void FootR()
    {

    }

}
