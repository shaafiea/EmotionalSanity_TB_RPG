using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySpellAnim : MonoBehaviour
{
    //public GameObject bm;
    public Animator anim;
    public BattleUIVisuals BUIV;
    // Start is called before the first frame update
    void Start()
    {
        //bm = GameObject.Find("BattleUIManager").GetComponent<GameObject>();
        BUIV = GameObject.Find("BattleUIManager").GetComponent<BattleUIVisuals>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerFireSkill()
    {
       BUIV.PlayFireSkillAnim();
    }

    void PlayerWeaponAttack()
    {
        BUIV.AttackDamageDealt();
    }

    void PlayerEndTurn()
    {
        BUIV.EndTurnAfterAnim();
    }

    void PlayerWalkBack()
    {
        BUIV.WalkBack();
    }

    void PlayerSpellUsed()
    {
        BUIV.isSpellUsed = false;
    }

    void PlayerHealSingle()
    {
        BUIV.SmallHeal();
    }
    void PlayerSingleHeal()
    {
        BUIV.SmallHealAnim();
    }

    void PlayerHealAll()
    {
        BUIV.BigHealAnim();
    }

    void PlayerBigHeal()
    {
        BUIV.BigHeal();
    }
    
    void PlayerSanity()
    {
        BUIV.SanityRegen();
    }

    void PlayerSanityRegen()
    {
        BUIV.SanityRegenAnim();
    }

    void PlayerRockHit()
    {
        BUIV.TakeRockFallDamage();
    }
    
    void PlayerRockFall()
    {
        BUIV.RockFallAnim();
    }

    void FootR()
    {

    }

    void FootL()
    {
    }
}
