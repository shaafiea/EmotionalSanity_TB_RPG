using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public GameObject player;

    public GameManager gameManager;
    public BaseEntities player1;
    public BaseEntities player2;
    public BaseEntities player3;
    public BaseEntities player4;
    public GameObject LevelOnePortal;
    public GameObject LevelTwoPortal;
    public GameObject LevelThreePortal;
    public GameObject LevelFourPortal;
    public GameObject FinalBossPortal;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {


        if (LevelOnePortal && gameManager.LevelOneDone != true && other.gameObject.tag == "Player")
        {
            gameManager.currentlevel = gameManager.levelOne;
            //P1
            PlayerPrefs.SetFloat("Player1HP", player1.HP);
            PlayerPrefs.SetFloat("Player1MP", player1.MP);
            PlayerPrefs.SetFloat("Player1SP", player1.SP);
            PlayerPrefs.SetInt("Player1Accuracy", player1.accuracy);
            PlayerPrefs.SetFloat("Player1WeaponStrength", player1.weaponstrength);
            PlayerPrefs.SetFloat("Player1WeaponDefence", player1.weapondefence);
            PlayerPrefs.SetFloat("Player1WeaponSanity", player1.weaponsanity);
            PlayerPrefs.SetFloat("Player1BlockSanity", player1.blocksanity);
            //P2
            PlayerPrefs.SetFloat("Player2HP", player2.HP);
            PlayerPrefs.SetFloat("Player2MP", player2.MP);
            PlayerPrefs.SetFloat("Player2SP", player2.SP);
            PlayerPrefs.SetInt("Player2Accuracy", player2.accuracy);
            PlayerPrefs.SetFloat("Player2WeaponStrength", player2.weaponstrength);
            PlayerPrefs.SetFloat("Player2WeaponDefence", player2.weapondefence);
            PlayerPrefs.SetFloat("Player2WeaponSanity", player2.weaponsanity);
            PlayerPrefs.SetFloat("Player2BlockSanity", player2.blocksanity);
            //P3
            PlayerPrefs.SetFloat("Player3HP", player3.HP);
            PlayerPrefs.SetFloat("Player3MP", player3.MP);
            PlayerPrefs.SetFloat("Player3SP", player3.SP);
            PlayerPrefs.SetInt("Player3Accuracy", player3.accuracy);
            PlayerPrefs.SetFloat("Player3WeaponStrength", player3.weaponstrength);
            PlayerPrefs.SetFloat("Player3WeaponDefence", player3.weapondefence);
            PlayerPrefs.SetFloat("Player3WeaponSanity", player3.weaponsanity);
            PlayerPrefs.SetFloat("Player3BlockSanity", player3.blocksanity);
            //P4
            PlayerPrefs.SetFloat("Player4HP", player4.HP);
            PlayerPrefs.SetFloat("Player4MP", player4.MP);
            PlayerPrefs.SetFloat("Player4SP", player4.SP);
            PlayerPrefs.SetInt("Player4Accuracy", player4.accuracy);
            PlayerPrefs.SetFloat("Player4WeaponStrength", player4.weaponstrength);
            PlayerPrefs.SetFloat("Player4WeaponDefence", player4.weapondefence);
            PlayerPrefs.SetFloat("Player4WeaponSanity", player4.weaponsanity);
            PlayerPrefs.SetFloat("Player4BlockSanity", player4.blocksanity);
            SceneManager.LoadScene(2);
        }

        if (LevelTwoPortal && gameManager.LevelTwoDone != true && other.gameObject.tag == "Player")
        {
            gameManager.currentlevel = gameManager.levelTwo;
            //P1
            PlayerPrefs.SetFloat("Player1HP", player1.HP);
            PlayerPrefs.SetFloat("Player1MP", player1.MP);
            PlayerPrefs.SetFloat("Player1SP", player1.SP);
            PlayerPrefs.SetInt("Player1Accuracy", player1.accuracy);
            PlayerPrefs.SetFloat("Player1WeaponStrength", player1.weaponstrength);
            PlayerPrefs.SetFloat("Player1WeaponDefence", player1.weapondefence);
            PlayerPrefs.SetFloat("Player1WeaponSanity", player1.weaponsanity);
            PlayerPrefs.SetFloat("Player1BlockSanity", player1.blocksanity);
            //P2
            PlayerPrefs.SetFloat("Player2HP", player2.HP);
            PlayerPrefs.SetFloat("Player2MP", player2.MP);
            PlayerPrefs.SetFloat("Player2SP", player2.SP);
            PlayerPrefs.SetInt("Player2Accuracy", player2.accuracy);
            PlayerPrefs.SetFloat("Player2WeaponStrength", player2.weaponstrength);
            PlayerPrefs.SetFloat("Player2WeaponDefence", player2.weapondefence);
            PlayerPrefs.SetFloat("Player2WeaponSanity", player2.weaponsanity);
            PlayerPrefs.SetFloat("Player2BlockSanity", player2.blocksanity);
            //P3
            PlayerPrefs.SetFloat("Player3HP", player3.HP);
            PlayerPrefs.SetFloat("Player3MP", player3.MP);
            PlayerPrefs.SetFloat("Player3SP", player3.SP);
            PlayerPrefs.SetInt("Player3Accuracy", player3.accuracy);
            PlayerPrefs.SetFloat("Player3WeaponStrength", player3.weaponstrength);
            PlayerPrefs.SetFloat("Player3WeaponDefence", player3.weapondefence);
            PlayerPrefs.SetFloat("Player3WeaponSanity", player3.weaponsanity);
            PlayerPrefs.SetFloat("Player3BlockSanity", player3.blocksanity);
            //P4
            PlayerPrefs.SetFloat("Player4HP", player4.HP);
            PlayerPrefs.SetFloat("Player4MP", player4.MP);
            PlayerPrefs.SetFloat("Player4SP", player4.SP);
            PlayerPrefs.SetInt("Player4Accuracy", player4.accuracy);
            PlayerPrefs.SetFloat("Player4WeaponStrength", player4.weaponstrength);
            PlayerPrefs.SetFloat("Player4WeaponDefence", player4.weapondefence);
            PlayerPrefs.SetFloat("Player4WeaponSanity", player4.weaponsanity);
            PlayerPrefs.SetFloat("Player4BlockSanity", player4.blocksanity);
            SceneManager.LoadScene(3);
        }

        if (LevelThreePortal && gameManager.LevelThreeDone != true && other.gameObject.tag == "Player")
        {
            gameManager.currentlevel = gameManager.levelThree;
            //P1
            PlayerPrefs.SetFloat("Player1HP", player1.HP);
            PlayerPrefs.SetFloat("Player1MP", player1.MP);
            PlayerPrefs.SetFloat("Player1SP", player1.SP);
            PlayerPrefs.SetInt("Player1Accuracy", player1.accuracy);
            PlayerPrefs.SetFloat("Player1WeaponStrength", player1.weaponstrength);
            PlayerPrefs.SetFloat("Player1WeaponDefence", player1.weapondefence);
            PlayerPrefs.SetFloat("Player1WeaponSanity", player1.weaponsanity);
            PlayerPrefs.SetFloat("Player1BlockSanity", player1.blocksanity);
            //P2
            PlayerPrefs.SetFloat("Player2HP", player2.HP);
            PlayerPrefs.SetFloat("Player2MP", player2.MP);
            PlayerPrefs.SetFloat("Player2SP", player2.SP);
            PlayerPrefs.SetInt("Player2Accuracy", player2.accuracy);
            PlayerPrefs.SetFloat("Player2WeaponStrength", player2.weaponstrength);
            PlayerPrefs.SetFloat("Player2WeaponDefence", player2.weapondefence);
            PlayerPrefs.SetFloat("Player2WeaponSanity", player2.weaponsanity);
            PlayerPrefs.SetFloat("Player2BlockSanity", player2.blocksanity);
            //P3
            PlayerPrefs.SetFloat("Player3HP", player3.HP);
            PlayerPrefs.SetFloat("Player3MP", player3.MP);
            PlayerPrefs.SetFloat("Player3SP", player3.SP);
            PlayerPrefs.SetInt("Player3Accuracy", player3.accuracy);
            PlayerPrefs.SetFloat("Player3WeaponStrength", player3.weaponstrength);
            PlayerPrefs.SetFloat("Player3WeaponDefence", player3.weapondefence);
            PlayerPrefs.SetFloat("Player3WeaponSanity", player3.weaponsanity);
            PlayerPrefs.SetFloat("Player3BlockSanity", player3.blocksanity);
            //P4
            PlayerPrefs.SetFloat("Player4HP", player4.HP);
            PlayerPrefs.SetFloat("Player4MP", player4.MP);
            PlayerPrefs.SetFloat("Player4SP", player4.SP);
            PlayerPrefs.SetInt("Player4Accuracy", player4.accuracy);
            PlayerPrefs.SetFloat("Player4WeaponStrength", player4.weaponstrength);
            PlayerPrefs.SetFloat("Player4WeaponDefence", player4.weapondefence);
            PlayerPrefs.SetFloat("Player4WeaponSanity", player4.weaponsanity);
            PlayerPrefs.SetFloat("Player4BlockSanity", player4.blocksanity);
            SceneManager.LoadScene(4);
        }

        if (LevelFourPortal && gameManager.LevelFourDone != true && other.gameObject.tag == "Player")
        {
            gameManager.currentlevel = gameManager.levelFour;
            //P1
            PlayerPrefs.SetFloat("Player1HP", player1.HP);
            PlayerPrefs.SetFloat("Player1MP", player1.MP);
            PlayerPrefs.SetFloat("Player1SP", player1.SP);
            PlayerPrefs.SetInt("Player1Accuracy", player1.accuracy);
            PlayerPrefs.SetFloat("Player1WeaponStrength", player1.weaponstrength);
            PlayerPrefs.SetFloat("Player1WeaponDefence", player1.weapondefence);
            PlayerPrefs.SetFloat("Player1WeaponSanity", player1.weaponsanity);
            PlayerPrefs.SetFloat("Player1BlockSanity", player1.blocksanity);
            //P2
            PlayerPrefs.SetFloat("Player2HP", player2.HP);
            PlayerPrefs.SetFloat("Player2MP", player2.MP);
            PlayerPrefs.SetFloat("Player2SP", player2.SP);
            PlayerPrefs.SetInt("Player2Accuracy", player2.accuracy);
            PlayerPrefs.SetFloat("Player2WeaponStrength", player2.weaponstrength);
            PlayerPrefs.SetFloat("Player2WeaponDefence", player2.weapondefence);
            PlayerPrefs.SetFloat("Player2WeaponSanity", player2.weaponsanity);
            PlayerPrefs.SetFloat("Player2BlockSanity", player2.blocksanity);
            //P3
            PlayerPrefs.SetFloat("Player3HP", player3.HP);
            PlayerPrefs.SetFloat("Player3MP", player3.MP);
            PlayerPrefs.SetFloat("Player3SP", player3.SP);
            PlayerPrefs.SetInt("Player3Accuracy", player3.accuracy);
            PlayerPrefs.SetFloat("Player3WeaponStrength", player3.weaponstrength);
            PlayerPrefs.SetFloat("Player3WeaponDefence", player3.weapondefence);
            PlayerPrefs.SetFloat("Player3WeaponSanity", player3.weaponsanity);
            PlayerPrefs.SetFloat("Player3BlockSanity", player3.blocksanity);
            //P4
            PlayerPrefs.SetFloat("Player4HP", player4.HP);
            PlayerPrefs.SetFloat("Player4MP", player4.MP);
            PlayerPrefs.SetFloat("Player4SP", player4.SP);
            PlayerPrefs.SetInt("Player4Accuracy", player4.accuracy);
            PlayerPrefs.SetFloat("Player4WeaponStrength", player4.weaponstrength);
            PlayerPrefs.SetFloat("Player4WeaponDefence", player4.weapondefence);
            PlayerPrefs.SetFloat("Player4WeaponSanity", player4.weaponsanity);
            PlayerPrefs.SetFloat("Player4BlockSanity", player4.blocksanity);
            SceneManager.LoadScene(5);
        }

        if (FinalBossPortal && gameManager.FinalBossDone != true && gameManager.LevelOneDone == true && gameManager.LevelTwoDone == true && gameManager.LevelThreeDone == true && gameManager.LevelFourDone == true && other.gameObject.tag == "Player")
        {
            gameManager.currentlevel = gameManager.FinalBoss;
            //P1
            PlayerPrefs.SetFloat("Player1HP", player1.HP);
            PlayerPrefs.SetFloat("Player1MP", player1.MP);
            PlayerPrefs.SetFloat("Player1SP", player1.SP);
            PlayerPrefs.SetInt("Player1Accuracy", player1.accuracy);
            PlayerPrefs.SetFloat("Player1WeaponStrength", player1.weaponstrength);
            PlayerPrefs.SetFloat("Player1WeaponDefence", player1.weapondefence);
            PlayerPrefs.SetFloat("Player1WeaponSanity", player1.weaponsanity);
            PlayerPrefs.SetFloat("Player1BlockSanity", player1.blocksanity);
            //P2
            PlayerPrefs.SetFloat("Player2HP", player2.HP);
            PlayerPrefs.SetFloat("Player2MP", player2.MP);
            PlayerPrefs.SetFloat("Player2SP", player2.SP);
            PlayerPrefs.SetInt("Player2Accuracy", player2.accuracy);
            PlayerPrefs.SetFloat("Player2WeaponStrength", player2.weaponstrength);
            PlayerPrefs.SetFloat("Player2WeaponDefence", player2.weapondefence);
            PlayerPrefs.SetFloat("Player2WeaponSanity", player2.weaponsanity);
            PlayerPrefs.SetFloat("Player2BlockSanity", player2.blocksanity);
            //P3
            PlayerPrefs.SetFloat("Player3HP", player3.HP);
            PlayerPrefs.SetFloat("Player3MP", player3.MP);
            PlayerPrefs.SetFloat("Player3SP", player3.SP);
            PlayerPrefs.SetInt("Player3Accuracy", player3.accuracy);
            PlayerPrefs.SetFloat("Player3WeaponStrength", player3.weaponstrength);
            PlayerPrefs.SetFloat("Player3WeaponDefence", player3.weapondefence);
            PlayerPrefs.SetFloat("Player3WeaponSanity", player3.weaponsanity);
            PlayerPrefs.SetFloat("Player3BlockSanity", player3.blocksanity);
            //P4
            PlayerPrefs.SetFloat("Player4HP", player4.HP);
            PlayerPrefs.SetFloat("Player4MP", player4.MP);
            PlayerPrefs.SetFloat("Player4SP", player4.SP);
            PlayerPrefs.SetInt("Player4Accuracy", player4.accuracy);
            PlayerPrefs.SetFloat("Player4WeaponStrength", player4.weaponstrength);
            PlayerPrefs.SetFloat("Player4WeaponDefence", player4.weapondefence);
            PlayerPrefs.SetFloat("Player4WeaponSanity", player4.weaponsanity);
            PlayerPrefs.SetFloat("Player4BlockSanity", player4.blocksanity);
            SceneManager.LoadScene(6);
        }
    }
}
