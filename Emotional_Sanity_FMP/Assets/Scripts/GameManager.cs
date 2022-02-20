using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerBase player1stats;
    [SerializeField] PlayerBase player2stats;
    [SerializeField] PlayerBase player3stats;
    [SerializeField] PlayerBase player4stats;

    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public Vector3 playerlp;
    public Vector3 player2lp;
    public Vector3 player3lp;
    public Vector3 player4lp;


    public static GameManager Instance
    {
        get { return instance; }
    }

    private static GameManager instance = null;

    private void Awake()
    {
        //Stops a second game manager from being made
        if (instance)
        {
            Debug.Log("already an instance so destroying new one");
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
