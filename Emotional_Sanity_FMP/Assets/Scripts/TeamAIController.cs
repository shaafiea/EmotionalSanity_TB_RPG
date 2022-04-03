using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TeamAIController : MonoBehaviour
{

    //AI Target
    public BaseEntities target;
    public GameObject bigHealspot;
    public int n_randomrange;

    [SerializeField] private BaseEntities aiStats;
    public TurnBasedBattleSystem tbbs;

    public GameObject grassVFX;
    public GameObject iceVFX;
    public GameObject fireVFX;
    public GameObject waterVFX;
    public GameObject rockVFX;
    public GameObject s_healVFX;
    public GameObject b_healVFX;
    public GameObject sanityVFX;

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

    public void AISpell()
    {
        target.TakeSpecialDamage(target.entityWeakness[0], (aiStats.spellmoves[0].damage), (aiStats.spellmoves[0].spTaken), aiStats.manastrength);
        Debug.Log(aiStats.entityName + " Used Their Attack!");
    }

    public void SmallHeal()
    {
        aiStats.MP = aiStats.MP - aiStats.spellmoves[1].mpUsed;
        aiStats.SP = aiStats.SP - aiStats.spellmoves[1].spUsed;
        if (tbbs.player1.HP > 0 && n_randomrange == 0)
        {
            tbbs.player1.TakeHeal(aiStats.spellmoves[1].damage);
        }

        if (tbbs.player2.HP > 0 && n_randomrange == 1)
        {
            tbbs.player2.TakeHeal(aiStats.spellmoves[1].damage);
        }

        if (tbbs.player3.HP > 0 && n_randomrange == 2)
        {
            tbbs.player3.TakeHeal(aiStats.spellmoves[1].damage);
        }

        if(tbbs.player4.HP > 0 && n_randomrange == 3)
        {
            tbbs.player4.TakeHeal(aiStats.spellmoves[1].damage);
        }
    }

    public void BigHeal()
    {
        aiStats.MP = aiStats.MP - aiStats.spellmoves[2].mpUsed;
        aiStats.SP = aiStats.SP - aiStats.spellmoves[2].spUsed;
        if (tbbs.player1.HP > 0)
        {
            tbbs.player1.TakeHeal(aiStats.spellmoves[2].damage);
            if (tbbs.player1.HP > tbbs.player1.maxHP)
            {
                tbbs.player1.HP = tbbs.player1.maxHP;
            }
        }

        if (tbbs.player2.HP > 0)
        {
            tbbs.player2.TakeHeal(aiStats.spellmoves[2].damage);
        }

        if (tbbs.player3.HP > 0)
        {
            tbbs.player3.TakeHeal(aiStats.spellmoves[2].damage);
        }

        if (tbbs.player4.HP > 0)
        {
            tbbs.player4.TakeHeal(aiStats.spellmoves[2].damage);
        }
    }

    public void SanityRegen()
    {
        aiStats.SanityRegen();
        Debug.Log(aiStats.entityName + "Regened their Sanity!");
    }

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

    public void GrassWhistleAnim()
    {
        aiStats.MP = aiStats.MP - aiStats.spellmoves[0].mpUsed;
        aiStats.SP = aiStats.SP - aiStats.spellmoves[0].spUsed;
        GameObject cloneobject = Instantiate(grassVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void FireBurnAnim()
    {
        aiStats.MP = aiStats.MP - aiStats.spellmoves[0].mpUsed;
        aiStats.SP = aiStats.SP - aiStats.spellmoves[0].spUsed;
        GameObject cloneobject = Instantiate(fireVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void WaterSplashAnim()
    {
        aiStats.MP = aiStats.MP - aiStats.spellmoves[0].mpUsed;
        aiStats.SP = aiStats.SP - aiStats.spellmoves[0].spUsed;
        GameObject cloneobject = Instantiate(waterVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void IceSpikeAnim()
    {
        aiStats.MP = aiStats.MP - aiStats.spellmoves[0].mpUsed;
        aiStats.SP = aiStats.SP - aiStats.spellmoves[0].spUsed;
        GameObject cloneobject = Instantiate(iceVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void SmallHealAnim()
    {
        n_randomrange = Random.Range(0, 3);
        if (n_randomrange == 0)
        {
            target = tbbs.player1;
        }
        if (n_randomrange == 1)
        {
            target = tbbs.player2;
        }
        if (n_randomrange == 2)
        {
            target = tbbs.player3;
        }
        if (n_randomrange == 3)
        {
            target = tbbs.player4;
        }
        GameObject cloneobject = Instantiate(s_healVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void BigHealAnim()
    {
        GameObject cloneobject = Instantiate(b_healVFX, bigHealspot.transform.position, bigHealspot.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void SanityRegenAnim()
    {
        GameObject cloneobject = Instantiate(sanityVFX, aiStats.transform.position, aiStats.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void EndTurnAfterAnim()
    {
        tbbs.k_anim.SetBool("isSpelling", false);
        tbbs.k_anim.SetBool("isSanityRegen", false);

        tbbs.b_anim.SetBool("isSpelling", false);
        tbbs.b_anim.SetBool("isSanityRegen", false);

        tbbs.s_anim.SetBool("isSpelling", false);
        tbbs.s_anim.SetBool("isSanityRegen", false);
        tbbs.s_anim.SetBool("isHealing", false);
        tbbs.s_anim.SetBool("isBigHealing", false);

        tbbs.teamTD = false;
        tbbs.isTargeting = true;
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
