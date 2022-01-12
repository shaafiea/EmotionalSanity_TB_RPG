using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAIController : MonoBehaviour
{

    [SerializeField] PlayerBase teamMateStats;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Current HP of " + gameObject + ": " + teamMateStats.CurHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
