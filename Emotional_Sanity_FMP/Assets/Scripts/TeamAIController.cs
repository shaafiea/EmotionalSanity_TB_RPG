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
        Heal,
        BigHeal,
        SanityRegen,
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

        for (int i = 0; i < tbbs.players.Count; i++)
        {
            //If it is the players(AI) turn turn off the blocking state when it is the players turn
            if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players && tbbs.players[i].GetComponent<BaseEntities>().entityName == "NinjaWarrior" && tbbs.playerIndex == i && blocking == true)
            {
                blocking = false;
                aiStats.isBlocking = false;
                tbbs.k_anim.SetBool("isBlocking", false);
                tbbs.s_anim.SetBool("isBlocking", false);
                tbbs.b_anim.SetBool("isBlocking", false);
                Debug.Log("Players Turn AI teammate block is now: " + this.gameObject + aiStats.isBlocking);
                if (tbbs.p2_State.aiStats.HP > 0)
                {
                    tbbs.k_anim.Play("Idle");
                }
                if (tbbs.p3_State.aiStats.HP > 0)
                {
                    tbbs.s_anim.Play("Idle");
                }
                if (tbbs.p4_State.aiStats.HP > 0)
                {
                    tbbs.b_anim.Play("Idle");
                }
               
            }
        }
    }


    public void AITarget()
    {
        target = tbbs.enemies[Random.Range(0, tbbs.enemies.Count)].GetComponent<BaseEntities>();
    }

    //Do damage to the Enemy Targeted
    public void AIWeaponAttack()
    {
        aiStats.WeaponSanity();
        target.TakeWeaponDamage(aiStats.damage, aiStats.weaponstrength);
        Debug.Log(this.gameObject + " Has Attacked " + target);
        //tbbs.EndPlayerTurn();
    }
   /* //Do damage to the Enemy Targeted
    public void AIWeaponAttack2()
    {
        tbbs.enemies[Random.Range(0, tbbs.enemies.Count)].GetComponent<BaseEntities>().TakeWeaponDamage(aiStats.damage, aiStats.weaponstrength);
        Debug.Log(this.gameObject + " Has Attacked!");
        tbbs.EndPlayerTurn();
    }*/
    //Enable Blocking
    public void AIBlock()
    {
        aiStats.BlockSanity();
        blockCountdown = 2;
        blocking = true;
        aiStats.isBlocking = true;
        tbbs.EndPlayerTurn();
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

    public void WalkBackP3()
    {
        Debug.Log("Walk To Original Position");
        tbbs.p3_isAttacking = false;
        tbbs.s_anim.Play("Walk");
    }

    public void WalkBackP4()
    {
        Debug.Log("Walk To Original Position");
        tbbs.p4_isAttacking = false;
        tbbs.b_anim.Play("Walk");
    }

    void FootL()
    {

    }

    void FootR()
    {

    }

}
