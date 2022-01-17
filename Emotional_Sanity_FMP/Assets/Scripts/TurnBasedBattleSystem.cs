using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public BattleUIVisuals bui;

    // Start is called before the first frame update
    void Start()
    {
        bui = GameObject.Find("BattleUIManager").GetComponent<BattleUIVisuals>();
        uiOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTurns == turns.players)
        {
            if (playerIndex == 0)
            {
                if (uiOff == true)
                {
                    bui.playerUI.SetActive(true);
                    uiOff = false;
                }
            }
            //Debug.Log("Players Turn");
            if (playerIndex != 0)
            {
                players[playerIndex].GetComponent<TeamAIController>().AIWeaponAttack();
                uiOff = true;
            }
        } else
        {
            enemies[enemyIndex].GetComponent<EnemyAI>().EnemyWeaponAttack();
        }
    }

    public void EndPlayerTurn()
    {
        playerIndex++;

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
}
