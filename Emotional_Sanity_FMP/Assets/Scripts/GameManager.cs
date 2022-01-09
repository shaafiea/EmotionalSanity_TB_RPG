using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerBase player1;
    [SerializeField] PlayerBase player2;
    [SerializeField] PlayerBase player3;
    [SerializeField] PlayerBase player4;
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
