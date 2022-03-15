using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerTargetPicker : MonoBehaviour
{

 

    private int damage;
    private float strength;
    private string instigatorName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set Stats for Move
    public void SetStats(int dmg, float strgth, string n)
    {
        damage = dmg;
        strength = strgth;
        instigatorName = n;
    }
}
