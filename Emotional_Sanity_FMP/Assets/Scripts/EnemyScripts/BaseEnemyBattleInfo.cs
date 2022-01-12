using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseEnemyBattleInfo : BaseEntities
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
