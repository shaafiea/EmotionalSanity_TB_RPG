using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class DoubleHandedController : MonoBehaviour
{
    Animator anim; //Animator ref

    public float speed = 5f;    //Speed of the doublehanded swordsman
    public float rotationspeed = 100f; // rotation speed

    SwordHitTest[] swordhittest;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //Get Sword for hit testing
        swordhittest = GetComponentsInChildren<SwordHitTest>();
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationspeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Rotate(0, rotation, 0);

        if (translation !=0)
        {
            anim.SetBool("Moving", true);
        } else
        {
            anim.SetBool("Moving", false);
        }

        anim.SetFloat("Forward", translation);


        //Click down on Left Mouse Button to Attack
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack1Trigger");
        }
    }


    public void FootL()
    {

    }

    public void FootR()
    {

    }

    public void Hit()
    {
        foreach (SwordHitTest sh in swordhittest)
        {
            if (sh.isHitting())
            {
                sh.AttackedGO().SendMessage("Attacked", null, SendMessageOptions.DontRequireReceiver);
            }
            // Reset hitting
            sh.Reset();
        }
    }

    void OnAnimatorMove()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        //If The Animation Played is not Attack, Then Apply Root Motion to the animations
        if (!stateInfo.IsTag("Attack"))
        {
            anim.ApplyBuiltinRootMotion();
        }
    }
}
