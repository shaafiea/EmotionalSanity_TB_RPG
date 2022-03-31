using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRubble : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Bonk");
        GameObject cloneprefab = Instantiate(prefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }
}
