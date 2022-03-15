using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //Enemy
    public BaseEntities enemy1;

    //Player Targets
    public GameObject player1Target;
    public GameObject player2Target;
    public GameObject player3Target;
    public GameObject player4Target;

    //Enemy Targets
    public GameObject enemy1Target;
    public GameObject enemy2Target;
    public GameObject enemy3Target;

    public BattleUIVisuals bui;

    //Teammate States
    public TeamAIController p2_State;
    public TeamAIController p3_State;
    public TeamAIController p4_State;

    //State Texts
    public TextMeshProUGUI p2_Text_State;
    public TextMeshProUGUI p3_Text_State;
    public TextMeshProUGUI p4_Text_State;

    // Adjust the speed for the application.
    public float speed = 8.0f;


    //Attacking States
    public bool p2_isAttacking = false;
    public bool p3_isAttacking = false;
    public bool p4_isAttacking = false;

    public bool p2_turn = false;
    public bool p3_turn = false;
    public bool p4_turn = false;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("NinjaWarrior").GetComponent<BaseEntities>();
        player2 = GameObject.Find("Karate").GetComponent<BaseEntities>();
        player3 = GameObject.Find("SorceressWarrior").GetComponent<BaseEntities>();
        player4 = GameObject.Find("Brute").GetComponent<BaseEntities>();

        p2_State = GameObject.Find("Karate").GetComponent<TeamAIController>();
        p3_State = GameObject.Find("SorceressWarrior").GetComponent<TeamAIController>();
        p4_State = GameObject.Find("Brute").GetComponent<TeamAIController>();

        k_anim = GameObject.Find("Karate").GetComponent<Animator>();
        s_anim = GameObject.Find("Sorceress").GetComponent<Animator>();
        b_anim = GameObject.Find("Brute").GetComponent<Animator>();

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

        bui = GameObject.Find("BattleUIManager").GetComponent<BattleUIVisuals>();
        uiOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Turn Based System
        //If the turn is equal to the players turns go through each player in the list

        for (int i = 0; i < players.Count; i++)
        {
            if (currentTurns == turns.players)
            {
                if (players[i].GetComponent<BaseEntities>().entityName == "NinjaWarrior" && playerIndex == i) 
                {
                    Debug.Log("Player 1 Turn");
                    //If the player isnt dead then allow them to fight in battle
                    if (player1.HP > 0)
                    {
                        if (uiOff == true)
                        {
                            //Activate the UI
                            bui.playerUI.SetActive(true);
                            uiOff = false;
                        }
                    }
                    else
                    {
                        //If the player is dead then move on to the next player
                        bui.playerUI.SetActive(false);
                        EndPlayerTurn();
                    }
                }

                if (players[i].GetComponent<BaseEntities>().entityName == "Karate" && playerIndex == i)
                {
                    if (player2.HP > 0)
                    {
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move
                        Debug.Log("Player 2 Turn");

                        //Depending on what state the AI is in do that specific command
                        if (p2_State.state == TeamAIController.AIState.Attack && p2_turn == true)
                        {
                            p2_State.AITarget();
                            if (p2_isAttacking == true)
                            {
                                player2.gameObject.transform.position = Vector3.MoveTowards(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position, step);
                                if (Vector3.Distance(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position) > 5f)
                                {
                                    k_anim.SetBool("isWalking", true);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position) <= 5f)
                                {
                                    k_anim.SetBool("isWalking", false);
                                    k_anim.Play("Attack");
                                    
                                }
                                //Once the player has attacked start returning to the original position
                                if (p2_isAttacking == false && p2_turn == true)
                                {
                                    player2.gameObject.transform.position = Vector3.MoveTowards(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(player2.gameObject.transform.position, player2Target.gameObject.transform.position) > 5f)
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
                                        EndPlayerTurn();
                                    }

                                }
                            }
                            uiOff = true;
                        }

                        if (p2_State.state == TeamAIController.AIState.Block)
                        {
                            players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                            uiOff = true;
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
                    if (player3.HP > 0)
                    {
                        Debug.Log("Player 3 Turn");
                        if (p3_State.state == TeamAIController.AIState.Attack)
                        {
                            
                            players[playerIndex].GetComponent<TeamAIController>().AIWeaponAttack2();
                            uiOff = true;
                        }

                        if (p3_State.state == TeamAIController.AIState.Block)
                        {
                            players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                            uiOff = true;
                        }
                    }
                    else
                    {
                        EndPlayerTurn();
                        players.Remove(players[2]);
                    }
                }

                if (players[i].GetComponent<BaseEntities>().entityName == "Brute" && playerIndex == i)
                {
                    if (player4.HP > 0)
                    {
                        Debug.Log("Player 4 Turn");
                        if (p4_State.state == TeamAIController.AIState.Attack)
                        {
                            players[playerIndex].GetComponent<TeamAIController>().AIWeaponAttack2();
                            uiOff = true;
                        }

                        if (p4_State.state == TeamAIController.AIState.Block)
                        {
                            players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                            uiOff = true;
                        }
                    }
                    else if (player4.HP <= 0)
                    {
                        EndPlayerTurn();
                        players.Remove(players[3]);
                    }
                }

            }
        }
        
        if (currentTurns == turns.enemies) //Once all players have done their moves make it so it is the enemies turn.
            {
                if (enemyIndex == 0)
                {
                    //CALL Enemy Function to decide attack or block
                    EnemyState();
                }
            }
        
    }
        

    //Once a player has done a move. Proceed to the person in the list
    public void EndPlayerTurn()
    {
        playerIndex++;

        //If the playerindex is more than the player count then make the enemies turn
        if (playerIndex >= players.Count)
        {
            currentTurns = turns.enemies;
            playerIndex = 0;
        }
    }

    public void EndEnemyTurn()
    {
        enemyIndex++;

        if (enemyIndex >= enemies.Count)
        {
            currentTurns = turns.players;
            enemyIndex = 0;
        }
    }


    //These functions allow the state of the AI to change to another state once pressed
    public void player2State()
    {
        if (p2_State.state == TeamAIController.AIState.Attack)
        {
            Debug.Log("P2 Block State");
            p2_Text_State.text = "Block";
            p2_State.state = TeamAIController.AIState.Block;
        } else if (p2_State.state == TeamAIController.AIState.Block)
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
            Debug.Log("P3 Attack State");
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
            Debug.Log("P4 Attack State");
            p4_Text_State.text = "Attack";
            p4_State.state = TeamAIController.AIState.Attack;
        }
    }



    //The Enemies state is decided on a random range.
    public void EnemyState()
    {
        int randomrange = Random.Range(0, 2);


        if (randomrange == 0)
        {
            Debug.Log("Enemy Attacks");
            enemies[enemyIndex].GetComponent<EnemyAI>().EnemyWeaponAttack();
        }
        else if (randomrange == 1)
        {
            Debug.Log("Enemy Blocks");
            enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
        }
        else if (randomrange == 2)
        {
            Debug.Log("Enemy Spell");
            enemies[enemyIndex].GetComponent<EnemyAI>().EnemySpell();
        }

    }
}
