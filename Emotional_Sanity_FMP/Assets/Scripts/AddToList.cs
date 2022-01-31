using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToList : MonoBehaviour
{
    [SerializeField] private TurnBasedBattleSystem tbbs;
    [SerializeField] private bool isPlayer1;
    [SerializeField] private bool isNPC;

    private void Awake()
    {
        tbbs = GameObject.Find("TBBSystem").GetComponent<TurnBasedBattleSystem>();

        if (isPlayer1)
        {
            tbbs.players.Add(this.gameObject);
            Debug.Log("Player1");
        } else
        {
            tbbs.players.Add(this.gameObject);
            Debug.Log("Player " + gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
