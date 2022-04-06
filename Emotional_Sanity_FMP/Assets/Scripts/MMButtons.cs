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
        //P1
        PlayerPrefs.SetFloat("Player1HP", 100);
        PlayerPrefs.SetFloat("Player1MP", 100);
        PlayerPrefs.SetFloat("Player1SP", 50);
        PlayerPrefs.SetInt("Player1Accuracy", 75);
        PlayerPrefs.SetFloat("Player1WeaponStrength", 100);
        PlayerPrefs.SetFloat("Player1WeaponDefence", 100);
        PlayerPrefs.SetFloat("Player1WeaponSanity", 5);
        PlayerPrefs.SetFloat("Player1BlockSanity", 10);
        //P2
        PlayerPrefs.SetFloat("Player2HP", 100);
        PlayerPrefs.SetFloat("Player2MP", 100);
        PlayerPrefs.SetFloat("Player2SP", 50);
        PlayerPrefs.SetInt("Player2Accuracy", 75);
        PlayerPrefs.SetFloat("Player2WeaponStrength", 100);
        PlayerPrefs.SetFloat("Player2WeaponDefence", 100);
        PlayerPrefs.SetFloat("Player2WeaponSanity", 5);
        PlayerPrefs.SetFloat("Player2BlockSanity", 10);
        //P3
        PlayerPrefs.SetFloat("Player3HP", 100);
        PlayerPrefs.SetFloat("Player3MP", 100);
        PlayerPrefs.SetFloat("Player3SP", 50);
        PlayerPrefs.SetInt("Player3Accuracy", 75);
        PlayerPrefs.SetFloat("Player3WeaponStrength", 100);
        PlayerPrefs.SetFloat("Player3WeaponDefence", 100);
        PlayerPrefs.SetFloat("Player3WeaponSanity", 5);
        PlayerPrefs.SetFloat("Player3BlockSanity", 10);
        //P4
        PlayerPrefs.SetFloat("Player4HP", 100);
        PlayerPrefs.SetFloat("Player4MP", 100);
        PlayerPrefs.SetFloat("Player4SP", 50);
        PlayerPrefs.SetInt("Player4Accuracy", 75);
        PlayerPrefs.SetFloat("Player4WeaponStrength", 100);
        PlayerPrefs.SetFloat("Player4WeaponDefence", 100);
        PlayerPrefs.SetFloat("Player4WeaponSanity", 5);
        PlayerPrefs.SetFloat("Player4BlockSanity", 10);
    }
}
