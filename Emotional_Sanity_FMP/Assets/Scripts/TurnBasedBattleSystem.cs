using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TurnBasedBattleSystem : MonoBehaviour
{
    /*    public GameObject playerUI;
        TeamAIController player2;
        TeamAIController player3;
        TeamAIController player4;

        //Enemy and Teammate AIs
        [SerializeField] BaseEntities enemy;
        [SerializeField] BaseEntities player2stats;
        [SerializeField] BaseEntities player3stats;
        [SerializeField] BaseEntities player4stats;*/

    public GameManager gameManager;

    public TrackSwitcher trackswitch;
    public enum turns 
    {
        players,
        enemies
    }
    public List<GameObject> players;
    public List<GameObject> enemies;

    public int playerIndex;
    public int enemyIndex;
    public turns currentTurns;
    private bool uiOff;

    //Player
    public BaseEntities player1;
    public BaseEntities player2;
    public BaseEntities player3;
    public BaseEntities player4;

    //Team AI Animators
    public Animator k_anim;
    public Animator s_anim;
    public Animator b_anim;


    //Player Targets
    public GameObject player1Target;
    public GameObject player2Target;
    public GameObject player3Target;
    public GameObject player4Target;

    public BattleUIVisuals bui;

    //Teammate States
    public TeamAIController p2_State;
    public TeamAIController p3_State;
    public TeamAIController p4_State;

    //State Texts
    public TextMeshProUGUI p2_Text_State;
    public TextMeshProUGUI p3_Text_State;
    public TextMeshProUGUI p4_Text_State;

    //Enemy
    public BaseEntities enemy1;
    public BaseEntities enemy2;
    public BaseEntities enemy3;

    //Enemy Team States
    public EnemyAI e1_state;
    public EnemyAI e2_state;
    public EnemyAI e3_state;


    //EnemyAnimators
    public Animator e1_anim;
    public Animator e2_anim;
    public Animator e3_anim;
    

    //Enemy Targets
    public GameObject enemy1Target;
    public GameObject enemy2Target;
    public GameObject enemy3Target;

    public DisplayMoves dpMove;

    //Timer 
    public float getmoving_timer;

    // Adjust the speed for the application.
    public float speed = 8.0f;

    //Attacking States
    public bool p2_isAttacking = false;
    public bool p3_isAttacking = false;
    public bool p4_isAttacking = false;

    public bool p2_turn = false;
    public bool p3_turn = false;
    public bool p4_turn = false;

    //Attacking States
    public bool e1_isAttacking = false;
    public bool e2_isAttacking = false;
    public bool e3_isAttacking = false;

    public bool e1_turn = false;
    public bool e2_turn = false;
    public bool e3_turn = false;

    //Dead States
    public bool isP2_Alive = true;
    public bool isP3_Alive = true;
    public bool isP4_Alive = true;

    public bool isE1_Alive = true;
    public bool isE2_Alive = true;
    public bool isE3_Alive = true;

    public int enemiesfighting;
    public int enemiesdead;

    //Targeted State (to stop target from being repeated)
    public bool isTargeting = false;

    //enemyrandomstate
    public int randomrange;
    public int m_randomrange;
    public int h_randomrange;
    public int a_randomrange;
    public int fb_randomrange;
    public bool enemyTD = false;

    public int t_randomrange;
    public bool teamTD = true;

    public int randomrangeacc;

    public bool t1_newState = false;
    public bool t2_newState = false;
    public bool t3_newState = false;
    public bool t4_newState = false;

    public bool e1_newState = false;
    public bool e2_newState = false;
    public bool e3_newState = false;


    //Spell Used (To stop from having 2 turns)
    public bool p2_spell = false;
    public bool p3_spell = false;
    public bool p4_spell = false;

    public bool e1_spell = false;
    public bool e2_spell = false;
    public bool e3_spell = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player1 = GameObject.Find("NinjaWarrior").GetComponent<BaseEntities>();
        player2 = GameObject.Find("Karate").GetComponent<BaseEntities>();
        player3 = GameObject.Find("SorceressWarrior").GetComponent<BaseEntities>();
        player4 = GameObject.Find("Brute").GetComponent<BaseEntities>();

        p2_State = GameObject.Find("Karate").GetComponent<TeamAIController>();
        p3_State = GameObject.Find("SorceressWarrior").GetComponent<TeamAIController>();
        p4_State = GameObject.Find("Brute").GetComponent<TeamAIController>();

        k_anim = GameObject.Find("Karate").GetComponent<Animator>();
        s_anim = GameObject.Find("SorceressWarrior").GetComponent<Animator>();
        b_anim = GameObject.Find("Brute").GetComponent<Animator>();

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies.Count == 1)
            {
                Debug.Log("Taking On 1 Enemy");
                enemy1 = GameObject.FindWithTag("Enemy1").GetComponent<BaseEntities>();
                e1_anim = GameObject.FindWithTag("Enemy1").GetComponent<Animator>();
                e1_state = GameObject.FindWithTag("Enemy1").GetComponent<EnemyAI>();

                e1_anim.SetBool("isE1_Alive", true);
                enemiesfighting = 1;
            }
            else if (enemies.Count == 2)
            {
                Debug.Log("Taking On 2 Enemies");
                enemy1 = GameObject.FindWithTag("Enemy1").GetComponent<BaseEntities>();
                enemy2 = GameObject.FindWithTag("Enemy2").GetComponent<BaseEntities>();

                e1_state = GameObject.FindWithTag("Enemy1").GetComponent<EnemyAI>();
                e2_state = GameObject.FindWithTag("Enemy2").GetComponent<EnemyAI>();

                e1_anim = GameObject.FindWithTag("Enemy1").GetComponent<Animator>();
                e2_anim = GameObject.FindWithTag("Enemy2").GetComponent<Animator>();

                e1_anim.SetBool("isE1_Alive", true);
                e2_anim.SetBool("isE2_Alive", true);
                enemiesfighting = 2;
            }
            else if (enemies.Count == 3)
            {
                Debug.Log("Taking On 3 Enemies");
                enemy1 = GameObject.FindWithTag("Enemy1").GetComponent<BaseEntities>();
                enemy2 = GameObject.FindWithTag("Enemy2").GetComponent<BaseEntities>();
                enemy3 = GameObject.FindWithTag("Enemy3").GetComponent<BaseEntities>();

                e1_state = GameObject.FindWithTag("Enemy1").GetComponent<EnemyAI>();
                e2_state = GameObject.FindWithTag("Enemy2").GetComponent<EnemyAI>();
                e3_state = GameObject.FindWithTag("Enemy3").GetComponent<EnemyAI>();

                e1_anim = GameObject.FindWithTag("Enemy1").GetComponent<Animator>();
                e2_anim = GameObject.FindWithTag("Enemy2").GetComponent<Animator>();
                e3_anim = GameObject.FindWithTag("Enemy3").GetComponent<Animator>();

                e1_anim.SetBool("isE1_Alive", true);
                e2_anim.SetBool("isE2_Alive", true);
                e3_anim.SetBool("isE3_Alive", true);
                enemiesfighting = 3;
            }
        }

        //When the battle scene starts automatically set the teammate states to attack
        p2_State.state = TeamAIController.AIState.Attack;
        p3_State.state = TeamAIController.AIState.Attack;
        p4_State.state = TeamAIController.AIState.Attack;

        p2_Text_State.text = "Attack";
        p3_Text_State.text = "Attack";
        p4_Text_State.text = "Attack";

        /* p2_Text_State = GameObject.Find("Player2Commands").GetComponentInChildren<TextMeshProUGUI>();
           p3_Text_State = GameObject.Find("Player3Commands").GetComponentInChildren<TextMeshProUGUI>();
           p4_Text_State = GameObject.Find("Player4Commands").GetComponentInChildren<TextMeshProUGUI>();*/

        //Insert all players into the list in this specific order
        players.Insert(0, player1.gameObject);
        players.Insert(1, player2.gameObject);
        players.Insert(2, player3.gameObject);
        players.Insert(3, player4.gameObject);

        //Find Enemies stats and then insert that gameObject into the enemyList
        /*     enemy1 = GameObject.FindWithTag("Enemy").GetComponent<BaseEntities>();
             enemies.Insert(0, enemy1.gameObject);*/

        dpMove = GameObject.Find("Battle Info Text (TMP)").GetComponent<DisplayMoves>();
        bui = GameObject.Find("BattleUIManager").GetComponent<BattleUIVisuals>();
        uiOff = true;
        isTargeting = true;
        p2_isAttacking = true;
        p3_isAttacking = true;
        p4_isAttacking = true;
        e1_isAttacking = true;
        e2_isAttacking = true;
        e3_isAttacking = true;
        e1_spell = false;
        e2_spell = false;
        e3_spell = false;

        if (enemy1.entityName == "Terrorbringer")
        {
            enemy1.SP = enemy1.maxSP;
        }

        getmoving_timer = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //Turn Based System
        //If the turn is equal to the players turns go through each player in the list

        //These variables were created to fix the bug of being able to not remove from the list instantly during an animation
        var enemiesToKill = new List<GameObject>();
        var teamToKill = new List<GameObject>();

        //Instant Swap Screen if Player Dies
        if (player1.HP <= 0)
        {
            SceneManager.LoadScene(8);
        }

        //Always plays in the background just in case a freeze happens skip the turn of the frozen player
        getmoving_timer -= Time.deltaTime;
        if (getmoving_timer <= 0 && (currentTurns == turns.players))
        {
            EndPlayerTurn();
            getmoving_timer = 60;
        } else if (getmoving_timer <= 0 && currentTurns == turns.enemies)
        {
            EndEnemyTurn();
            getmoving_timer = 60;
        }

        //Turn off ui if the player isnt moving
        if (playerIndex != 0)
        {
            bui.playerUI.SetActive(false);
            bui.commandsUI.SetActive(false);
            bui.spellsUI.SetActive(false);
            bui.targetUI.SetActive(false);
            bui.spellTargetUI.SetActive(false);
            bui.teamTargetUI.SetActive(false);
            bui.tipsUI.SetActive(false);
            bui.itemPickerUI.SetActive(false);
            bui.teamtarget2UI.SetActive(false);
            bui.timer.gameObject.SetActive(false);
        }


        for (int i = 0; i < players.Count; i++)
        {
            if (currentTurns == turns.players)
            {
                e1_isAttacking = true;
                e2_isAttacking = true;
                e3_isAttacking = true;

                if (players[i].GetComponent<BaseEntities>().entityName == "NinjaWarrior" && playerIndex == i) 
                {
                    Debug.Log("Player 1 Turn");
                    //If the player isnt dead then allow them to fight in battle
                    if (player1.HP > 0)
                    {
                        if (t1_newState == true)
                        {
                            Debug.Log("Teammate 1 is attacking");
                            teamTD = true;
                            RandomTeamState();
                            Debug.Log(player2.gameObject + " " + t_randomrange);
                            t1_newState = false;
                        }

                        if (uiOff == true)
                        {
                            //Activate the UI
                            bui.playerUI.SetActive(true);
                            teamTD = true;
                            uiOff = false;
                        }
                    }
                    else
                    {
                        //If the player is dead then move on to the next player
                        bui.playerUI.SetActive(false);
                        SceneManager.LoadScene(8);
                    }
                 
                }

                //If the player (Karate) in the list is the same as the player index variable then they are able to attack
                if (players[i].GetComponent<BaseEntities>().entityName == "Karate" && playerIndex == i)
                {
                    //They can only do attacks if their HP is above 0
                    p2_turn = true;
                    if (player2.HP > 0)
                    {

                        if (t2_newState == true)
                        {
                            getmoving_timer = 15;
                            uiOff = true;
                            Debug.Log("Teammate 2 is attacking");
                            teamTD = true;
                            RandomTeamState();
                            Debug.Log(player2.gameObject + " " + t_randomrange);
                            t2_newState = false;
                        }
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move
                        Debug.Log("Player 2 Turn");

                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            p2_State.AITarget();
                            isTargeting = false;
                        }

                        //Depending on what state the AI is in do that specific command
                        if (p2_State.state == TeamAIController.AIState.Attack || (p2_State.state == TeamAIController.AIState.Random && t_randomrange == 0) && p2_turn == true)
                        {
                            //If the player attacking state is true then play their attack
                            if (p2_isAttacking == true)
                            {
                                Debug.Log("Karate is targeting " + p2_State.target);
                                player2.gameObject.transform.position = Vector3.MoveTowards(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position, step);
                                if (Vector3.Distance(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position) > 5f)
                                {
                                    dpMove.DisplayMove(player2.entityName, "Attacks", p2_State.target.entityName);
                                    k_anim.SetBool("isWalking", true);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position) <= 2f)
                                {
                                    speed = 0f;
                                    k_anim.SetBool("isWalking", false);
                                    k_anim.Play("Attack");
                                }

                            }
                        }

                        //Once the player has attacked start returning to the original position
                        if (p2_isAttacking == false && p2_turn == true)
                        {
                            speed = 8.0f;
                            Debug.Log("WalkBack");
                            player2.gameObject.transform.position = Vector3.MoveTowards(player2.gameObject.transform.position, player2Target.gameObject.transform.position, step);
                            if (Vector3.Distance(player2.gameObject.transform.position, player2Target.gameObject.transform.position) > 6f)
                            {
                                k_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (player2.gameObject.transform.position == player2Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                k_anim.SetBool("isWalking", false);
                                p2_turn = false;
                                k_anim.Play("Idle");
                                isTargeting = true;
                                teamTD = true;
                                p3_isAttacking = true;
                                EndPlayerTurn();
                            }

                            uiOff = true;
                        }

                        if (p2_State.state == TeamAIController.AIState.Block || (p2_State.state == TeamAIController.AIState.Random && t_randomrange == 1) && p2_turn == true)
                        {
                            if (player2.HP > 0)
                            {
                                dpMove.BlockDisplay(player2.entityName);
                                k_anim.SetBool("isBlocking", true);
                                players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                                isTargeting = true;
                                p3_isAttacking = true;
                                teamTD = true;
                                uiOff = true;
                            }
                        }

                        if (p2_State.state == TeamAIController.AIState.Spell || (p2_State.state == TeamAIController.AIState.Random && t_randomrange == 2) && p2_turn == true)
                        {
                            if (player2.HP > 0 && player2.MP >= player2.spellmoves[0].mpUsed)
                            {
                                p2_spell = true;
                                if (enemiesfighting == 1)
                                {
                                    for (int j = 0; j < player2.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player2.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p2_State.target = enemy1;
                                        }
                                        break;
                                    }
                                }

                                if (enemiesfighting == 2)
                                {
                                    for (int j = 0; j < player2.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player2.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p2_State.target = enemy1;
                                        }
                                        else if (player2.entityType[j] == enemy2.entityWeakness[0] && enemy2.HP > 0 && enemiesfighting >= 2)
                                        {
                                            p2_State.target = enemy2;
                                        }
                                        break;
                                    }
                                }

                                if (enemiesfighting == 3)
                                {
                                    for (int j = 0; j < player2.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player2.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p2_State.target = enemy1;
                                        }
                                        else if (player2.entityType[j] == enemy2.entityWeakness[0] && enemy2.HP > 0 && enemiesfighting >= 2)
                                        {
                                            p2_State.target = enemy2;
                                        }
                                        else if (player2.entityType[j] == enemy3.entityWeakness[0] && enemy3.HP > 0 && enemiesfighting >= 3)
                                        {
                                            p2_State.target = enemy3;
                                        }
                                        break;
                                    }
                                }
                                dpMove.DisplaySpellMove(player2.entityName, "is using", player2.spellmoves[0].name, p2_State.target.entityName);
                                k_anim.SetBool("isSpelling", true);
                                p3_isAttacking = true;
                                uiOff = true;
                            } else if (player2.HP > 0 && player2.MP < player2.spellmoves[0].mpUsed && p2_spell == false)
                            {
                                p2_State.state = TeamAIController.AIState.Random;
                                p2_Text_State.text = "Random";
                                t2_newState = true;
                            }
                        }

                        if (p2_State.state == TeamAIController.AIState.SanityRegen || (p2_State.state == TeamAIController.AIState.Random && t_randomrange == 3) && p2_turn == true)
                        {
                            if (player2.HP > 0)
                            {
                                dpMove.SanityRegenDisplay(player2.entityName);
                                k_anim.SetBool("isSanityRegen", true);
                                p3_isAttacking = true;
                                uiOff = true;
                            } else
                            {
                                p2_State.state = TeamAIController.AIState.Random;
                                p2_Text_State.text = "Random";
                                t2_newState = true;
                            }
                        }
                    }
                    else
                    {
                        EndPlayerTurn();
                        //players.Remove(players[1]);
                    }
                }

                if (players[i].GetComponent<BaseEntities>().entityName == "SorceressWarrior" && playerIndex == i)
                {
                    p3_turn = true;
                    if (player3.HP > 0)
                    {
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move
                        //Debug.Log("Player 2 Turn");
                        if (t3_newState == true)
                        {
                            getmoving_timer = 15;
                            uiOff = true;
                            p3_State.n_randomrange = Random.Range(0, 3);
                            teamTD = true;
                            Debug.Log("Teammate 3 is attacking");
                            RandomTeamState();
                            Debug.Log(player3.gameObject + " " + t_randomrange);
                            t3_newState = false;
                        }

                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            p3_State.AITarget();
                            isTargeting = false;
                        }
                        //Depending on what state the AI is in do that specific command
                        if (p3_State.state == TeamAIController.AIState.Attack || (p3_State.state == TeamAIController.AIState.Random && t_randomrange == 0) && p3_turn == true)
                        {
                            //If the player attacking state is true then play their attack
                            if (p3_isAttacking == true)
                            {
                                //Debug.Log("Karate is targeting " + p3_State.target);
                                player3.gameObject.transform.position = Vector3.MoveTowards(player3.gameObject.transform.position, p3_State.target.gameObject.transform.position, step);
                                if (Vector3.Distance(player3.gameObject.transform.position, p3_State.target.gameObject.transform.position) > 5f)
                                {
                                    dpMove.DisplayMove(player3.entityName, "Attacks", p3_State.target.entityName);
                                    s_anim.SetBool("isWalking", true);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(player3.gameObject.transform.position, p3_State.target.gameObject.transform.position) <= 2f)
                                {
                                    speed = 0f;
                                    s_anim.SetBool("isWalking", false);
                                    s_anim.Play("Attack");

                                }

                            }
                        }

                        //Once the player has attacked start returning to the original position
                        if (p3_isAttacking == false && p3_turn == true)
                        {
                            speed = 8.0f;
                            Debug.Log("WalkBack");
                            player3.gameObject.transform.position = Vector3.MoveTowards(player3.gameObject.transform.position, player3Target.gameObject.transform.position, step);
                            if (Vector3.Distance(player3.gameObject.transform.position, player3Target.gameObject.transform.position) > 6f)
                            {
                                s_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (player3.gameObject.transform.position == player3Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                s_anim.SetBool("isWalking", false);
                                p3_turn = false;
                                s_anim.Play("Idle");
                                isTargeting = true;
                                teamTD = true;
                                p4_isAttacking = true;
                                EndPlayerTurn();
                            }

                            uiOff = true;
                        }

                        if (p3_State.state == TeamAIController.AIState.Block || (p3_State.state == TeamAIController.AIState.Random && t_randomrange == 1) && p3_turn == true)
                        {
                            if (player3.HP > 0)
                            {
                                dpMove.BlockDisplay(player3.entityName);
                                s_anim.SetBool("isBlocking", true);
                                players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                                isTargeting = true;
                                p4_isAttacking = true;
                                teamTD = true;
                                uiOff = true;
                            }
                        }

                        if (p3_State.state == TeamAIController.AIState.Spell || (p3_State.state == TeamAIController.AIState.Random && t_randomrange == 2) && p3_turn == true)
                        {
                            if (player3.HP > 0 && player3.MP >= player3.spellmoves[0].mpUsed)
                            {
                                p3_spell = true;
                                if (enemiesfighting == 1)
                                {
                                    for (int j = 0; j < player3.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player3.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p3_State.target = enemy1;
                                        }
                                        break;
                                    }
                                }

                                if (enemiesfighting == 2)
                                {
                                    for (int j = 0; j < player3.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player3.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p3_State.target = enemy1;
                                        }
                                        else if (player3.entityType[j] == enemy2.entityWeakness[0] && enemy2.HP > 0)
                                        {
                                            p3_State.target = enemy2;
                                        }
                                        break;
                                    }
                                }

                                if (enemiesfighting == 3)
                                {
                                    for (int j = 0; j < player3.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player3.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p3_State.target = enemy1;
                                        }
                                        else if (player3.entityType[j] == enemy2.entityWeakness[0] && enemy2.HP > 0)
                                        {
                                            p3_State.target = enemy2;
                                        }
                                        else if (player3.entityType[j] == enemy3.entityWeakness[0] && enemy3.HP > 0)
                                        {
                                            p3_State.target = enemy3;
                                        }
                                        break;
                                    }
                                }
                                dpMove.DisplaySpellMove(player3.entityName, "is using", player3.spellmoves[0].name, p3_State.target.entityName);
                                s_anim.SetBool("isSpelling", true);
                                p4_isAttacking = true;
                                uiOff = true;
                            } else if (player3.HP > 0 && player3.MP < player3.spellmoves[0].mpUsed && p3_spell == false)
                            {
                                p3_State.state = TeamAIController.AIState.Random;
                                p3_Text_State.text = "Random";
                                t3_newState = true;
                            }
                        }

                        if (p3_State.state == TeamAIController.AIState.Heal && p3_turn == true)
                        {
                            if (player3.HP > 0 && player3.MP >= player3.spellmoves[1].mpUsed)
                            {
                                p3_spell = true;
                                s_anim.SetBool("isHealing", true);
                                isTargeting = true;
                                uiOff = true;
                            } else if (player3.HP > 0 && player3.MP < player3.spellmoves[1].mpUsed && p3_spell == false)
                            {
                                p3_State.state = TeamAIController.AIState.Random;
                                p3_Text_State.text = "Random";
                                t3_newState = true; ;
                            }
                        }

                        if (p3_State.state == TeamAIController.AIState.BigHeal && p3_turn == true)
                        {
                            if (player3.HP > 0 && player3.MP >= player3.spellmoves[2].mpUsed)
                            {
                                p3_spell = true;
                                dpMove.HeallAllDisplay(player3.entityName);
                                s_anim.SetBool("isBigHealing", true);
                                isTargeting = true;
                                uiOff = true;
                            } else if (player3.HP > 0 && player3.MP < player3.spellmoves[2].mpUsed && p3_spell == false)
                            {
                                p3_State.state = TeamAIController.AIState.Random;
                                p3_Text_State.text = "Random";
                                t2_newState = true;
                            }
                        }

                        if (p3_State.state == TeamAIController.AIState.SanityRegen || (p3_State.state == TeamAIController.AIState.Random && t_randomrange == 3) && p3_turn == true)
                        {
                            if (player3.HP > 0)
                            {
                                dpMove.SanityRegenDisplay(player3.entityName);
                                s_anim.SetBool("isSanityRegen", true);
                                p4_isAttacking = true;
                                uiOff = true;
                            }
                            else
                            {
                                p3_State.state = TeamAIController.AIState.Random;
                                p3_Text_State.text = "Random";
                                t2_newState = true;
                            }
                        }
                    }
                    else
                    {
                        EndPlayerTurn();
                        players.Remove(players[i]);
                    }
                }

                if (players[i].GetComponent<BaseEntities>().entityName == "Brute" && playerIndex == i)
                {
                    p4_turn = true;
                    if (player4.HP > 0)
                    {
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move
                                                             //Debug.Log("Player 2 Turn");
                        if (t4_newState == true)
                        {
                            getmoving_timer = 15;
                            uiOff = true;
                            teamTD = true;
                            Debug.Log("Teammate 4 is attacking");
                            RandomTeamState();
                            Debug.Log(player4.gameObject + " " + t_randomrange);
                            t4_newState = false;
                        }

                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            p4_State.AITarget();
                            isTargeting = false;
                        }

                        //Depending on what state the AI is in do that specific command
                        if (p4_State.state == TeamAIController.AIState.Attack || (p4_State.state == TeamAIController.AIState.Random && t_randomrange == 0) && p4_turn == true)
                        {

                            //If the player attacking state is true then play their attack
                            if (p4_isAttacking == true)
                            {
                                Debug.Log("Karate is targeting " + p4_State.target);
                                player4.gameObject.transform.position = Vector3.MoveTowards(player4.gameObject.transform.position, p4_State.target.gameObject.transform.position, step);
                                if (Vector3.Distance(player4.gameObject.transform.position, p4_State.target.gameObject.transform.position) > 5f)
                                {
                                    dpMove.DisplayMove(player4.entityName, "Attacks", p4_State.target.entityName);
                                    b_anim.SetBool("isWalking", true);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(player4.gameObject.transform.position, p4_State.target.gameObject.transform.position) <= 2f)
                                {
                                    speed = 0f;
                                    b_anim.SetBool("isWalking", false);
                                    b_anim.Play("Attack");

                                }

                            }
                        }

                        //Once the player has attacked start returning to the original position
                        if (p4_isAttacking == false && p4_turn == true)
                        {
                            speed = 8.0f;
                            Debug.Log("WalkBack");
                            player4.gameObject.transform.position = Vector3.MoveTowards(player4.gameObject.transform.position, player4Target.gameObject.transform.position, step);
                            if (Vector3.Distance(player4.gameObject.transform.position, player4Target.gameObject.transform.position) > 6f)
                            {
                                b_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (player4.gameObject.transform.position == player4Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                b_anim.SetBool("isWalking", false);
                                p4_turn = false;
                                b_anim.Play("Idle");
                                isTargeting = true;
                                teamTD = true;
                                EndPlayerTurn();
                            }

                            uiOff = true;
                        }

                        if (p4_State.state == TeamAIController.AIState.Block || (p4_State.state == TeamAIController.AIState.Random && t_randomrange == 1) && p4_turn == true)
                        {
                            if (player4.HP > 0)
                            {
                                dpMove.BlockDisplay(player4.entityName);
                                b_anim.SetBool("isBlocking", true);
                                players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                                teamTD = true;
                                uiOff = true;
                            }
                        }

                        if (p4_State.state == TeamAIController.AIState.Spell || (p4_State.state == TeamAIController.AIState.Random && t_randomrange == 2) && p4_turn == true)
                        {
                            if (player4.HP > 0 && player4.MP >= player4.spellmoves[0].mpUsed)
                            {
                                p4_spell = true;
                                if (enemiesfighting == 1)
                                {
                                    for (int j = 0; j < player4.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player4.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p4_State.target = enemy1;
                                        }
                                        for (int k = 0; k < player4.entityType.Count; k++)
                                        {
                                            //Priotize hitting enemy with a weakness otherwise hit at random
                                            if (player4.entityType[k] == enemy1.entityWeakness[1] && enemy1.HP > 0)
                                            {
                                                p4_State.target = enemy1;
                                            }
                                        }
                                        break;
                                    }
                                }

                                if (enemiesfighting == 2)
                                {
                                    for (int j = 0; j < player4.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player4.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p4_State.target = enemy1;
                                        }
                                        else if (player4.entityType[j] == enemy2.entityWeakness[0] && enemy2.HP > 0 && enemiesfighting >= 2)
                                        {
                                            p4_State.target = enemy2;
                                        }

                                        for (int k = 0; k < player4.entityType.Count; k++)
                                        {
                                            //Priotize hitting enemy with a weakness otherwise hit at random
                                            if (player4.entityType[k] == enemy1.entityWeakness[1] && enemy1.HP > 0)
                                            {
                                                p4_State.target = enemy1;
                                            }
                                            else if (player4.entityType[k] == enemy2.entityWeakness[1] && enemy2.HP > 0)
                                            {
                                                p4_State.target = enemy2;
                                            }
                                        }
                                        break;
                                    }
                                }

                                if (enemiesfighting == 3)
                                {
                                    for (int j = 0; j < player4.entityType.Count; j++)
                                    {
                                        //Priotize hitting enemy with a weakness otherwise hit at random
                                        if (player4.entityType[j] == enemy1.entityWeakness[0] && enemy1.HP > 0)
                                        {
                                            p4_State.target = enemy1;
                                        }
                                        else if (player4.entityType[j] == enemy2.entityWeakness[0] && enemy2.HP > 0 && enemiesfighting >= 2)
                                        {
                                            p4_State.target = enemy2;
                                        }
                                        else if (player3.entityType[j] == enemy3.entityWeakness[0] && enemy3.HP > 0 && enemiesfighting >= 3)
                                        {
                                            p4_State.target = enemy3;
                                        }

                                        for (int k = 0; k < player4.entityType.Count; k++)
                                        {
                                            //Priotize hitting enemy with a weakness otherwise hit at random
                                            if (player4.entityType[k] == enemy1.entityWeakness[1] && enemy1.HP > 0)
                                            {
                                                p4_State.target = enemy1;
                                            }
                                            else if (player4.entityType[k] == enemy2.entityWeakness[1] && enemy2.HP > 0)
                                            {
                                                p4_State.target = enemy2;
                                            }
                                            else if (player3.entityType[k] == enemy3.entityWeakness[1] && enemy3.HP > 0)
                                            {
                                                p4_State.target = enemy3;
                                            }
                                        }
                                        break;
                                    }
                                }
                                dpMove.DisplaySpellMove(player4.entityName, "is using", player4.spellmoves[0].name, p4_State.target.entityName);
                                b_anim.SetBool("isSpelling", true);
                                uiOff = true;
                            } else if (player4.HP > 0 && player4.MP < player4.spellmoves[0].mpUsed && p4_spell == false)
                            {
                                p4_State.state = TeamAIController.AIState.Random;
                                p4_Text_State.text = "Random";
                                t4_newState = true;
                            }
                        }

                        if (p4_State.state == TeamAIController.AIState.SanityRegen || (p4_State.state == TeamAIController.AIState.Random && t_randomrange == 3) && p4_turn == true)
                        {
                            if (player4.HP > 0)
                            {
                                dpMove.SanityRegenDisplay(player4.entityName);
                                b_anim.SetBool("isSanityRegen", true);
                                uiOff = true;
                            } else
                            {
                                p4_State.state = TeamAIController.AIState.Random;
                                p4_Text_State.text = "Random";
                                t4_newState = true;
                            }
                        }

                    }
                    else if (player4.HP <= 0)
                    {
                        EndPlayerTurn();
                        players.Remove(players[i]);
                    }
                }

            }
                if (players[i].GetComponent<BaseEntities>().enemyOrder == "P2" && player2.HP <= 0 && isP2_Alive)
            {
                isP2_Alive = false;
                teamToKill.Add(players[i]);
                bui.p2_Button.SetActive(false);
                bui.p2_Button_item.SetActive(false);
                k_anim.SetBool("isBlocking", false);
                k_anim.SetBool("isP2Alive", false);
                k_anim.Play("Death");
            }

            if (players[i].GetComponent<BaseEntities>().enemyOrder == "P3" && player3.HP <= 0 && isP3_Alive)
            {
                isP3_Alive = false;
                teamToKill.Add(players[i]);
                bui.p3_Button.SetActive(false);
                bui.p3_Button_item.SetActive(false);
                Debug.Log("P3 HAS DIED");
                s_anim.SetBool("isBlocking", false);
                s_anim.SetBool("isP3Alive", false);
                s_anim.Play("Death");
            }

            if (players[i].GetComponent<BaseEntities>().enemyOrder == "P4" && player4.HP <= 0 && isP4_Alive)
            {
                isP4_Alive = false;
                teamToKill.Add(players[i]);
                bui.p4_Button.SetActive(false);
                bui.p4_Button_item.SetActive(false);
                Debug.Log("P4 HAS DIED");
                b_anim.SetBool("isBlocking", false);
                b_anim.SetBool("isP4Alive", false);
                b_anim.Play("Death");
            }
        }

        //Once added to the list remove the player instantly
        for (int i = 0; i < teamToKill.Count; i++)
        {
            players.Remove(teamToKill[i]);
        }
        teamToKill.Clear();


        for (int i = 0; i < enemies.Count; i++)
        {

            if (currentTurns == turns.enemies) //Once all players have done their moves make it so it is the enemies turn.
            {

////////////////////////////////////////////////////////// ENEMY 1 (FIRST SPOT) Level 1 Boss //////////////////////////////////////////////
                    if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Grass)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && enemyIndex == i)
                    {
                        e1_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy1.HP > 0)
                        {

                            if (e1_newState == true)
                            {
                                uiOff = true;
                                Debug.Log("ENEMY 1 DECIDING");
                                EnemyState();
                                e1_newState = false;
                            }
                            //If the player hasnt decided on a target yet then decide on a target at random
                            if (isTargeting == true)
                            {
                                randomrangeacc = Random.Range(0, 100);
                                Debug.Log("Enemy 1 " + randomrange);
                                e1_state.AITarget();
                                isTargeting = false;
                            }

                            if ((randomrange == 0 || (randomrange == 2 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || randomrange == 4 || randomrange == 8) && e1_turn == true && e1_spell == false)
                            {
                                if (e1_isAttacking == true)
                                {

                                    enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) > 5f)
                                    {
                                        speed = 7.0f;
                                        dpMove.DisplayMove(enemy1.entityName, "Attacks", e1_state.target.entityName);
                                        e1_anim.SetBool("isWalking", true);
                                        Debug.Log("ENEMY TARGETING.... " + e1_state.target);
                                    }

                                    // If the player has reached the enemy position, then play the attack animation
                                    if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) <= 1f)
                                    {
                                        speed = 0f;
                                        e1_anim.SetBool("isWalking", false);
                                        e1_anim.Play("Attack");
                                    }
                                }

                            }

                            if (e1_isAttacking == false && e1_turn == true)
                            {
                                speed = 7.0f;
                                Debug.Log("WalkBack");
                                enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position) > 6f)
                                {
                                    e1_anim.SetBool("isWalking", true);
                                }

                                //Once the player has reached his original position end their turn
                                if (enemy1.gameObject.transform.position == enemy1Target.gameObject.transform.position)
                                {
                                    Debug.Log("Im back to my positon!");
                                    e1_anim.SetBool("isWalking", false);
                                    e1_turn = false;
                                    e1_anim.Play("Idle");
                                    isTargeting = true;
                                    EndEnemyTurn();
                                }

                            uiOff = true;
                            
                        }
                        if ((randomrange == 1 || randomrange == 3 || randomrange == 5 || (randomrange == 6 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || randomrange == 7) && e1_turn == true && e1_spell == false)
                        {
                            if (enemy1.HP > 0) 
                            {
                                dpMove.BlockDisplay(enemy1.entityName);
                                Debug.Log("Enemy 1 Block");
                                e1_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((randomrange == 2 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (randomrange == 6 && enemy1.MP >= enemy1.spellmoves[0].mpUsed)) && e1_turn == true && e1_spell == false)
                        {
                            e1_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy1.entityName, "is using", enemy1.spellmoves[0].name, e1_state.target.entityName);
                            e1_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                    }
                    else if (enemy1.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }
                }
                /////////////////////////////////////////Level 2 Boss/////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Golem(Boss)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && enemyIndex == i)
                {
                    e1_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy1.HP > 0)
                    {

                        if (e1_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 1 DECIDING");
                            MediumEnemyState();
                            e1_newState = false;
                        }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 1 " + m_randomrange);
                            e1_state.AITarget();
                            isTargeting = false;
                        }

                        if ((m_randomrange == 0 || (m_randomrange == 2 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || m_randomrange == 4 || m_randomrange == 8 || m_randomrange == 11) && e1_turn == true && e1_spell == false)
                        {
                            if (e1_isAttacking == true)
                            {

                                enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy1.entityName, "Attacks", e1_state.target.entityName);
                                    e1_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e1_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) <= 1f)
                                {
                                    speed = 0f;
                                    e1_anim.SetBool("isWalking", false);
                                    e1_anim.Play("Attack");
                                }
                            }

                        }

                        if (e1_isAttacking == false && e1_turn == true)
                        {
                            speed = 7.0f;
                            Debug.Log("WalkBack");
                            enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position) > 6f)
                            {
                                e1_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy1.gameObject.transform.position == enemy1Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e1_anim.SetBool("isWalking", false);
                                e1_turn = false;
                                e1_anim.Play("Idle");
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;

                        }
                        if ((m_randomrange == 1 || m_randomrange == 3 || m_randomrange == 5 || (m_randomrange == 6 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || m_randomrange == 12) && e1_turn == true && e1_spell == false)
                        {
                            if (enemy1.HP > 0)
                            {
                                dpMove.BlockDisplay(enemy1.entityName);
                                Debug.Log("Enemy 1 Block");
                                e1_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((m_randomrange == 2 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (m_randomrange == 6 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (m_randomrange == 9 && enemy1.MP >= enemy1.spellmoves[0].mpUsed)) && e1_turn == true && e1_spell == false)
                        {
                            e1_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy1.entityName, "is using", enemy1.spellmoves[0].name, e1_state.target.entityName);
                            e1_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                        if ((m_randomrange == 3 || m_randomrange == 7 ||  m_randomrange == 10 || (m_randomrange == 9 && enemy1.MP < enemy1.spellmoves[0].mpUsed)) && e1_turn == true && e1_spell == false)
                        {
                            e1_spell = true;
                            dpMove.SanityRegenDisplay(enemy1.entityName);
                            e1_anim.SetBool("isSanityRegen", true);
                            uiOff = true;
                        }

                    }
                    else if (enemy1.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }

                   
                    
                }
                /////////////////////////////////////////Level 3 Boss/////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Outlaw(Spiky)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && enemyIndex == i)
                {
                    e1_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy1.HP > 0)
                    {

                        if (e1_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 1 DECIDING");
                            MediumEnemyState();
                            e1_newState = false;
                        }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 1 " + m_randomrange);
                            e1_state.AITarget();
                            isTargeting = false;
                        }

                        if ((m_randomrange == 0 || (m_randomrange == 2 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || m_randomrange == 4 || m_randomrange == 8 || m_randomrange == 11) && e1_turn == true && e1_spell == false)
                        {
                            if (e1_isAttacking == true)
                            {

                                enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy1.entityName, "Attacks", e1_state.target.entityName);
                                    e1_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e1_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) <= 1f)
                                {
                                    speed = 0f;
                                    e1_anim.SetBool("isWalking", false);
                                    e1_anim.Play("Attack");
                                }
                            }

                        }

                        if (e1_isAttacking == false && e1_turn == true)
                        {
                            speed = 7.0f;
                            Debug.Log("WalkBack");
                            enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position) > 6f)
                            {
                                e1_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy1.gameObject.transform.position == enemy1Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e1_anim.SetBool("isWalking", false);
                                e1_turn = false;
                                e1_anim.Play("Idle");
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;

                        }

                        if ((m_randomrange == 1 || m_randomrange == 3 || m_randomrange == 5 || (m_randomrange == 6 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || m_randomrange == 12) && e1_turn == true && e1_spell == false)
                        {
                            if (enemy1.HP > 0)
                            {
                                dpMove.BlockDisplay(enemy1.entityName);
                                Debug.Log("Enemy 1 Block");
                                e1_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((m_randomrange == 2 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (m_randomrange == 6 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (m_randomrange == 9 && enemy1.MP >= enemy1.spellmoves[0].mpUsed)) && e1_turn == true && e1_spell == false)
                        {
                            e1_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy1.entityName, "is using", enemy1.spellmoves[0].name, e1_state.target.entityName);
                            e1_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                        if ((m_randomrange == 3 || m_randomrange == 7 || m_randomrange == 10 || (m_randomrange == 9 && enemy1.MP < enemy1.spellmoves[0].mpUsed)) && e1_turn == true && e1_spell == false)
                        {
                            e1_spell = true;
                            dpMove.SanityRegenDisplay(enemy1.entityName);
                            e1_anim.SetBool("isSanityRegen", true);
                            uiOff = true;
                        }

                    }
                    else if (enemy1.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }

                }

                /////////////////////////////////////////Level 4 Boss/////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Hammer(Possessed)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && enemyIndex == i)
                {
                    e1_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy1.HP > 0)
                    {

                        if (e1_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 1 DECIDING");
                            HardEnemyState();
                            e1_newState = false;
                        }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 1 " + h_randomrange);
                            e1_state.AITarget();
                            isTargeting = false;
                        }

                        if ((h_randomrange == 0 || (h_randomrange == 2 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || h_randomrange == 4 || h_randomrange == 8 || h_randomrange == 12 || h_randomrange == 13 || h_randomrange == 14) && e1_turn == true && e1_spell == false)
                        {
                            if (e1_isAttacking == true)
                            {

                                enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy1.entityName, "Attacks", e1_state.target.entityName);
                                    e1_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e1_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) <= 1f)
                                {
                                    speed = 0f;
                                    e1_anim.SetBool("isWalking", false);
                                    e1_anim.Play("Attack");
                                }
                            }

                        }

                        if (e1_isAttacking == false && e1_turn == true)
                        {
                            speed = 7.0f;
                            Debug.Log("WalkBack");
                            enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position) > 6f)
                            {
                                e1_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy1.gameObject.transform.position == enemy1Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e1_anim.SetBool("isWalking", false);
                                e1_turn = false;
                                e1_anim.Play("Idle");
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;

                        }
                        if ((h_randomrange == 1 || h_randomrange == 3 || h_randomrange == 5 || (h_randomrange == 6 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || h_randomrange == 9 || h_randomrange == 15) && e1_turn == true && e1_spell == false)
                        {
                            if (enemy1.HP > 0)
                            {
                                dpMove.BlockDisplay(enemy1.entityName);
                                Debug.Log("Enemy 1 Block");
                                e1_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((h_randomrange == 2 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (h_randomrange == 6 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (h_randomrange == 10 && enemy1.MP >= enemy1.spellmoves[0].mpUsed)) && e1_turn == true && e1_spell == false)
                        {
                            e1_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy1.entityName, "is using", enemy1.spellmoves[0].name, e1_state.target.entityName);
                            e1_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                        if ((m_randomrange == 3 || m_randomrange == 7 || m_randomrange == 11 || m_randomrange == 16 || (m_randomrange == 10 && enemy1.MP < enemy1.spellmoves[0].mpUsed)) && e1_turn == true)
                        {
                            dpMove.SanityRegenDisplay(enemy1.entityName);
                            e1_anim.SetBool("isSanityRegen", true);
                            uiOff = true;
                        }

                    }
                    else if (enemy1.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }

                }

                /////////////////////////////////////////Level 5 Boss/////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Terrorbringer" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && enemyIndex == i)
                {
                    e1_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy1.HP > 0)
                    {

                        if (e1_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 1 DECIDING");
                            FinalBossEnemyState();
                            e1_newState = false;
                        }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 1 " + fb_randomrange);
                            e1_state.AITarget();
                            isTargeting = false;
                        }

                        if ((fb_randomrange == 0 || (fb_randomrange == 2 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || (fb_randomrange == 6 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || fb_randomrange == 4 || fb_randomrange == 8 || fb_randomrange == 10 || fb_randomrange == 11 || fb_randomrange == 12) && e1_turn == true && e1_spell == false)
                        {
                            if (e1_isAttacking == true)
                            {

                                enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy1.entityName, "Attacks", e1_state.target.entityName);
                                    e1_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e1_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) <= 3f)
                                {
                                    speed = 0f;
                                    e1_anim.SetBool("isWalking", false);
                                    e1_anim.Play("Attack");
                                }
                            }

                        }

                        if (e1_isAttacking == false && e1_turn == true)
                        {
                            speed = 7.0f;
                            Debug.Log("WalkBack");
                            enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy1.gameObject.transform.position, enemy1Target.gameObject.transform.position) > 6f)
                            {
                                e1_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy1.gameObject.transform.position == enemy1Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e1_anim.SetBool("isWalking", false);
                                e1_turn = false;
                                e1_anim.Play("Idle");
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;

                        }
                        if ((fb_randomrange == 1 || (fb_randomrange == 3 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || fb_randomrange == 5 || (fb_randomrange == 6 && enemy1.MP < enemy1.spellmoves[0].mpUsed) || (fb_randomrange == 7 && enemy1.MP < enemy1.spellmoves[0].mpUsed)) && e1_turn == true && e1_spell == false)
                        {
                            if (enemy1.HP > 0)
                            {
                                dpMove.BlockDisplay(enemy1.entityName);
                                Debug.Log("Enemy 1 Block");
                                e1_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((fb_randomrange == 2 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (fb_randomrange == 3 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (fb_randomrange == 6 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (fb_randomrange == 7 && enemy1.MP >= enemy1.spellmoves[0].mpUsed) || (fb_randomrange == 9 && enemy1.MP >= enemy1.spellmoves[0].mpUsed)) && e1_turn == true && e1_spell == false)
                        {
                            e1_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy1.entityName, "is using", enemy1.spellmoves[0].name, "on Everyone!!!");
                            e1_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                    }
                    else if (enemy1.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }

                }

                ///////////////////////////////////////// END OF ENEMY 1 AI /////////////////////////////////////////////////////////////////////


                //////////////////////////////////////// ENEMY 2 SPOT Level 1 Side Enemy //////////////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Fire)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy2" && enemyIndex == i)
                    {
                        e2_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy2.HP > 0)
                        {
                            if (e2_newState == true)
                            {
                                uiOff = true;
                                Debug.Log("ENEMY 2 DECIDING");
                                EnemyState();
                                e2_newState = false;
                            }

                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 2 " + randomrange);
                            e2_state.AITarget();
                            isTargeting = false;
                        }

                            if (randomrange == 0 || randomrange == 2 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 4 || randomrange == 8 && e2_turn == true && e2_spell == false)
                            {
                                if (e2_isAttacking == true)
                                {
                                    enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) > 5f)
                                    {
                                        speed = 7.0f;
                                        dpMove.DisplayMove(enemy2.entityName, "Attacks", e2_state.target.entityName);
                                        e2_anim.SetBool("isWalking", true);
                                        Debug.Log("ENEMY TARGETING.... " + e2_state.target);
                                    }

                                    // If the player has reached the enemy position, then play the attack animation
                                    if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) <= 1f)
                                    {
                                        speed = 0f;
                                        e2_anim.SetBool("isWalking", false);
                                        e2_anim.Play("Attack");
                                        Debug.Log("yolooooooooooooooooooo");

                                    }
                                }

                            }

                            if (e2_isAttacking == false && e2_turn == true)
                            {
                                speed = 6.0f;
                                Debug.Log("WalkBack");
                                enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, enemy2Target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy2.gameObject.transform.position, enemy2Target.gameObject.transform.position) > 6f)
                                {
                                    e2_anim.SetBool("isWalking", true);
                                }

                                //Once the player has reached his original position end their turn
                                if (enemy2.gameObject.transform.position == enemy2Target.gameObject.transform.position)
                                {
                                    Debug.Log("Im back to my positon!");
                                    e2_anim.SetBool("isWalking", false);
                                    e2_turn = false;
                                    e2_anim.Play("Idle");
                                    enemyTD = false;
                                    isTargeting = true;
                                    EndEnemyTurn();
                                }

                                uiOff = true;
                            }

                        if (randomrange == 1 || randomrange == 3 || randomrange == 5 || randomrange == 6 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 7 && e2_turn == true && e2_spell == false)
                        {
                            if (enemy2.HP > 0)
                            {
                                Debug.Log("Enemy 2 Block");
                                dpMove.BlockDisplay(enemy2.entityName);
                                e2_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (randomrange == 2 && enemy2.MP >= enemy2.spellmoves[0].mpUsed || randomrange == 6 && enemy2.MP >= enemy2.spellmoves[0].mpUsed && e2_turn == true && e2_spell == false)
                        {
                            e2_spell = true;
                            dpMove.DisplaySpellMove(enemy2.entityName, "is using", enemy2.spellmoves[0].name, e2_state.target.entityName);
                            e2_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }
                    }
                    else if (enemy2.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }
                }
                ////////////////////////////////////////////LEVEL 2 SIDE ENEMY 2/////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Golem(Side1)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy2" && enemyIndex == i)
                {
                    e2_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy2.HP > 0)
                    {

                        if (e2_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 1 DECIDING");
                            MediumEnemyState();
                            e2_newState = false;
                        }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 1 " + m_randomrange);
                            e2_state.AITarget();
                            isTargeting = false;
                        }

                        if ((m_randomrange == 0 || (m_randomrange == 2 && enemy2.MP < enemy2.spellmoves[0].mpUsed) || m_randomrange == 4 || m_randomrange == 8 || m_randomrange == 11) && e2_turn == true && e2_spell == false)
                        {
                            if (e2_isAttacking == true)
                            {

                                enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy2.entityName, "Attacks", e2_state.target.entityName);
                                    e2_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e2_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) <= 1f)
                                {
                                    speed = 0f;
                                    e2_anim.SetBool("isWalking", false);
                                    e2_anim.Play("Attack");
                                }
                            }

                        }

                        if (e2_isAttacking == false && e2_turn == true)
                        {
                            speed = 7.0f;
                            Debug.Log("WalkBack");
                            enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, enemy2Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy2.gameObject.transform.position, enemy2Target.gameObject.transform.position) > 6f)
                            {
                                e2_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy2.gameObject.transform.position == enemy2Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e2_anim.SetBool("isWalking", false);
                                e2_turn = false;
                                e2_anim.Play("Idle");
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;

                        }
                        if ((m_randomrange == 1 || m_randomrange == 3 || m_randomrange == 5 || (m_randomrange == 6 && enemy2.MP < enemy2.spellmoves[0].mpUsed) || m_randomrange == 12) && e2_turn == true && e2_spell == false)
                        {
                            if (enemy2.HP > 0)
                            {
                                dpMove.BlockDisplay(enemy2.entityName);
                                Debug.Log("Enemy 1 Block");
                                e2_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((m_randomrange == 2 && enemy2.MP >= enemy2.spellmoves[0].mpUsed) || (m_randomrange == 6 && enemy2.MP >= enemy2.spellmoves[0].mpUsed) || (m_randomrange == 9 && enemy2.MP >= enemy2.spellmoves[0].mpUsed)) && e2_turn == true && e2_spell == false)
                        {
                            e2_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy2.entityName, "is using", enemy2.spellmoves[0].name, e2_state.target.entityName);
                            e2_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                        if ((m_randomrange == 3 || m_randomrange == 7 || m_randomrange == 10 || (m_randomrange == 9 && enemy2.MP < enemy2.spellmoves[0].mpUsed)) && e2_turn == true && e2_spell == false)
                        {
                            e2_spell = true;
                            dpMove.SanityRegenDisplay(enemy2.entityName);
                            e2_anim.SetBool("isSanityRegen", true);
                            uiOff = true;
                        }

                    }
                    else if (enemy2.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }
                }

                ////////////////////////////////////////////LEVEL 3 SIDE ENEMY 2/////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "SideLaw" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy2" && enemyIndex == i)
                {
                    e2_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy2.HP > 0)
                    {

                        if (e2_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 1 DECIDING");
                            MediumEnemyState();
                            e2_newState = false;
                        }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 1 " + m_randomrange);
                            e2_state.AITarget();
                            isTargeting = false;
                        }

                        if ((m_randomrange == 0 || (m_randomrange == 2 && enemy2.MP < enemy2.spellmoves[0].mpUsed) || m_randomrange == 4 || m_randomrange == 8 || m_randomrange == 11) && e2_turn == true && e2_spell == false)
                        {
                            if (e2_isAttacking == true)
                            {

                                enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy2.entityName, "Attacks", e2_state.target.entityName);
                                    e2_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e2_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) <= 1f)
                                {
                                    speed = 0f;
                                    e2_anim.SetBool("isWalking", false);
                                    e2_anim.Play("Attack");
                                }
                            }

                        }

                        if (e2_isAttacking == false && e2_turn == true)
                        {
                            speed = 7.0f;
                            Debug.Log("WalkBack");
                            enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, enemy2Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy2.gameObject.transform.position, enemy2Target.gameObject.transform.position) > 6f)
                            {
                                e2_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy2.gameObject.transform.position == enemy2Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e2_anim.SetBool("isWalking", false);
                                e2_turn = false;
                                e2_anim.Play("Idle");
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;

                        }
                        if ((m_randomrange == 1 || m_randomrange == 3 || m_randomrange == 5 || (m_randomrange == 6 && enemy2.MP < enemy2.spellmoves[0].mpUsed) || m_randomrange == 12) && e2_turn == true && e2_spell == false)
                        {
                            if (enemy2.HP > 0)
                            {
                                dpMove.BlockDisplay(enemy2.entityName);
                                Debug.Log("Enemy 1 Block");
                                e2_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((m_randomrange == 2 && enemy2.MP >= enemy2.spellmoves[0].mpUsed) || (m_randomrange == 6 && enemy2.MP >= enemy2.spellmoves[0].mpUsed) || (m_randomrange == 9 && enemy2.MP >= enemy2.spellmoves[0].mpUsed)) && e2_turn == true && e2_spell == false)
                        {
                            e2_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy2.entityName, "is using", enemy2.spellmoves[0].name, e2_state.target.entityName);
                            e2_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                        if ((m_randomrange == 3 || m_randomrange == 7 || m_randomrange == 10 || (m_randomrange == 9 && enemy2.MP < enemy2.spellmoves[0].mpUsed)) && e2_turn == true && e2_spell == false)
                        {
                            e2_spell = true;
                            dpMove.SanityRegenDisplay(enemy2.entityName);
                            e2_anim.SetBool("isSanityRegen", true);
                            uiOff = true;
                        }

                    }
                    else if (enemy2.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }
                }

                ////////////////////////////////////////////LEVEL 4 SIDE ENEMY /////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Mage(Possessed)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy2" && enemyIndex == i)
                {
                    e2_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy2.HP > 0)
                    {
                        if (e2_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 2 DECIDING");
                            AssistEnemyState();
                            e2_newState = false;
                        }

                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 2 " + randomrange);
                            e2_state.AITarget();
                            isTargeting = false;
                        }

                        if (randomrange == 0 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 2 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 4 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 8 && enemy2.MP < enemy2.spellmoves[0].mpUsed && e2_turn == true && e2_spell == false)
                        {
                            if (e2_isAttacking == true)
                            {
                                enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy2.entityName, "Attacks", e2_state.target.entityName);
                                    e2_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e2_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) <= 1f)
                                {
                                    speed = 0f;
                                    e2_anim.SetBool("isWalking", false);
                                    e2_anim.Play("Attack");
                                    Debug.Log("yolooooooooooooooooooo");

                                }
                            }

                        }

                        if (e2_isAttacking == false && e2_turn == true)
                        {
                            speed = 6.0f;
                            Debug.Log("WalkBack");
                            enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, enemy2Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy2.gameObject.transform.position, enemy2Target.gameObject.transform.position) > 6f)
                            {
                                e2_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy2.gameObject.transform.position == enemy2Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e2_anim.SetBool("isWalking", false);
                                e2_turn = false;
                                e2_anim.Play("Idle");
                                enemyTD = false;
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;
                        }

                        if (randomrange == 1 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 3 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 5 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 6 && enemy2.MP < enemy2.spellmoves[0].mpUsed || randomrange == 7 && enemy2.MP < enemy2.spellmoves[0].mpUsed && e2_turn == true && e2_spell == false)
                        {
                            if (enemy2.HP > 0)
                            {
                                Debug.Log("Enemy 2 Block");
                                dpMove.BlockDisplay(enemy2.entityName);
                                e2_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (randomrange == 0 && enemy2.MP >= enemy2.spellmoves[0].mpUsed || randomrange == 2 && enemy2.MP >= enemy2.spellmoves[0].mpUsed || randomrange == 4 && enemy2.MP >= enemy2.spellmoves[0].mpUsed || randomrange == 6 && enemy2.MP >= enemy2.spellmoves[0].mpUsed || randomrange == 8 && enemy2.MP >= enemy2.spellmoves[0].mpUsed && e2_spell == false)
                        {
                            e2_spell = true;
                            dpMove.HealOneDisplay(enemy2.entityName, e2_state.target.entityName);
                            e2_anim.SetBool("isHealing", true);
                            uiOff = true;
                        }

                        if (randomrange == 1 && enemy2.MP >= enemy2.spellmoves[0].mpUsed || randomrange == 3 && enemy2.MP >= enemy2.spellmoves[0].mpUsed || randomrange == 5 && enemy2.MP >= enemy2.spellmoves[0].mpUsed || randomrange == 7 && enemy2.MP >= enemy2.spellmoves[0].mpUsed && e2_spell == false)
                        {
                            e2_spell = true;
                            dpMove.HeallAllDisplay(enemy2.entityName);
                            e2_anim.SetBool("isBigHealing", true);
                            uiOff = true;
                        }
                    }
                    else if (enemy2.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }
                }

                ///////////////////////////////////////// END OF ENEMY 2 AI /////////////////////////////////////////////////////////////////////

                /////////////////////////////////////////ENEMY 3 AI Level 1 Side Enemy /////////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Water)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy3" && enemyIndex == i)
                    {
                        e3_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy3.HP > 0)
                        {
                            if (e3_newState == true)
                            {
                                uiOff = true;
                                Debug.Log("ENEMY 3 DECIDING");
                                EnemyState();
                                e3_newState = false;
                            }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                            {
                                randomrangeacc = Random.Range(0, 100);
                                Debug.Log("Enemy 3 " + randomrange);
                                e3_state.AITarget();
                                isTargeting = false;
                            }

                            if (randomrange == 0 || randomrange == 2 && enemy3.MP < enemy3.spellmoves[0].mpUsed || randomrange == 4 || randomrange == 8 && e3_turn == true && e3_spell == false)
                            {
                                if (e3_isAttacking == true)
                                {

                                    enemy3.gameObject.transform.position = Vector3.MoveTowards(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position) > 5f)
                                    {
                                        speed = 7.0f;
                                        dpMove.DisplayMove(enemy3.entityName, "Attacks", e3_state.target.entityName);
                                        e3_anim.SetBool("isWalking", true);
                                        Debug.Log("ENEMY TARGETING.... " + e3_state.target);
                                    }

                                    // If the player has reached the enemy position, then play the attack animation
                                    if (Vector3.Distance(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position) <= 1f)
                                    {
                                        speed = 0f;
                                        e3_anim.SetBool("isWalking", false);
                                        e3_anim.Play("Attack");
                                        Debug.Log("yolooooooooooooooooooo");

                                    }
                                }

                            }

                            if (e3_isAttacking == false && e3_turn == true)
                            {
                                speed = 6.0f;
                                Debug.Log("WalkBack");
                                enemy3.gameObject.transform.position = Vector3.MoveTowards(enemy3.gameObject.transform.position, enemy3Target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy3.gameObject.transform.position, enemy3Target.gameObject.transform.position) > 6f)
                                {
                                    e3_anim.SetBool("isWalking", true);
                                }

                                //Once the player has reached his original position end their turn
                                if (enemy3.gameObject.transform.position == enemy3Target.gameObject.transform.position)
                                {
                                    Debug.Log("Im back to my positon!");
                                    e3_anim.SetBool("isWalking", false);
                                    e3_turn = false;
                                    e3_anim.Play("Idle");
                                    isTargeting = true;
                                    EndEnemyTurn();
                                }

                                uiOff = true;
                            }

                        if (randomrange == 1 || randomrange == 3 || randomrange == 5 || randomrange == 6 && enemy3.MP < enemy3.spellmoves[0].mpUsed || randomrange == 7 && e3_turn == true && e3_spell == false)
                        {
                            if (enemy3.HP > 0)
                            {
                                Debug.Log("Enemy 3 Block");
                                dpMove.BlockDisplay(enemy3.entityName);
                                e3_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (randomrange == 2 && enemy3.MP >= enemy3.spellmoves[0].mpUsed || randomrange == 6 && enemy3.MP >= enemy3.spellmoves[0].mpUsed && e3_spell == false)
                        {
                            e3_spell = true;
                            dpMove.DisplaySpellMove(enemy3.entityName, "is using", enemy3.spellmoves[0].name, e3_state.target.entityName);
                            e3_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }
                    }
                    else if (enemy3.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }
                }
                /////////////////////////////////////////Level 2 Side Enemy 3 /////////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Golem(Side2)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy3" && enemyIndex == i)
                {
                    e3_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy3.HP > 0)
                    {

                        if (e3_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 3 DECIDING");
                            MediumEnemyState();
                            e3_newState = false;
                        }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 3 " + m_randomrange);
                            e3_state.AITarget();
                            isTargeting = false;
                        }

                        if ((m_randomrange == 0 || (m_randomrange == 2 && enemy3.MP < enemy3.spellmoves[0].mpUsed) || m_randomrange == 4 || m_randomrange == 8 || m_randomrange == 11) && e3_turn == true && e3_spell == false)
                        {
                            if (e3_isAttacking == true)
                            {

                                enemy3.gameObject.transform.position = Vector3.MoveTowards(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy3.entityName, "Attacks", e3_state.target.entityName);
                                    e3_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e3_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position) <= 1f)
                                {
                                    speed = 0f;
                                    e3_anim.SetBool("isWalking", false);
                                    e3_anim.Play("Attack");
                                }
                            }

                        }

                        if (e3_isAttacking == false && e3_turn == true)
                        {
                            speed = 7.0f;
                            Debug.Log("WalkBack");
                            enemy3.gameObject.transform.position = Vector3.MoveTowards(enemy3.gameObject.transform.position, enemy3Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy3.gameObject.transform.position, enemy3Target.gameObject.transform.position) > 6f)
                            {
                                e3_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy3.gameObject.transform.position == enemy3Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e3_anim.SetBool("isWalking", false);
                                e3_turn = false;
                                e3_anim.Play("Idle");
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;

                        }

                        if ((m_randomrange == 1 || m_randomrange == 3 || m_randomrange == 5 || (m_randomrange == 6 && enemy3.MP < enemy3.spellmoves[0].mpUsed) || m_randomrange == 12) && e3_turn == true && e3_spell == false)
                        {
                            if (enemy3.HP > 0)
                            {
                                dpMove.BlockDisplay(enemy3.entityName);
                                Debug.Log("Enemy 3 Block");
                                e3_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((m_randomrange == 2 && enemy3.MP >= enemy3.spellmoves[0].mpUsed) || (m_randomrange == 6 && enemy3.MP >= enemy3.spellmoves[0].mpUsed) || (m_randomrange == 9 && enemy3.MP >= enemy3.spellmoves[0].mpUsed)) && e3_turn == true && e3_spell == false)
                        {
                            e3_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy3.entityName, "is using", enemy3.spellmoves[0].name, e3_state.target.entityName);
                            e3_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                        if ((m_randomrange == 3 || m_randomrange == 7 || m_randomrange == 10 || (m_randomrange == 9 && enemy3.MP < enemy3.spellmoves[0].mpUsed)) && e3_turn == true && e3_spell == false)
                        {
                            e3_spell = true;
                            dpMove.SanityRegenDisplay(enemy3.entityName);
                            e3_anim.SetBool("isSanityRegen", true);
                            uiOff = true;
                        }
                    }
                    else if (enemy3.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }
                }
                /////////////////////////////////////////Level 3 Side Enemy 3 /////////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "SideLaw2" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy3" && enemyIndex == i)
                {
                    e3_turn = true;
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move

                    if (enemy3.HP > 0)
                    {

                        if (e3_newState == true)
                        {
                            uiOff = true;
                            Debug.Log("ENEMY 3 DECIDING");
                            MediumEnemyState();
                            e3_newState = false;
                        }
                        //If the player hasnt decided on a target yet then decide on a target at random
                        if (isTargeting == true)
                        {
                            randomrangeacc = Random.Range(0, 100);
                            Debug.Log("Enemy 3 " + m_randomrange);
                            e3_state.AITarget();
                            isTargeting = false;
                        }

                        if ((m_randomrange == 0 || (m_randomrange == 2 && enemy3.MP < enemy3.spellmoves[0].mpUsed) || m_randomrange == 4 || m_randomrange == 8 || m_randomrange == 11) && e3_turn == true && e3_spell == false)
                        {
                            if (e3_isAttacking == true)
                            {

                                enemy3.gameObject.transform.position = Vector3.MoveTowards(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position, step);
                                if (Vector3.Distance(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position) > 5f)
                                {
                                    speed = 7.0f;
                                    dpMove.DisplayMove(enemy3.entityName, "Attacks", e3_state.target.entityName);
                                    e3_anim.SetBool("isWalking", true);
                                    Debug.Log("ENEMY TARGETING.... " + e3_state.target);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position) <= 1f)
                                {
                                    speed = 0f;
                                    e3_anim.SetBool("isWalking", false);
                                    e3_anim.Play("Attack");
                                }
                            }

                        }

                        if (e3_isAttacking == false && e3_turn == true)
                        {
                            speed = 7.0f;
                            Debug.Log("WalkBack");
                            enemy3.gameObject.transform.position = Vector3.MoveTowards(enemy3.gameObject.transform.position, enemy3Target.gameObject.transform.position, step);
                            if (Vector3.Distance(enemy3.gameObject.transform.position, enemy3Target.gameObject.transform.position) > 6f)
                            {
                                e3_anim.SetBool("isWalking", true);
                            }

                            //Once the player has reached his original position end their turn
                            if (enemy3.gameObject.transform.position == enemy3Target.gameObject.transform.position)
                            {
                                Debug.Log("Im back to my positon!");
                                e3_anim.SetBool("isWalking", false);
                                e3_turn = false;
                                e3_anim.Play("Idle");
                                isTargeting = true;
                                EndEnemyTurn();
                            }

                            uiOff = true;

                        }

                        if ((m_randomrange == 1 || m_randomrange == 3 || m_randomrange == 5 || (m_randomrange == 6 && enemy3.MP < enemy3.spellmoves[0].mpUsed) || m_randomrange == 12) && e3_turn == true && e3_spell == false)
                        {
                            if (enemy3.HP > 0)
                            {
                                dpMove.BlockDisplay(enemy3.entityName);
                                Debug.Log("Enemy 3 Block");
                                e3_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((m_randomrange == 2 && enemy3.MP >= enemy3.spellmoves[0].mpUsed) || (m_randomrange == 6 && enemy3.MP >= enemy3.spellmoves[0].mpUsed) || (m_randomrange == 9 && enemy3.MP >= enemy3.spellmoves[0].mpUsed)) && e3_turn == true && e3_spell == false)
                        {
                            e3_spell = true;
                            Debug.Log("HELLO");
                            dpMove.DisplaySpellMove(enemy3.entityName, "is using", enemy3.spellmoves[0].name, e3_state.target.entityName);
                            e3_anim.SetBool("isSpelling", true);
                            uiOff = true;
                        }

                        if ((m_randomrange == 3 || m_randomrange == 7 || m_randomrange == 10 || (m_randomrange == 9 && enemy3.MP < enemy3.spellmoves[0].mpUsed)) && e3_turn == true && e3_spell == false)
                        {
                            e3_spell = true;
                            dpMove.SanityRegenDisplay(enemy3.entityName);
                            e3_anim.SetBool("isSanityRegen", true);
                            uiOff = true;
                        }
                    }
                    else if (enemy3.HP <= 0)
                    {
                        EndEnemyTurn();
                        enemies.Remove(enemies[i]);
                    }
                }

                ///////////////////////////////////////// END OF ENEMY 3 AI /////////////////////////////////////////////////////////////////////
            }

            if (enemy1.HP <= 0 && isE1_Alive == true)
            {
                enemiesToKill.Add(enemy1.gameObject);
                enemiesdead++;
                e1_anim.SetBool("isE1Alive", false);
                e1_anim.SetBool("isBlocking", false);
                e1_anim.Play("Death");
                bui.e1_Button.SetActive(false);
                bui.e1_sp_Button.SetActive(false);
                isE1_Alive = false;

            }

            if (enemiesfighting >= 2)
            {

                if (enemy2.HP <= 0 && isE2_Alive == true)
                {
                    enemiesToKill.Add(enemy2.gameObject);
                    enemiesdead++;
                    e2_anim.SetBool("isBlocking", false);
                    e2_anim.SetBool("isE2Alive", false);
                    e2_anim.Play("Death");
                    bui.e2_Button.SetActive(false);
                    bui.e2_sp_Button.SetActive(false);
                    isE2_Alive = false;
                }
            }
            if (enemiesfighting >= 3)
            {

                if (enemy3.HP <= 0 && isE3_Alive == true)
                {
                    enemiesToKill.Add(enemy3.gameObject);
                    enemiesdead++;
                    e3_anim.SetBool("isBlocking", false);
                    e3_anim.SetBool("isE3Alive", false);
                    e3_anim.Play("Death");
                    bui.e3_Button.SetActive(false);
                    bui.e3_sp_Button.SetActive(false);
                    isE3_Alive = false;
                }
            }

        }

        for (int i = 0; i < enemiesToKill.Count; i++)
        {
            enemies.Remove(enemiesToKill[i]);
        }
        enemiesToKill.Clear();

        if (enemiesdead == enemiesfighting)
        {
            if (gameManager.currentlevel == gameManager.levelOne)
            {
                gameManager.LevelOneDone = true;
                gameManager.currentlevel = gameManager.overWorld;
                gameManager.smallHealItem += 3;
                gameManager.smallManaItem += 3;
                gameManager.smallSanityItem += 3;
                gameManager.mediumHealItem += 2;
                gameManager.mediumManaItem += 2;
                gameManager.mediumSanityItem += 2;
                gameManager.mediumHealAll += 1;
                gameManager.mediumManaAll += 1;
                gameManager.mediumSanityAll += 1;
                SceneManager.LoadScene(1);
            }

            if (gameManager.currentlevel == gameManager.levelTwo)
            {
                gameManager.LevelTwoDone = true;
                gameManager.currentlevel = gameManager.overWorld;
                gameManager.smallHealItem += 2;
                gameManager.smallManaItem += 2;
                gameManager.smallSanityItem += 2;
                gameManager.mediumHealItem += 1;
                gameManager.mediumManaItem += 1;
                gameManager.mediumSanityItem += 1;
                SceneManager.LoadScene(1);
            }

            if (gameManager.currentlevel == gameManager.levelThree)
            {
                gameManager.LevelThreeDone = true;
                gameManager.currentlevel = gameManager.overWorld;
                gameManager.smallHealItem += 2;
                gameManager.smallManaItem += 2;
                gameManager.smallSanityItem += 2;
                gameManager.mediumHealItem += 2;
                gameManager.mediumManaItem += 2;
                gameManager.mediumSanityItem += 2;
                gameManager.mediumHealAll += 1;
                gameManager.mediumManaAll += 1;
                gameManager.mediumSanityAll += 1;
                SceneManager.LoadScene(1);
            }

            if (gameManager.currentlevel == gameManager.levelFour)
            {
                gameManager.LevelFourDone = true;
                gameManager.currentlevel = gameManager.overWorld;
                gameManager.smallHealItem += 2;
                gameManager.smallManaItem += 2;
                gameManager.smallSanityItem += 2;
                gameManager.mediumHealItem += 2;
                gameManager.mediumManaItem += 2;
                gameManager.mediumSanityItem += 2;
                gameManager.mediumHealAll += 2;
                gameManager.mediumManaAll += 2;
                gameManager.mediumSanityAll += 2;
                SceneManager.LoadScene(1);
            }

            if (gameManager.currentlevel == gameManager.FinalBoss)
            {
                gameManager.FinalBossDone = true;
                SceneManager.LoadScene(7);
            }


            //P1
            player1.HP += 20;
            player1.MP += 40;
            player1.SP += 25;
           PlayerPrefs.SetFloat("Player1HP", player1.HP);
           PlayerPrefs.SetFloat("Player1MP", player1.MP);
           PlayerPrefs.SetFloat("Player1SP", player1.SP);
            //P2
            if (player2.HP > 0)
            {
                player2.HP += 20;
                player2.MP += 40;
                player2.SP += 25;
                PlayerPrefs.SetFloat("Player2HP", player2.HP);
                PlayerPrefs.SetFloat("Player2MP", player2.MP);
                PlayerPrefs.SetFloat("Player2SP", player2.SP);
            } else if (player2.HP <=0)
            {
                player2.MP += 40;
                player2.SP += 25;
                PlayerPrefs.SetFloat("Player2HP", 1);
                PlayerPrefs.SetFloat("Player2MP", player2.MP);
                PlayerPrefs.SetFloat("Player2SP", player2.SP);
            }
            //P3
            if (player3.HP > 0)
            {
                player3.HP += 20;
                player3.MP += 40;
                player3.SP += 25;
                PlayerPrefs.SetFloat("Player3HP", player3.HP);
                PlayerPrefs.SetFloat("Player3MP", player3.MP);
                PlayerPrefs.SetFloat("Player3SP", player3.SP);
            } else if (player3.HP <= 0)
            {
                player3.MP += 40;
                player3.SP += 25;
                PlayerPrefs.SetFloat("Player3HP", 1);
                PlayerPrefs.SetFloat("Player3MP", player3.MP);
                PlayerPrefs.SetFloat("Player3SP", player3.SP);
            }
            //P4
            if (player4.HP > 0)
            {
                player4.HP += 20;
                player4.MP += 40;
                player4.SP += 25;
                PlayerPrefs.SetFloat("Player4HP", player4.HP);
                PlayerPrefs.SetFloat("Player4MP", player4.MP);
                PlayerPrefs.SetFloat("Player4SP", player4.SP);
            } else if (player4.HP <= 0)
            {
                player4.MP += 40;
                player4.SP += 25;
                PlayerPrefs.SetFloat("Player4HP", 1);
                PlayerPrefs.SetFloat("Player4MP", player4.MP);
                PlayerPrefs.SetFloat("Player4SP", player4.SP);
            }
        }

    }
        

    //Once a player has done a move. Proceed to the person in the list
    public void EndPlayerTurn()
    {

        playerIndex++;
        getmoving_timer = 60;

        //If the playerindex is more than the player count then make the enemies turn
        if (playerIndex >= players.Count)
        {
            currentTurns = turns.enemies;
            playerIndex = 0;
            bui.isSpellUsed = false;
            e1_isAttacking = true;
            e2_isAttacking = true;
            e3_isAttacking = true;
            e1_newState = true;
            e2_newState = true;
            e3_newState = true;
            t2_newState = true;
            t3_newState = true;
            t4_newState = true;
            e1_spell = false;
            e2_spell = false;
            e3_spell = false;
            enemyTD = false;
            e1_anim.SetBool("isBlocking", false);
            if (enemiesfighting >= 2)
            {
                e2_anim.SetBool("isBlocking", false);
            }
            if (enemiesfighting >= 3)
            {
                e3_anim.SetBool("isBlocking", false);
            }
            isTargeting = true;
            enemyTD = false;
        }
    }

    public void EndEnemyTurn()
    {
        enemyIndex++;
        getmoving_timer = 60;

        if (enemyIndex >= enemies.Count)
        {
            currentTurns = turns.players;
            bui.timer.gameObject.SetActive(true);
            bui.isSpellUsed = false;
            enemyIndex = 0;
            playerIndex = 0;
            //Adding this here because if a teammate is to die we can still attack later on
            p2_isAttacking = true;
            p3_isAttacking = true;
            p4_isAttacking = true;
            p2_spell = false;
            p3_spell = false;
            p4_spell = false;
            e1_newState = true;
            e2_newState = true;
            e3_newState = true;
            t2_newState = true;
            t3_newState = true;
            t4_newState = true;
            e1_spell = false;
            e2_spell = false;
            e3_spell = false;
            enemyTD = false;
            e1_anim.SetBool("isSpelling", false);
            if (enemiesfighting >= 2)
            { 
                e2_anim.SetBool("isSpelling", false);
            }
            if (enemiesfighting >= 3)
            {
                e3_anim.SetBool("isSpelling", false);
            }
            dpMove.p1Turn();
            bui.spellPlayer.anim.SetBool("isBlocking", false);
            bui.spellPlayer.anim.Play("Idle");
            bui.blocking = false;
            bui.player.isBlocking = false;
        }
    }


    //These functions allow the state of the AI to change to another state once pressed
    public void player2State()
    {
        if (p2_State.state == TeamAIController.AIState.Attack)
        {
            Debug.Log("P2 Block State");
            p2_Text_State.text = "Block";
            //Change to the next button when u press on the button
            p2_State.state = TeamAIController.AIState.Block;
        } else if (p2_State.state == TeamAIController.AIState.Block)
        {
            Debug.Log("P2 Spell State");
            p2_Text_State.text = "Spell";
            p2_State.state = TeamAIController.AIState.Spell;
        } else if (p2_State.state == TeamAIController.AIState.Spell)
        {
            Debug.Log("P2 Sanity State");
            p2_Text_State.text = "SanityRegen";
            p2_State.state = TeamAIController.AIState.SanityRegen;

        } else if (p2_State.state == TeamAIController.AIState.SanityRegen)
        {
            Debug.Log("P2 Random State");
            p2_Text_State.text = "Random";
            p2_State.state = TeamAIController.AIState.Random;
        }
        else if (p2_State.state == TeamAIController.AIState.Random)
        {
            Debug.Log("P2 Attack State");
            p2_Text_State.text = "Attack";
            p2_State.state = TeamAIController.AIState.Attack;
        }
    }

    public void player3State()
    {
        if (p3_State.state == TeamAIController.AIState.Attack)
        {
            Debug.Log("P3 Block State");
            p3_Text_State.text = "Block";
            p3_State.state = TeamAIController.AIState.Block;
        } else if (p3_State.state == TeamAIController.AIState.Block)
        {
            Debug.Log("P3 Spell State");
            p3_Text_State.text = "Spell";
            p3_State.state = TeamAIController.AIState.Spell;
        } else if (p3_State.state == TeamAIController.AIState.Spell)
        {
            Debug.Log("P3 Heal State");
            p3_Text_State.text = "Heal Random Teammate";
            p3_State.state = TeamAIController.AIState.Heal;

        } else if (p3_State.state == TeamAIController.AIState.Heal)
        {
            Debug.Log("P3 Heal State");
            p3_Text_State.text = "Heal All Teammates";
            p3_State.state = TeamAIController.AIState.BigHeal;
        } else if (p3_State.state == TeamAIController.AIState.BigHeal)
        {
            Debug.Log("P3 Sanity Regen State");
            p3_Text_State.text = "Sanity Regen";
            p3_State.state = TeamAIController.AIState.SanityRegen;
        } else if (p3_State.state == TeamAIController.AIState.SanityRegen)
        {
            Debug.Log("P2 Random State");
            p3_Text_State.text = "Random";
            p3_State.state = TeamAIController.AIState.Random;
        }
        else if (p3_State.state == TeamAIController.AIState.Random)
        {
            Debug.Log("P3 Random State");
            p3_Text_State.text = "Attack";
            p3_State.state = TeamAIController.AIState.Attack;
        }
    }

    public void player4State()
    {
        if (p4_State.state == TeamAIController.AIState.Attack)
        {
            Debug.Log("P4 Block State");
            p4_Text_State.text = "Block";
            p4_State.state = TeamAIController.AIState.Block;
        } else if (p4_State.state == TeamAIController.AIState.Block)
        {
            Debug.Log("P4 Spell State");
            p4_Text_State.text = "Spell";
            p4_State.state = TeamAIController.AIState.Spell;
        } else if (p4_State.state == TeamAIController.AIState.Spell)
        {
            Debug.Log("P4 Sanity Regen State");
            p4_Text_State.text = "Sanity Regen";
            p4_State.state = TeamAIController.AIState.SanityRegen;

        } else if (p4_State.state == TeamAIController.AIState.SanityRegen)
        {
            Debug.Log("P4 Random State");
            p4_Text_State.text = "Random";
            p4_State.state = TeamAIController.AIState.Random;
        } else if (p4_State.state == TeamAIController.AIState.Random)
        {
            Debug.Log("P4 Attack State");
            p4_Text_State.text = "Attack";
            p4_State.state = TeamAIController.AIState.Attack;
        }
    }



    //The Enemies state is decided on a random range.
    public void EnemyState()
    {

        if (enemyTD == false)
        {
            randomrange = Random.Range(0, 8);
            enemyTD = true;
        }

        if (randomrange == 0 || randomrange == 2 || randomrange == 4 || randomrange == 8)
        {
            Debug.Log("Enemy Attacks");
        }
        else if (randomrange == 1 || randomrange == 3 || randomrange == 5 || randomrange == 6 || randomrange == 7)
        {
            Debug.Log("Enemy Blocks");
        }

    }

    public void MediumEnemyState()
    {
        if (enemyTD == false)
        {
            m_randomrange = Random.Range(0, 12);
            enemyTD = true;
        }
    }

    public void HardEnemyState()
    {
        if (enemyTD == false)
        {
            h_randomrange = Random.Range(0, 16);
            enemyTD = true;
        }
    }

    public void AssistEnemyState()
    {
        if (enemyTD == false)
        {
            a_randomrange = Random.Range(0, 8);
            enemyTD = true;
        }
    }

    public void FinalBossEnemyState()
    {
         if (enemyTD == false)
        {
            fb_randomrange = Random.Range(0, 12);
            enemyTD = true;
        }
    }

    public void RandomTeamState()
    {

        if (teamTD == true)
        {
            print("spin the wheel");
            t_randomrange = Random.Range(0, 3);
            teamTD = false;
        }

        if (t_randomrange == 0)
        {
            Debug.Log("Teammate Attacks");
        }
        else if (t_randomrange == 1)
        {
            Debug.Log("Teammate Blocks");
        } else if (t_randomrange == 2)
        {
            Debug.Log("Teammate Spell");
        } else if (t_randomrange == 3)
        {
            Debug.Log("Team Sanity Regen");
        }

    }

    public void EnemyStateNoAnim()
    {
        int randomrange = Random.Range(0, 2);


        if (randomrange == 0)
        {
            Debug.Log(enemies[enemyIndex] + "Enemy Attacks");
            enemies[enemyIndex].GetComponent<EnemyAI>().EnemyWeaponAttack();
        }
        else if (randomrange == 1)
        {
            Debug.Log(enemies[enemyIndex] + "Blocks");
            enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
        }
        else if (randomrange == 2)
        {
            Debug.Log(enemies[enemyIndex] + "Enemy Spell");
            enemies[enemyIndex].GetComponent<EnemyAI>().EnemySpell();
        }

    }
}
