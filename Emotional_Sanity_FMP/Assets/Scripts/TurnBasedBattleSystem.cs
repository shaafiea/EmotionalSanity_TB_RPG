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

    //Targeted State (to stop target from being repeated)
    public bool isTargeting = false;

    public int randomrange;

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
                e1_isAttacking = true;
                e2_isAttacking = true;
                e3_isAttacking = true;

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

                //If the player (Karate) in the list is the same as the player index variable then they are able to attack
                if (players[i].GetComponent<BaseEntities>().entityName == "Karate" && playerIndex == i)
                {
                    //They can only do attacks if their HP is above 0
                    p2_turn = true;
                    if (player2.HP > 0)
                    {
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move
                        Debug.Log("Player 2 Turn");

                        //Depending on what state the AI is in do that specific command
                        if (p2_State.state == TeamAIController.AIState.Attack && p2_turn == true)
                        {
                            //If the player hasnt decided on a target yet then decide on a target at random
                            if (isTargeting == true)
                            {
                                p2_State.AITarget();
                                isTargeting = false;
                            }
                            
                            //If the player attacking state is true then play their attack
                            if (p2_isAttacking == true)
                            {
                                Debug.Log("Karate is targeting " + p2_State.target);
                                player2.gameObject.transform.position = Vector3.MoveTowards(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position, step);
                                if (Vector3.Distance(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position) > 5f)
                                {
                                    k_anim.SetBool("isWalking", true);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(player2.gameObject.transform.position, p2_State.target.gameObject.transform.position) <= 1f)
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
                                p3_isAttacking = true;
                                EndPlayerTurn();
                            }

                            uiOff = true;
                        }

                        if (p2_State.state == TeamAIController.AIState.Block && p2_turn == true)
                        {
                            if (player2.HP > 0)
                            {
                                k_anim.SetBool("isBlocking", true);
                                players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                                isTargeting = true;
                                p3_isAttacking = true;
                                uiOff = true;
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

                        //Depending on what state the AI is in do that specific command
                        if (p3_State.state == TeamAIController.AIState.Attack && p3_turn == true)
                        {
                            //If the player hasnt decided on a target yet then decide on a target at random
                            if (isTargeting == true)
                            {
                                p3_State.AITarget();
                                isTargeting = false;
                            }

                            //If the player attacking state is true then play their attack
                            if (p3_isAttacking == true)
                            {
                                //Debug.Log("Karate is targeting " + p3_State.target);
                                player3.gameObject.transform.position = Vector3.MoveTowards(player3.gameObject.transform.position, p3_State.target.gameObject.transform.position, step);
                                if (Vector3.Distance(player3.gameObject.transform.position, p3_State.target.gameObject.transform.position) > 5f)
                                {
                                    s_anim.SetBool("isWalking", true);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(player3.gameObject.transform.position, p3_State.target.gameObject.transform.position) <= 1f)
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
                                p4_isAttacking = true;
                                EndPlayerTurn();
                            }

                            uiOff = true;
                        }

                        if (p3_State.state == TeamAIController.AIState.Block && p3_turn == true)
                        {
                            if (player3.HP > 0)
                            {
                                s_anim.SetBool("isBlocking", true);
                                players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                                isTargeting = true;
                                p4_isAttacking = true;
                                uiOff = true;
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

                        //Depending on what state the AI is in do that specific command
                        if (p4_State.state == TeamAIController.AIState.Attack && p4_turn == true)
                        {
                            //If the player hasnt decided on a target yet then decide on a target at random
                            if (isTargeting == true)
                            {
                                p4_State.AITarget();
                                isTargeting = false;
                            }

                            //If the player attacking state is true then play their attack
                            if (p4_isAttacking == true)
                            {
                                Debug.Log("Karate is targeting " + p4_State.target);
                                player4.gameObject.transform.position = Vector3.MoveTowards(player4.gameObject.transform.position, p4_State.target.gameObject.transform.position, step);
                                if (Vector3.Distance(player4.gameObject.transform.position, p4_State.target.gameObject.transform.position) > 5f)
                                {
                                    b_anim.SetBool("isWalking", true);
                                }

                                // If the player has reached the enemy position, then play the attack animation
                                if (Vector3.Distance(player4.gameObject.transform.position, p4_State.target.gameObject.transform.position) <= 1f)
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
                                EndPlayerTurn();
                            }

                            uiOff = true;
                        }

                        if (p4_State.state == TeamAIController.AIState.Block && p4_turn == true)
                        {
                            if (player4.HP > 0)
                            {
                                b_anim.SetBool("isBlocking", true);
                                players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                                uiOff = true;
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

            if (player2.HP <= 0 && isP2_Alive)
            {
                isP2_Alive = false;
                k_anim.SetBool("isP2Alive", false);
                k_anim.Play("Death");
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {

            if (currentTurns == turns.enemies) //Once all players have done their moves make it so it is the enemies turn.
            {
                //Adding this here because if a teammate is to die we can still attack later on
                p2_isAttacking = true;
                p3_isAttacking = true;
                p4_isAttacking = true;

                    if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Grass)" && enemyIndex == i)
                    {
                        e1_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy1.HP > 0)
                        {
                            EnemyState();
                            if (randomrange == 0 && e1_turn == true)
                            {
                                if (isTargeting == true)
                                {
                                    e1_state.AITarget();
                                    isTargeting = false;
                                }

                                if (e1_isAttacking == true)
                                {

                                    enemy1.gameObject.transform.position = Vector3.MoveTowards(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) > 5f)
                                    {
                                        speed = 7.0f;
                                        e1_anim.SetBool("isWalking", true);
                                        Debug.Log("ENEMY TARGETING.... " + e1_state.target);
                                    }

                                    // If the player has reached the enemy position, then play the attack animation
                                    if (Vector3.Distance(enemy1.gameObject.transform.position, e1_state.target.gameObject.transform.position) <= 1f)
                                    {
                                        speed = 0f;
                                        e1_anim.SetBool("isWalking", false);
                                        e1_anim.Play("Attack");
                                        Debug.Log("yolooooooooooooooooooo");

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
                                    EndPlayerTurn();
                                }

                                uiOff = true;
                            }

                            if (randomrange == 1)
                            {
                                if (enemy1.HP > 0)
                                {
                                    e1_state.EnemyBlock();
                                    e1_turn = false;
                                    uiOff = true;
                                }

                            }
                        }
                    }

                    if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Water)" && enemyIndex == i)
                    {
                        e2_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy2.HP > 0)
                        {
                            EnemyState();
                            if (randomrange == 0 && e2_turn == true)
                            {
                                if (isTargeting == true)
                                {
                                    e2_state.AITarget();
                                    isTargeting = false;
                                }

                                if (e2_isAttacking == true)
                                {

                                    enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) > 5f)
                                    {
                                        speed = 6.0f;
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
                                    isTargeting = true;
                                    EndPlayerTurn();
                                }

                                uiOff = true;
                            }

                            if (randomrange == 1)
                            {
                                if (enemy2.HP > 0)
                                {
                                    e2_state.EnemyBlock();
                                    e2_turn = false;
                                    uiOff = true;
                                }

                            }
                        }
                    }

                    if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Fire)" && enemyIndex == i)
                    {
                        e3_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy3.HP > 0)
                        {
                            EnemyState();
                            if (randomrange == 0 && e3_turn == true)
                            {
                                if (isTargeting == true)
                                {
                                    e3_state.AITarget();
                                    isTargeting = false;
                                }

                                if (e3_isAttacking == true)
                                {

                                    enemy3.gameObject.transform.position = Vector3.MoveTowards(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position) > 5f)
                                    {
                                        speed = 6.0f;
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
                                    EndPlayerTurn();
                                }

                                uiOff = true;
                            }

                            if (randomrange == 1)
                            {
                                if (enemy3.HP > 0)
                                {
                                    e3_state.EnemyBlock();
                                    e3_turn = false;
                                    uiOff = true;
                                }

                            }
                        }
                    }

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
        bool randomdone = false;

        if (randomdone == false)
        {
            randomrange = Random.Range(0, 2);
            randomdone = true;
        }

        if (randomrange == 0)
        {
            Debug.Log("Enemy Attacks");
        }
        else if (randomrange == 1)
        {
            Debug.Log("Enemy Blocks");
        }
        else if (randomrange == 2)
        {
            Debug.Log("Enemy Spell");
        }

    }

    public void EnemyStateNoAnim()
    {
        int randomrange = Random.Range(0, 2);


        if (randomrange == 0)
        {
            Debug.Log(enemies[enemyIndex] + "Enemy Attacks");
            enemies[enemyIndex].GetComponent<EnemyAI>().EnemyWeaponAttack2();
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
