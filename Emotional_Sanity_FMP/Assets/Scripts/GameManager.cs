using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int smallHealItem;
    public int smallManaItem;
    public int smallSanityItem;
    public int mediumHealItem;
    public int mediumManaItem;
    public int mediumSanityItem;

    public bool LevelOneDone;
    public bool LevelTwoDone;
    public bool LevelThreeDone;
    public bool LevelFourDone;
    public bool FinalBossDone;

    public string currentlevel;
    public string overWorld;
    public string levelOne;
    public string levelTwo;
    public string levelThree;
    public string levelFour;
    public string FinalBoss;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance = null;

    void Awake()
    {
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
        overWorld = "Overworld";
        levelOne = "Level One";
        levelTwo = "Level Two";
        levelThree = "Level Three";
        levelFour = "Level Four";
        FinalBoss = "Final Boss";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
