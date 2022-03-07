using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleUIVisuals : MonoBehaviour
{

    [SerializeField] BaseEntities enemy;
    [SerializeField] BaseEntities player;
    [SerializeField] PlaySpellAnim spellPlayer;

    public TurnBasedBattleSystem tbbs;

    public DisplayMoveDescription display;
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
        //If the turn is equal to the player then make blocking true
        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players && tbbs.playerIndex != 0 && blocking == true)
        {
            spellPlayer.anim.SetBool("isBlocking", true);
            Debug.Log("Blocking is true whilst not the players turn");
        }
        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players && tbbs.playerIndex == 0 && blocking == true)
        {
            //If the turn returns back to 0 and the player is blocking turn off the blocking state
            spellPlayer.anim.SetBool("isBlocking", false);
            spellPlayer.anim.Play("Idle");
            blocking = false;
            player.isBlocking = false;
            Debug.Log("Players Turn Now player block is now: " + player.isBlocking);
        }

        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move

        //If the player is attacking set the player to move to the enemy position and attack the enemy
        if (isAttacking == true)
        {
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, enemy.gameObject.transform.position, step);
            if (Vector3.Distance(player.gameObject.transform.position, enemy.gameObject.transform.position) > 3f)
            {
                spellPlayer.anim.SetBool("isWalking", true);
            }
               
            // If the player has reached the enemy position, then play the attack animation
            if (Vector3.Distance(player.gameObject.transform.position, enemy.gameObject.transform.position) <= 3f)
            {
                spellPlayer.anim.SetBool("isWalking", false);
                spellPlayer.anim.Play("DWAttack");
                
            }
        } 

        //Once the player has attacked start returning to the original position
        if (isAttacking == false && playerTurn == true)
        {
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, tbbs.player1Target.gameObject.transform.position, step);
            if (Vector3.Distance(player.gameObject.transform.position, tbbs.player1Target.gameObject.transform.position) > 5f)
            {
                spellPlayer.anim.SetBool("isWalking", true);
            }

            //Once the player has reached his original position end their turn
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


    //Block function (allows the player to take a blocking stance and ends their turn)
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

    //Allows the player to check their list of spell moves
    public void Spell()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(true);
        commandsUI.SetActive(false);
        spellUIMenu = true;
    }


    //Allows the player to look at the current commands of his team mates
    public void Command()
    {
        commandsUI.SetActive(true);
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
    }

    //Allows the player to go back in a menu
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
            display.OnClickVanish();

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
        Debug.Log("You Tried To Escape");
        SceneManager.LoadScene(0);
        
    }
}
