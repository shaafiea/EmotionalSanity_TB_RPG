using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitTest : MonoBehaviour
{

    bool hitting = false; // true when collider hits some object
    string namehit; //name of game object being hit
    GameObject go; // ref to game object

    
    private void OnTriggerEnter(Collider other)
    {
        //If Attack doesnt hit enemy ignore
        if (other.gameObject.tag != "Enemy") return;
        namehit = other.gameObject.name;

        hitting = true;

        go = other.gameObject;
        
    }

    // did the sword hit something
    public bool isHitting()
    {
        return hitting;
    }

    //name of the hit object
    public string Name()
    {
        return namehit;
    }

    //game object that was hit
    public GameObject AttackedGO()
    {
        return go;
    }

    //Reset the hit detection
    public void Reset()
    {
        hitting = false;
    }

}
