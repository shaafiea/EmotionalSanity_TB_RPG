using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyOW : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent enemyAI;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 2f;

    public Transform player;

    [SerializeField] private float patrol_time;
    [SerializeField] private int waypoint;
    public float waiting_time = 0.5f;
    public Transform[] patrolWayPoints;

    public float targetdist;

    private void Start()
    {
        enemyAI = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        targetdist = Vector3.Distance(player.transform.position, transform.position);

        if (targetdist < 7)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
            Chase();
        } else if (targetdist > 10)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            Patrol();
        }

    }

    //Set Zombie to stay in position and Attack
    public void Attack()
    {
        enemyAI.isStopped = true;
        enemyAI.velocity = Vector3.zero;
    }

    //Set the Enemy to walk in his set waypoints given to him
    public void Patrol()
    {
        enemyAI.isStopped = false;
        enemyAI.speed = patrolSpeed;
        if (enemyAI.remainingDistance <= enemyAI.stoppingDistance)
        {
            patrol_time += Time.deltaTime;
            anim.Play("Idle");
            anim.SetBool("isWalking", false);
            if (patrol_time >= waiting_time)
            {
                if (waypoint == patrolWayPoints.Length - 1)
                {
                    waypoint = 0;
                }
                else
                {
                    waypoint++;
                }
                patrol_time = 0f;
            }
        }
        else
        {
            //Reset Timer if not near destination
            patrol_time = 0f;
        }
        enemyAI.destination = patrolWayPoints[waypoint].position;
    }


    //Set zombie to chase the player
    public void Chase()
    {
        enemyAI.isStopped = false;
        enemyAI.destination = player.transform.position;
        enemyAI.speed = chaseSpeed;
    }
}
