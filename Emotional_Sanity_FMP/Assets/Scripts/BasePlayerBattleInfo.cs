using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayerBattleInfo : BaseEntities
{
    [SerializeField] private Image hpImage = null;
    [SerializeField] private Image mpImage = null;
    [SerializeField] private Image spImage = null;

    private void Start()
    {
        
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
