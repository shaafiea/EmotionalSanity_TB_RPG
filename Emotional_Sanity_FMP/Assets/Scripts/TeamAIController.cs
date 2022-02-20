using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TeamAIController : MonoBehaviour
{

    [SerializeField] private BaseEntities aiStats;
    public TurnBasedBattleSystem tbbs;

    //AI Bools
    bool isP2 = false;
    bool isP3 = false;
    bool isP4 = false;
    bool isAttacking = false;
    bool playerTurn = false;
    bool blocking = false;

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
        //If it is the players(AI) turn turn off the blocking state
        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players && tbbs.playerIndex == 0 && blocking == true)
        {
            blocking = false;
            aiStats.isBlocking = false;
            Debug.Log("Players Turn AI teammate block is now: " + this.gameObject + aiStats.isBlocking);
        }
    }

    //Do damage to the Enemy Targeted
    public void AIWeaponAttack()
    {
        tbbs.enemies[Random.Range(0, tbbs.enemies.Count)].GetComponent<BaseEntities>().TakeWeaponDamage(aiStats.damage, aiStats.weaponstrength);
        Debug.Log(this.gameObject + " Has Attacked!");
        tbbs.EndPlayerTurn();
    }

    //Enable Blocking
    public void AIBlock()
    {
        blocking = true;
        aiStats.isBlocking = true;
        Debug.Log(this.gameObject + "Has Blocked " + aiStats.isBlocking);
        tbbs.EndPlayerTurn();
    }
    
    public void EndTurnAfterAnim()
    {
        tbbs.EndPlayerTurn();
    }

}
