using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAIController : MonoBehaviour
{

    [SerializeField] private List<BaseEntities> enemiesOnScreen;
    [SerializeField] private BaseEntities aiStats;
    public TurnBasedBattleSystem tbbs;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Current HP of " + gameObject + ": " + teamMateStats.CurHP);
        aiStats = GetComponent<BaseEntities>();
        tbbs = GameObject.Find("TBBSystem").GetComponent<TurnBasedBattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AIWeaponAttack()
    {
        enemiesOnScreen[Random.Range(0, enemiesOnScreen.Count)].TakeWeaponDamage(aiStats.damage, aiStats.weaponstrength);
        tbbs.EndPlayerTurn();
    }

}
