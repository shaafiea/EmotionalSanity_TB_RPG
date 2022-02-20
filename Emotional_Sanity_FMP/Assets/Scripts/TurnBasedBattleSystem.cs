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
        enemy1 = GameObject.FindWithTag("Enemy").GetComponent<BaseEntities>();
        enemies.Insert(0, enemy1.gameObject);

        bui = GameObject.Find("BattleUIManager").GetComponent<BattleUIVisuals>();
        uiOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Turn Based System

        //If the turn is equal to the players turns go through each player in the list
        if (currentTurns == turns.players)
        {
            if (playerIndex == 0)
            {
                //If the player isnt dead then allow them to fight in battle
                if (player1.HP > 0)
                {
                    if (uiOff == true)
                    {
                        //Activate the UI
                        bui.playerUI.SetActive(true);
                        uiOff = false;
                    }
                } else
                {
                    //If the player is dead then move on to the next player
                    bui.playerUI.SetActive(false);
                    EndPlayerTurn();
                }
            }
            //Debug.Log("Players Turn");
            if (playerIndex == 1)
            {
                if (player2.HP > 0)
                {
                    Debug.Log("Player 2 Turn");
                    //Depending on what state the AI is in do that specific command
                    if (p2_State.state == TeamAIController.AIState.Attack)
                    {
                        players[playerIndex].GetComponent<TeamAIController>().AIWeaponAttack();
                        uiOff = true;
                    }

                    if (p2_State.state == TeamAIController.AIState.Block)
                    {
                        players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                        uiOff = true;
                    }
                } else
                {
                    EndPlayerTurn();
                }
            }

            if (playerIndex == 2)
            {
                if (player3.HP > 0)
                {
                    if (p3_State.state == TeamAIController.AIState.Attack)
                    {
                        players[playerIndex].GetComponent<TeamAIController>().AIWeaponAttack();
                        uiOff = true;
                    }

                    if (p3_State.state == TeamAIController.AIState.Block)
                    {
                        players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                        uiOff = true;
                    }
                } else
                {
                    EndPlayerTurn();
                }
            }

            if (playerIndex == 3)
            {
                if (player4.HP > 0)
                {
                    if (p4_State.state == TeamAIController.AIState.Attack)
                    {
                        players[playerIndex].GetComponent<TeamAIController>().AIWeaponAttack();
                        uiOff = true;
                    }

                    if (p4_State.state == TeamAIController.AIState.Block)
                    {
                        players[playerIndex].GetComponent<TeamAIController>().AIBlock();
                        uiOff = true;
                    }
                }
            }

        } else if (currentTurns == turns.enemies) //Once all players have done their moves make it so it is the enemies turn.
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
            Debug.Log("Block State");
            p2_Text_State.text = "Block";
            p2_State.state = TeamAIController.AIState.Block;
        } else if (p2_State.state == TeamAIController.AIState.Block)
        {
            Debug.Log("Attack State");
            p2_Text_State.text = "Attack";
            p2_State.state = TeamAIController.AIState.Attack;
        }
    }

    public void player3State()
    {
        if (p3_State.state == TeamAIController.AIState.Attack)
        {
            p3_Text_State.text = "Block";
            p3_State.state = TeamAIController.AIState.Block;
        } else if (p3_State.state == TeamAIController.AIState.Block)
        {
            p3_Text_State.text = "Attack";
            p3_State.state = TeamAIController.AIState.Attack;
        }
    }

    public void player4State()
    {
        if (p4_State.state == TeamAIController.AIState.Attack)
        {
            p4_Text_State.text = "Block";
            p4_State.state = TeamAIController.AIState.Block;
        } else if (p4_State.state == TeamAIController.AIState.Block)
        {
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
