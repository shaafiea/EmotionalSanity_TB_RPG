using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalRotation : MonoBehaviour
{
    public Vector3 musicalnote;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(musicalnote * Time.deltaTime);
    }
}
