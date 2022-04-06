using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseEnemyBattleInfo : BaseEntities
{
    [SerializeField] private Image hpImage = null;
    [SerializeField] private Image spImage = null;
    [SerializeField] private Image mpImage = null;

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

        spImage.fillAmount = (float)SP / maxSP;

        if (SP >= maxSP)
        {
            SP = maxSP;
        }

        mpImage.fillAmount = (float)MP / maxMP;
        {
            if (MP >= maxMP)
            {
                MP = maxMP;
            }

        }
    }
}
