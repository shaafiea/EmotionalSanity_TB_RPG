using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Set Stats To their Default State
    public void StartGame()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("Player1HP", 100);
        PlayerPrefs.SetFloat("Player2HP", 100);
    }
}
