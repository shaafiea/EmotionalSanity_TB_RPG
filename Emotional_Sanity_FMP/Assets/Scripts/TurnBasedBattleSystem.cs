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

    public DisplayMoves dpMove;

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

    //enemyrandom
    public int randomrange;
    public bool enemyTD = false;

    public int t_randomrange;
    public bool teamTD = false;

    public int randomrangeacc;

    public bool t2_newState = false;
    public bool t3_newState = false;
    public bool t4_newState = false;

    public bool e1_newState = false;
    public bool e2_newState = false;
    public bool e3_newState = false;

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

                        if (t2_newState == true)
                        {
                            Debug.Log("Teammate 2 is attacking");
                            RandomTeamState();
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

                        if (p2_State.state == TeamAIController.AIState.Block || (p2_State.state == TeamAIController.AIState.Random && t_randomrange == 1) && p2_turn == true)
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
                                randomrangeacc = Random.Range(0, 100);
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
                                randomrangeacc = Random.Range(0, 100);
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

            if (player3.HP <= 0 && isP3_Alive)
            {
                isP3_Alive = false;
                s_anim.SetBool("isP3Alive", false);
                s_anim.Play("Death");
            }

            if (player4.HP <= 0 && isP4_Alive)
            {
                isP4_Alive = false;
                b_anim.SetBool("isP4Alive", false);
                b_anim.Play("Death");
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {

            if (currentTurns == turns.enemies) //Once all players have done their moves make it so it is the enemies turn.
            {

////////////////////////////////////////////////////////// ENEMY 1 (FIRST SPOT) //////////////////////////////////////////////
                    if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Grass)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && enemyIndex == i)
                    {
                        e1_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy1.HP > 0)
                        {

                            if (e1_newState == true)
                            {
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

                            if ((randomrange == 0 || (randomrange == 2 && enemy1.MP < 25) || randomrange == 4 || randomrange == 8) && e1_turn == true)
                            {
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
                        if ((randomrange == 1 || randomrange == 3 || randomrange == 5 || (randomrange == 6 && enemy1.MP < 25) || randomrange == 7) && e1_turn == true)
                        {
                            if (enemy1.HP > 0) 
                            {
                                Debug.Log("Enemy 1 Block");
                                e1_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (((randomrange == 2 && enemy1.MP >= 25) || (randomrange == 6 && enemy1.MP >= 25)) && e1_turn == true)
                        {
                            Debug.Log("HELLO");
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


//////////////////////////////////////// ENEMY 2 SPOT //////////////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Fire)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy2" && enemyIndex == i)
                    {
                        e2_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy2.HP > 0)
                        {
                            if (e2_newState == true)
                            {
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

                            if (randomrange == 0 || randomrange == 2 && enemy2.MP < 25 || randomrange == 4 || randomrange == 8 && e2_turn == true)
                            {
                                if (e2_isAttacking == true)
                                {
                                    enemy2.gameObject.transform.position = Vector3.MoveTowards(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(enemy2.gameObject.transform.position, e2_state.target.gameObject.transform.position) > 5f)
                                    {
                                        speed = 7.0f;
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

                        if (randomrange == 1 || randomrange == 3 || randomrange == 5 || randomrange == 6 && enemy2.MP < 25 || randomrange == 7 && e2_turn == true)
                        {
                            if (enemy2.HP > 0)
                            {
                                Debug.Log("Enemy 2 Block");
                                e2_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (randomrange == 2 && enemy2.MP >= 25 || randomrange == 6 && enemy2.MP >= 25)
                        {
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
///////////////////////////////////////// END OF ENEMY 2 AI /////////////////////////////////////////////////////////////////////

///////////////////////////////////////// END OF ENEMY 3 AI /////////////////////////////////////////////////////////////////////
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Water)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy3" && enemyIndex == i)
                    {
                        e3_turn = true;
                        // Move our position a step closer to the target.
                        float step = speed * Time.deltaTime; // calculate distance to move

                        if (enemy3.HP > 0)
                        {
                            if (e3_newState == true)
                            {
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

                            if (randomrange == 0 || randomrange == 2 && enemy3.MP < 25 || randomrange == 4 || randomrange == 8 && e3_turn == true)
                            {
                                if (e3_isAttacking == true)
                                {

                                    enemy3.gameObject.transform.position = Vector3.MoveTowards(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position, step);
                                    if (Vector3.Distance(enemy3.gameObject.transform.position, e3_state.target.gameObject.transform.position) > 5f)
                                    {
                                        speed = 7.0f;
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

                        if (randomrange == 1 || randomrange == 3 || randomrange == 5 || randomrange == 6 && enemy3.MP < 25 || randomrange == 7 && e3_turn == true)
                        {
                            if (enemy3.HP > 0)
                            {
                                Debug.Log("Enemy 3 Block");
                                e3_anim.SetBool("isBlocking", true);
                                isTargeting = true;
                                enemies[enemyIndex].GetComponent<EnemyAI>().EnemyBlock();
                                enemyTD = false;
                                uiOff = true;
                            }
                        }

                        if (randomrange == 2 && enemy3.MP >= 25 || randomrange == 6 && enemy3.MP >= 25)
                        {
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
                ///////////////////////////////////////// END OF ENEMY 3 AI /////////////////////////////////////////////////////////////////////
            }

            if (enemy1.HP <= 0 && isE1_Alive == true)
            {
                isE1_Alive = false;
                e1_anim.SetBool("isE1Alive", false);
                e1_anim.Play("Death");
                bui.e1_Button.SetActive(false);
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Grass)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy1" && enemyIndex == i)
                {
                    enemies.Remove(enemies[i]);
                    enemiesdead++;
                }
           
            } 

            if (enemy2.HP <= 0 && isE2_Alive)
            {
                isE2_Alive = false;
                e2_anim.SetBool("isE2Alive", false);
                e2_anim.Play("Death");
                bui.e2_Button.SetActive(false);
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Water)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy2" && enemyIndex == i)
                {
                    enemies.Remove(enemies[i]);
                    enemiesdead++;
                }
            }

            if (enemy3.HP <= 0 && isE3_Alive)
            {
                isE3_Alive = false;
                e3_anim.SetBool("isE3Alive", false);
                e3_anim.Play("Death");
                bui.e3_Button.SetActive(false);
                if (enemies[i].GetComponent<BaseEntities>().entityName == "Skully(Fire)" && enemies[i].GetComponent<BaseEntities>().enemyOrder == "Enemy3" && enemyIndex == i)
                {
                    enemies.Remove(enemies[i]);
                     enemiesdead++;
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
            e1_isAttacking = true;
            e2_isAttacking = true;
            e3_isAttacking = true;
            e1_newState = true;
            e2_newState = true;
            e3_newState = true;
            t2_newState = true;
            t3_newState = true;
            t4_newState = true;
            e1_anim.SetBool("isBlocking", false);
            e2_anim.SetBool("isBlocking", false);
            e3_anim.SetBool("isBlocking", false);
            isTargeting = true;
            enemyTD = false;
        }
    }

    public void EndEnemyTurn()
    {
        enemyIndex++;

        if (enemyIndex >= enemies.Count)
        {
            currentTurns = turns.players;
            enemyIndex = 0;
            //Adding this here because if a teammate is to die we can still attack later on
            p2_isAttacking = true;
            p3_isAttacking = true;
            p4_isAttacking = true;
            e1_newState = true;
            e2_newState = true;
            e3_newState = true;
            t2_newState = true;
            t3_newState = true;
            t4_newState = true;
            e1_anim.SetBool("isSpelling", false);
            e2_anim.SetBool("isSpelling", false);
            e3_anim.SetBool("isSpelling", false);
            dpMove.p1Turn();
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
        } else if (p2_State.state == TeamAIController.AIState.SanityRegen)
        {
            Debug.Log("P2 Random State");
            p2_Text_State.text = "Random";
            p2_State.state = TeamAIController.AIState.Random;
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

        } else if (p2_State.state == TeamAIController.AIState.SanityRegen)
        {
            Debug.Log("P4 Random State");
            p2_Text_State.text = "Random";
            p2_State.state = TeamAIController.AIState.Random;
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

    public void RandomTeamState()
    {

        if (teamTD == false)
        {
            t_randomrange = Random.Range(0, 1);
            teamTD = true;
        }

        if (t_randomrange == 0)
        {
            Debug.Log("Enemy Attacks");
        }
        else if (t_randomrange == 1)
        {
            Debug.Log("Enemy Blocks");
        } else if (t_randomrange == 2)
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
