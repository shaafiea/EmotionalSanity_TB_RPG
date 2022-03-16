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

    public StatsManager sm;

    private void Awake()
    {

    }

    private void Start()
    {
       if (entityName == "NinjaWarrior")
        {
            HP = PlayerPrefs.GetFloat("Player1HP");
        }

        if (entityName == "Karate")
        {
            HP = PlayerPrefs.GetFloat("Player2HP");
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

        if (SP <= 0)
        {

        }
    }

    void FootL()
    {

    }

    void FootR()
    {

    }
}
