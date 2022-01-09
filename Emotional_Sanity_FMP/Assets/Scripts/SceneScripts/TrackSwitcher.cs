using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitcher : MonoBehaviour
{
    Cinemachine.CinemachineDollyCart cart;

    //This keeps all of the tracks made in the scene
    public Cinemachine.CinemachineSmoothPath startPath;
    public Cinemachine.CinemachineSmoothPath[] alternatePaths;
    // Start is called before the first frame update
    void Start()
    {
        cart = GetComponent<Cinemachine.CinemachineDollyCart>();

        Reset();
    }

    private void Reset()
    {
        StopAllCoroutines();
        cart.m_Path = startPath;

        StartCoroutine(ChangeTrack());
    }

    IEnumerator ChangeTrack()
    {
        yield return new WaitForSeconds(Random.RandomRange(4, 6));
        var path = alternatePaths[Random.RandomRange(0, alternatePaths.Length)];
        cart.m_Path = path;

        StartCoroutine(ChangeTrack());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
