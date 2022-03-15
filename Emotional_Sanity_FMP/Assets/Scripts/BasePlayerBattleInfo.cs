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
        if (SceneManager.GetActiveScene().name == "BattleTestScene")
        {
            sm = GameObject.Find("StatsManager").GetComponent<StatsManager>();
            if (name == sm.player1.name)
            {
                HP = sm.player1.HP;
                SP = sm.player1.SP;
                accuracy = sm.player1.accuracy;
                weaponstrength = sm.player1.weaponstrength;
                weapondefence = sm.player1.weapondefence;

            }
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
}
