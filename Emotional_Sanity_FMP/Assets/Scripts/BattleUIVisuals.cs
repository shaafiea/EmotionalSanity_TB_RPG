using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIVisuals : MonoBehaviour
{

    [SerializeField] BaseEntities enemy;
    [SerializeField] BaseEntities player;
    [SerializeField] PlaySpellAnim spellPlayer;

    public TurnBasedBattleSystem tbbs;

    //UI    
    public GameObject commandsUI;
    public GameObject spellsUI;
    public GameObject fireVFX;
    public GameObject playerUI;

    //Spells
    bool spellUIMenu = false;
    bool spellUsed = false;
    float spellLifetime;

    //PlayerBools
    bool isAttacking = false;
    bool playerTurn = false;
    bool blocking = false;

    // Adjust the speed for the application.
    public float speed = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        tbbs = GameObject.Find("TBBSystem").GetComponent<TurnBasedBattleSystem>();
        player = GameObject.Find("NinjaWarrior").GetComponent<BaseEntities>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<BaseEntities>();
        spellPlayer = GameObject.Find("NinjaWarrior").GetComponentInChildren<PlaySpellAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players && tbbs.playerIndex != 0 && blocking == true)
        {
            spellPlayer.anim.SetBool("isBlocking", true);
            Debug.Log("Blocking is true whilst not the players turn");
        }
        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players && tbbs.playerIndex == 0 && blocking == true)
        {
            spellPlayer.anim.SetBool("isBlocking", false);
            spellPlayer.anim.Play("Idle");
            blocking = false;
            player.isBlocking = false;
            Debug.Log("Players Turn Now player block is now: " + player.isBlocking);
        }

        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move

        if (isAttacking == true)
        {
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, enemy.gameObject.transform.position, step);
            if (Vector3.Distance(player.gameObject.transform.position, enemy.gameObject.transform.position) > 3f)
            {
                spellPlayer.anim.SetBool("isWalking", true);
            }
               
            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(player.gameObject.transform.position, enemy.gameObject.transform.position) <= 3f)
            {
                spellPlayer.anim.SetBool("isWalking", false);
                spellPlayer.anim.Play("DWAttack");
                
            }
        } 

        if (isAttacking == false && playerTurn == true)
        {
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, tbbs.player1Target.gameObject.transform.position, step);
            if (Vector3.Distance(player.gameObject.transform.position, tbbs.player1Target.gameObject.transform.position) > 5f)
            {
                spellPlayer.anim.SetBool("isWalking", true);
            }

            if (player.gameObject.transform.position == tbbs.player1Target.gameObject.transform.position)
            {
                spellPlayer.anim.SetBool("isWalking", false);
                playerTurn = false;
                spellPlayer.anim.Play("Idle");
                EndTurnAfterAnim();
            }
            
        }
    }

    //Basic Attacking Scripts to test damage
    public void Attack()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        isAttacking = true;
        playerTurn = true;
        Debug.Log("Enemy Weapon Damage Taken: " + (player.damage, player.weaponstrength));
    }

    public void Block()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        blocking = true;
        player.isBlocking = true;
        Debug.Log(player.isBlocking);
        tbbs.EndPlayerTurn();
        
    }

    public void Spell()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(true);
        commandsUI.SetActive(false);
        spellUIMenu = true;
    }

    public void Command()
    {
        commandsUI.SetActive(true);
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
    }

    public void GoBack()
    {
        playerUI.SetActive(true);
        commandsUI.SetActive(false);
        spellsUI.SetActive(false);
        spellUIMenu = false;
    }

    public void PlayFireSkillAnim()
    {
        Debug.Log("Player MP Before Spell: " + player.MP);
        Debug.Log("Spell MP: " + player.spellmoves[0].mpUsed);
        enemy.TakeSpecialDamage(enemy.entityWeakness[0], (player.spellmoves[0].damage), player.manastrength);
        player.MP = player.MP - player.spellmoves[0].mpUsed;
        Debug.Log("PlayerMP After Spell: " + player.MP);
        player.SP = player.SP + player.spellmoves[0].spUsed;
        Debug.Log("Damage Done: " + player.spellmoves[0].damage * player.manastrength / enemy.manadefence);
        Debug.Log("Enemy Has taken a " + player.spellmoves[0] + " It hurt!");
        fireVFX = Instantiate(fireVFX, enemy.transform.position, enemy.transform.rotation);
        spellPlayer.anim.SetBool("spellUsed", false);
    }

    public void AttackDamageDealt()
    {
        //isAttacking = false;
        enemy.TakeWeaponDamage(player.damage, player.weaponstrength);
    }

    public void PlayFireSpellVFX()
    {

        if (player.MP >= player.spellmoves[0].mpUsed)
        {
            //playerUI.SetActive(false);
            //spellsUI.SetActive(false);
            //spellUIMenu = false;
            spellPlayer.anim.SetBool("spellUsed", true);
            spellsUI.SetActive(false);

        } else
        {

            GoBack();
        }
        //spellLifetime = 3;
        //Destroy(fireVFX, 3);

    }

    public void EndTurnAfterAnim()
    {
        tbbs.EndPlayerTurn();
    }

    public void WalkBack()
    {
        Debug.Log("Walk To Original Position");
        isAttacking = false;
        spellPlayer.anim.Play("Walk");
    }

    public void Escape()
    {

    }
}
