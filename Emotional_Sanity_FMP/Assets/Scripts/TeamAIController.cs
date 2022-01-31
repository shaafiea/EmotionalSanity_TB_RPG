using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TeamAIController : MonoBehaviour
{

    [SerializeField] private BaseEntities aiStats;
    public TurnBasedBattleSystem tbbs;

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
        
    }

    public void AIWeaponAttack()
    {
        tbbs.enemies[Random.Range(0, tbbs.enemies.Count)].GetComponent<BaseEntities>().TakeWeaponDamage(aiStats.damage, aiStats.weaponstrength);
        Debug.Log(this.gameObject + " Has Attacked!");
        tbbs.EndPlayerTurn();
    }

    public void AIBlock()
    {
        Debug.Log(this.gameObject + "Has Blocked");
        tbbs.EndPlayerTurn();
    }
    
    public void EndTurnAfterAnim()
    {
        tbbs.EndPlayerTurn();
    }

}
