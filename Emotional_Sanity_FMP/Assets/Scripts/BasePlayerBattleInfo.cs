using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayerBattleInfo : BaseEntities
{
    [SerializeField] private Image hpImage = null;

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
    }
}
