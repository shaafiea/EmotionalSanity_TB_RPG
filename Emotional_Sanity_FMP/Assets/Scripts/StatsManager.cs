using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public BaseEntities player1;
    public BaseEntities player2;
    public BaseEntities player3;
    public BaseEntities player4;

    public static StatsManager Instance
    {
        get { return instance; }
    }

    private static StatsManager instance = null;

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
        if (SceneManager.GetActiveScene().name == "OverworldScene")
        {
            player1 = GameObject.Find("NinjaWarrior").GetComponent<BaseEntities>();
            player2 = GameObject.Find("Karate").GetComponent<BaseEntities>();
            player3 = GameObject.Find("SorceressWarrior").GetComponent<BaseEntities>();
            player4 = GameObject.Find("Brute").GetComponent<BaseEntities>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1);
        }
            
    }

    
}
