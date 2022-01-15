using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedBattleSystem : MonoBehaviour
{
    public GameObject playerUI;
    TeamAIController player2;
    TeamAIController player3;
    TeamAIController player4;

    //Enemy and Teammate AIs
    [SerializeField] BaseEntities enemy;
    [SerializeField] BaseEntities player2stats;
    [SerializeField] BaseEntities player3stats;
    [SerializeField] BaseEntities player4stats;


    //Later (If i decide to make it so the player has an option to control ai teammates)
    private GameObject player2UI;
    private GameObject player3UI;
    private GameObject player4UI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
