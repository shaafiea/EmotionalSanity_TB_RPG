using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public CharacterController controller;
    public float moveSpeed = 6f;
    [SerializeField] private Animator anim;


    public float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;

    public float cooldown;
    public float attackCooldown = 1.5f;

    [SerializeField] PlayerBase playerStats;

    private int scene;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        scene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Current HP: " + playerStats.CurHP);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Disable Movement if the player is in the battle scene otherwise move around and attack!
        if (scene == 1)
        {
            if (direction.magnitude >= 0.1f)
            {
                //If player is moving play animation
                anim.SetBool("isWalking", true);

                //Grab the angles that we want to return using Atan
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                //Stopping our character from snaping to the character position
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                //Setting the rotation
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

         /*   //If the player presses left click mouse button and the cool down is finished play attack animation
            if (Input.GetMouseButtonDown(0) && cooldown <= 0)
            {
                anim.SetTrigger("Attack");
                Debug.Log("Fire 1 was pressed");

                //Once the player has triggered an attack set a cooldown before next use
                cooldown = attackCooldown;
            }*/

/*
            //If the cooldown is greater than 0 minus the time period until it reaches less than 0
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(1) && cooldown <= 0)
            {
                SceneManager.LoadScene(1);
            }*/

        }

    }

    public void FootL()
    {

    }

    public void FootR()
    {

    }
}
