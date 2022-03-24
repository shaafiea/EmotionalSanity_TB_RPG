using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasePlayerBattleInfo : BaseEntities
{
    [SerializeField] private Image hpImage = null;
    [SerializeField] private Image mpImage = null;
    [SerializeField] private Image spImage = null;


    private void Awake()
    {

    }

    private void Start()
    {
       if (entityName == "NinjaWarrior")
        {
            HP = PlayerPrefs.GetFloat("Player1HP");
            MP = PlayerPrefs.GetFloat("Player1MP");
            SP = PlayerPrefs.GetFloat("Player1SP");
            accuracy = PlayerPrefs.GetInt("Player1Accuracy");
            weaponstrength = PlayerPrefs.GetFloat("Player1WeaponStrength");
            weapondefence = PlayerPrefs.GetFloat("Player1WeaponDefence");
        }

        if (entityName == "Karate")
        {
            HP = PlayerPrefs.GetFloat("Player2HP");
            MP = PlayerPrefs.GetFloat("Player2MP");
            SP = PlayerPrefs.GetFloat("Player2SP");
            accuracy = PlayerPrefs.GetInt("Player2Accuracy");
            weaponstrength = PlayerPrefs.GetFloat("Player2WeaponStrength");
            weapondefence = PlayerPrefs.GetFloat("Player2WeaponDefence");
        }

        if (entityName == "SorceressWarrior")
        {
            HP = PlayerPrefs.GetFloat("Player3HP");
            MP = PlayerPrefs.GetFloat("Player3MP");
            SP = PlayerPrefs.GetFloat("Player3SP");
            accuracy = PlayerPrefs.GetInt("Player3Accuracy");
            weaponstrength = PlayerPrefs.GetFloat("Player3WeaponStrength");
            weapondefence = PlayerPrefs.GetFloat("Player3WeaponDefence");
        }

        if (entityName == "Brute")
        {
            HP = PlayerPrefs.GetFloat("Player4HP");
            MP = PlayerPrefs.GetFloat("Player4MP");
            SP = PlayerPrefs.GetFloat("Player4SP");
            accuracy = PlayerPrefs.GetInt("Player4Accuracy");
            weaponstrength = PlayerPrefs.GetFloat("Player4WeaponStrength");
            weapondefence = PlayerPrefs.GetFloat("Player4WeaponDefence");
        }
    }

    private void Update()
    {
        hpImage.fillAmount = (float)HP / maxHP;

        if (HP >= maxHP)
        {
            HP = maxHP;
        }

        if (HP <= 0)
        {

        }

        mpImage.fillAmount = (float)MP / maxMP;

        if (MP >= maxMP)
        {
            MP = maxMP;
        }

        if (MP <= 0)
        {

        }

        spImage.fillAmount = (float)SP / maxSP;

        if (SP >= maxSP)
        {
            SP = maxSP;
        }

    }

    void FootL()
    {

    }

    void FootR()
    {

    }
}
