using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PartyFollow : MonoBehaviour
{
    public Transform targetPoint;
    NavMeshAgent nav;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //set the navigation to always move to the target position
        nav.SetDestination(targetPoint.position);

        //if the player is not at the targetPoint, play their running animation else, play their idle animation
        if (nav.remainingDistance != 0)
        {
            anim.SetBool("isWalking", true);
        } else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void FootL()
    {

    }

    void FootR()
    {

    }
}
