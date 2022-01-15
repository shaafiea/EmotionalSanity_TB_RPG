using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIVisuals : MonoBehaviour
{

    [SerializeField] BaseEntities enemy;
    [SerializeField] BaseEntities player;
    [SerializeField] PlaySpellAnim spellPlayer;

    //UI    
    public GameObject commandsUI;
    public GameObject spellsUI;
    public GameObject fireVFX;
    public GameObject playerUI;

    //Spells
    bool spellUIMenu = false;
    bool spellUsed = false;
    float spellLifetime;

    // Start is called before the first frame update
    void Start()
    {
        spellPlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Basic Attacking Scripts to test damage
    public void Attack()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        enemy.TakeWeaponDamage(player.damage,player.weaponstrength);
        Debug.Log("Enemy Weapon Hurt");
    }

    public void Spell()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(true);
        commandsUI.SetActive(false);
        spellUIMenu = true;
    }

    public void Command()
    {
        commandsUI.SetActive(true);
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
    }

    public void GoBack()
    {
        playerUI.SetActive(true);
        commandsUI.SetActive(false);
        spellsUI.SetActive(false);
        spellUIMenu = false;
    }

    public void PlayFireSkillAnim()
    {
        enemy.TakeSpecialDamage(enemy.entityWeakness[0], (player.spellmoves[0].damage), player.manastrength);
        player.MP = player.MP - player.spellmoves[0].mpUsed;
        Debug.Log(player.MP = player.MP - player.spellmoves[0].mpUsed);
        player.SP = player.SP - player.spellmoves[0].spUsed;
        Debug.Log(player.spellmoves[0].damage * player.manastrength / enemy.manadefence);
        Debug.Log("Enemy Spell Hurt");
        fireVFX = Instantiate(fireVFX, enemy.transform.position, enemy.transform.rotation);
        spellPlayer.anim.SetBool("spellUsed", false);
    }

    public void PlayFireSpellVFX()
    {

        if (player.MP > 50)
        {
            //playerUI.SetActive(false);
            //spellsUI.SetActive(false);
            //spellUIMenu = false;
            spellPlayer.anim.SetBool("spellUsed", true);

        } else
        {

            GoBack();
        }
        //spellLifetime = 3;
        //Destroy(fireVFX, 3);

    }

}
