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
    public int n_randomrange;

    public GameObject grassVFX;
    public GameObject iceVFX;
    public GameObject fireVFX;
    public GameObject waterVFX;
    public GameObject rockVFX;
    public GameObject s_healVFX;
    public GameObject b_healVFX;
    public GameObject sanityVFX;
    public GameObject bigHealspot;

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
        enemyStats.WeaponSanity();
        if (tbbs.randomrangeacc <= enemyStats.accuracy)
        {
            target.TakeWeaponDamage(enemyStats.damage, enemyStats.weaponstrength);
        } else
        {
            tbbs.dpMove.MissDisplay(enemyStats.entityName);
        }
        //ttbs.EndEnemyTurn();
    }

    public void EnemyBlock()
    {
        blocking = true;
        enemyStats.turnblock = true;
        enemyStats.isBlocking = true;
        StartCoroutine(EnemyBlockWaitTime());
        //Debug.Log(this.gameObject + "Has Blocked " + enemyStats.isBlocking);
    }

    public IEnumerator EnemyBlockWaitTime()
    {
        yield return new WaitForSeconds(1.5f);
        if (enemyStats.turnblock == true)
        {
            enemyStats.BlockSanity();
            enemyStats.turnblock = false;
            tbbs.EndEnemyTurn();
        }
    }
    public void EnemySpell()
    {
        target.TakeSpecialDamage(target.entityWeakness[0], (enemyStats.spellmoves[0].damage), (enemyStats.spellmoves[0].spTaken), enemyStats.manastrength);
        Debug.Log("Enemy Used Their Attack!");
        Debug.Log("Enemy Spell Hurt");
    }

    public void SmallHeal()
    {
        enemyStats.MP = enemyStats.MP - enemyStats.spellmoves[1].mpUsed;
        enemyStats.SP = enemyStats.SP - enemyStats.spellmoves[1].spUsed;
        if (tbbs.enemy1.HP > 0 && n_randomrange == 0)
        {
            tbbs.enemy1.TakeHeal(enemyStats.spellmoves[1].damage);
        }

        if (tbbs.enemy2.HP > 0 && n_randomrange == 1)
        {
            tbbs.enemy2.TakeHeal(enemyStats.spellmoves[1].damage);
        }

        if (tbbs.enemy3.HP > 0 && n_randomrange == 2)
        {
            tbbs.enemy3.TakeHeal(enemyStats.spellmoves[1].damage);
        }
    }

    public void BigHeal()
    {
        enemyStats.MP = enemyStats.MP - enemyStats.spellmoves[2].mpUsed;
        enemyStats.SP = enemyStats.SP - enemyStats.spellmoves[2].spUsed;
        if (tbbs.enemy1.HP > 0)
        {
            tbbs.enemy1.TakeHeal(enemyStats.spellmoves[2].damage);
        }

        if (tbbs.enemy2.HP > 0)
        {
            tbbs.enemy2.TakeHeal(enemyStats.spellmoves[2].damage);
        }

        if (tbbs.enemy3.HP > 0)
        {
            tbbs.enemy3.TakeHeal(enemyStats.spellmoves[2].damage);
        }
    }

    public void SanityRegen()
    {
        enemyStats.SanityRegen();
        Debug.Log(enemyStats.entityName + "Regened their Sanity!");
    }

    public void GrassWhistleAnim()
    {
        enemyStats.MP = enemyStats.MP - enemyStats.spellmoves[0].mpUsed;
        enemyStats.SP = enemyStats.SP - enemyStats.spellmoves[0].spUsed;
        GameObject cloneobject = Instantiate(grassVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void FireBurnAnim()
    {
        enemyStats.MP = enemyStats.MP - enemyStats.spellmoves[0].mpUsed;
        enemyStats.SP = enemyStats.SP - enemyStats.spellmoves[0].spUsed;
        GameObject cloneobject = Instantiate(fireVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void WaterSplashAnim()
    {
        enemyStats.MP = enemyStats.MP - enemyStats.spellmoves[0].mpUsed;
        enemyStats.SP = enemyStats.SP - enemyStats.spellmoves[0].spUsed;
        GameObject cloneobject = Instantiate(waterVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void IceSpikeAnim()
    {
        enemyStats.MP = enemyStats.MP - enemyStats.spellmoves[0].mpUsed;
        enemyStats.SP = enemyStats.SP - enemyStats.spellmoves[0].spUsed;
        GameObject cloneobject = Instantiate(iceVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }
    public void SmallHealAnim()
    {
        n_randomrange = Random.Range(0, 2);
        if (n_randomrange == 0)
        {
            target = tbbs.enemy1;
        }
        if (n_randomrange == 1)
        {
            target = tbbs.enemy2;
        }
        if (n_randomrange == 2)
        {
            target = tbbs.enemy3;
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
        GameObject cloneobject = Instantiate(sanityVFX, enemyStats.transform.position, enemyStats.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void E1SpellUsed()
    {
        tbbs.e1_anim.SetBool("spellUsed", false);
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

    public void EndTurnAfterAnim()
    {
        tbbs.e1_anim.SetBool("isSpelling", false);
        tbbs.e1_anim.SetBool("isSanityRegen", false);
        tbbs.e1_anim.SetBool("isHealing", false);
        tbbs.e1_anim.SetBool("isBigHealing", false);

        tbbs.e2_anim.SetBool("isSpelling", false);
        tbbs.e2_anim.SetBool("isSanityRegen", false);
        tbbs.e2_anim.SetBool("isHealing", false);
        tbbs.e2_anim.SetBool("isBigHealing", false);

        tbbs.e3_anim.SetBool("isSpelling", false);
        tbbs.e3_anim.SetBool("isSanityRegen", false);
        tbbs.e3_anim.SetBool("isHealing", false);
        tbbs.e3_anim.SetBool("isBigHealing", false);
        tbbs.isTargeting = true;
        tbbs.EndEnemyTurn();
    }
}
