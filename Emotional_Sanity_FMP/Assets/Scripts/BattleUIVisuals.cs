using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BattleUIVisuals : MonoBehaviour
{
    public List<BaseEntities> teamMembers;
    public List<BaseEntities> enemies;
    public BaseEntities player;
    public PlaySpellAnim spellPlayer;
    [SerializeField] private PlayerTargetPicker targetPicker = null;

    public TurnBasedBattleSystem tbbs;

    //Team Target Buttons
    public GameObject p1_Button;
    public GameObject p2_Button;
    public GameObject p3_Button;
    public GameObject p4_Button;
    public Text p1_text;
    public Text p2_text;
    public Text p3_text;
    public Text p4_text;

    //Enemy Target Buttons
    public GameObject e1_Button;
    public GameObject e2_Button;
    public GameObject e3_Button;
    public Text e1_wp_text;
    public Text e2_wp_text;
    public Text e3_wp_text;
    public Text e1_sp_text;
    public Text e2_sp_text;
    public Text e3_sp_text;

    public DisplayMoveDescription display;

    //UI    
    public GameObject commandsUI;
    public GameObject spellsUI;
    public GameObject playerUI;
    public GameObject targetUI;
    public GameObject spellTargetUI;
    public GameObject teamTargetUI;

    public GameObject grassVFX;
    public GameObject iceVFX;
    public GameObject fireVFX;
    public GameObject waterVFX;
    public GameObject rockVFX;
    public GameObject s_healVFX;
    public GameObject b_healVFX;
    public GameObject sanityVFX;

    //Spell Decider
    public int spelldecider;
    public int teamdecider;
    public GameObject bigHealspot;

    //Show Target Button and Back Button
    public List<GameObject> targetAttackButtons = null;
    [SerializeField] private GameObject backButton = null;

    //Spells
    bool spellUIMenu = false;
    bool spellUsed = false;
    float spellLifetime;

    //PlayerBools
    public bool isSpellUsed = false;
    bool isAttacking = false;
    bool playerTurn = false;
    public bool blocking = false;
    public bool isFireUsed = false;
    public bool isHealOneUsed = false;
    public bool isHealAllUsed = false;
    public bool isRockUsed = false;

    // Adjust the speed for the application.
    public float speed = 25.0f;

    public int randomrange;

    public BaseEntities target;

    // Start is called before the first frame update
    void Start()
    {
        tbbs = GameObject.Find("TBBSystem").GetComponent<TurnBasedBattleSystem>();
        player = GameObject.Find("NinjaWarrior").GetComponent<BaseEntities>();
        spellPlayer = GameObject.Find("NinjaWarrior").GetComponentInChildren<PlaySpellAnim>();
        teamMembers.Insert(0, tbbs.player1);
        teamMembers.Insert(1, tbbs.player2);
        teamMembers.Insert(2, tbbs.player3);
        teamMembers.Insert(3, tbbs.player4);
        p1_Button = GameObject.FindGameObjectWithTag("p1Button").GetComponent<GameObject>();
        p2_Button = GameObject.FindGameObjectWithTag("p2Button").GetComponent<GameObject>();
        p3_Button = GameObject.FindGameObjectWithTag("p3Button").GetComponent<GameObject>();
        p4_Button = GameObject.FindGameObjectWithTag("p4Button").GetComponent<GameObject>();
        p1_text = GameObject.FindGameObjectWithTag("p1Text").GetComponent<Text>();
        p2_text = GameObject.FindGameObjectWithTag("p2Text").GetComponent<Text>();
        p3_text = GameObject.FindGameObjectWithTag("p3Text").GetComponent<Text>();
        p4_text = GameObject.FindGameObjectWithTag("p4Text").GetComponent<Text>();
        p1_text.text = tbbs.player1.entityName;
        p2_text.text = tbbs.player2.entityName;
        p3_text.text = tbbs.player3.entityName;
        p4_text.text = tbbs.player4.entityName;

        //Setting up the target buttons to know how many target there are on the field
        for (int i = 0; i < tbbs.enemies.Count; i++)
        {
            if (tbbs.enemies.Count == 1)
            {
                enemies.Insert(0, tbbs.enemy1);
                e1_Button = GameObject.FindGameObjectWithTag("e1Button").GetComponent<GameObject>();
                e2_Button = GameObject.FindGameObjectWithTag("e2Button").GetComponent<GameObject>();
                e3_Button = GameObject.FindGameObjectWithTag("e3Button").GetComponent<GameObject>();
                e1_wp_text = GameObject.FindGameObjectWithTag("e1Text").GetComponent<Text>();
                e2_Button.SetActive(false);
                e3_Button.SetActive(false);
                e1_wp_text.text = tbbs.enemy1.entityName;

                e1_sp_text = GameObject.FindGameObjectWithTag("e1sptext").GetComponent<Text>();
                e1_sp_text.text = tbbs.enemy1.entityName;
            }
            else if (enemies.Count == 2)
            {
                enemies.Insert(0, tbbs.enemy1);
                enemies.Insert(1, tbbs.enemy2);
                e1_Button = GameObject.FindGameObjectWithTag("e1Button").GetComponent<GameObject>();
                e2_Button = GameObject.FindGameObjectWithTag("e2Button").GetComponent<GameObject>();
                e3_Button = GameObject.FindGameObjectWithTag("e3Button").GetComponent<GameObject>();
                e1_wp_text = GameObject.FindGameObjectWithTag("e1Text").GetComponent<Text>();
                e2_wp_text = GameObject.FindGameObjectWithTag("e2Text").GetComponent<Text>();
                e3_Button.SetActive(false);
                e1_wp_text.text = tbbs.enemy1.entityName;
                e2_wp_text.text = tbbs.enemy2.entityName;

                e1_sp_text = GameObject.FindGameObjectWithTag("e1sptext").GetComponent<Text>();
                e2_sp_text = GameObject.FindGameObjectWithTag("e2sptext").GetComponent<Text>();
                e1_sp_text.text = tbbs.enemy1.entityName;
                e2_sp_text.text = tbbs.enemy2.entityName;
            }
            else if (enemies.Count == 3)
            {
/*                e1_Button = GameObject.FindGameObjectWithTag("e1Button").GetComponent<GameObject>();
                e2_Button = GameObject.FindGameObjectWithTag("e2Button").GetComponent<GameObject>();
                e3_Button = GameObject.FindGameObjectWithTag("e3Button").GetComponent<GameObject>();*/
                e1_wp_text = GameObject.FindGameObjectWithTag("e1Text").GetComponent<Text>();
                e2_wp_text = GameObject.FindGameObjectWithTag("e2Text").GetComponent<Text>();
                e3_wp_text = GameObject.FindGameObjectWithTag("e3Text").GetComponent<Text>();
                e1_wp_text.text = tbbs.enemy1.entityName;
                e2_wp_text.text = tbbs.enemy2.entityName;
                e3_wp_text.text = tbbs.enemy3.entityName;
                e1_sp_text = GameObject.FindGameObjectWithTag("e1sptext").GetComponent<Text>();
                e2_sp_text = GameObject.FindGameObjectWithTag("e2sptext").GetComponent<Text>();
                e3_sp_text = GameObject.FindGameObjectWithTag("e3sptext").GetComponent<Text>();
                e1_sp_text.text = tbbs.enemy1.entityName;
                e2_sp_text.text = tbbs.enemy2.entityName;
                e3_sp_text.text = tbbs.enemy3.entityName;
            }
        }

        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players)
        {
            tbbs.dpMove.p1Turn();
            playerUI.SetActive(true);
            spellsUI.SetActive(false);
            commandsUI.SetActive(false);
            targetUI.SetActive(false);
            spellTargetUI.SetActive(false);
            teamTargetUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the turn is equal to the player then make blocking true
        if (tbbs.currentTurns == TurnBasedBattleSystem.turns.players && tbbs.playerIndex != 0 && blocking == true || tbbs.currentTurns == TurnBasedBattleSystem.turns.enemies && blocking == true)
        {
            spellPlayer.anim.SetBool("isBlocking", true);
            Debug.Log("Blocking is true whilst not the players turn");
        }

        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move

        //If the player is attacking set the player to move to the enemy position and attack the enemy
        if (isAttacking == true)
        {
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, target.gameObject.transform.position, step);
            if (Vector3.Distance(player.gameObject.transform.position, target.gameObject.transform.position) > 3f)
            {
                spellPlayer.anim.SetBool("isWalking", true);
            }
               
            // If the player has reached the enemy position, then play the attack animation
            if (Vector3.Distance(player.gameObject.transform.position, target.gameObject.transform.position) <= 3f)
            {
                spellPlayer.anim.SetBool("isWalking", false);
                spellPlayer.anim.Play("DWAttack");
                
            }
        } 

        //Once the player has attacked start returning to the original position
        if (isAttacking == false && playerTurn == true)
        {
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, tbbs.player1Target.gameObject.transform.position, step);
            if (Vector3.Distance(player.gameObject.transform.position, tbbs.player1Target.gameObject.transform.position) > 5f)
            {
                spellPlayer.anim.SetBool("isWalking", true);
            }

            //Once the player has reached his original position end their turn
            if (player.gameObject.transform.position == tbbs.player1Target.gameObject.transform.position && isSpellUsed == false)
            {
               
                spellPlayer.anim.SetBool("isWalking", false);
                playerTurn = false;
                spellPlayer.anim.Play("Idle");
                //tbbs.p2_turn = true;
                tbbs.p2_isAttacking = true;
                tbbs.isTargeting = true;
                EndTurnAfterAnim();
            }
            
        }
    }

    //Target Menu
    public void OpenTargetMenu()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(true);
        spellTargetUI.SetActive(false);
    }

    public void OpenSpellTargetMenu(int index)
    {
        spelldecider = index;
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(false);
        spellTargetUI.SetActive(true);
        teamTargetUI.SetActive(false);
        display.OnClickVanish();
    }

    public void OpenHealTargetMenu()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(false);
        spellTargetUI.SetActive(false);
        teamTargetUI.SetActive(true);
        display.OnClickVanish();
    }

    //Targeting Teammates
    public void GetHealTarget(int index)
    {
        target = teamMembers[index];
        teamdecider = index;
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(true);
        spellTargetUI.SetActive(false);
        teamTargetUI.SetActive(false);
        DealHealSpell();
    }

    //Targeting Different Enemies
    public void GetTargetAttack(int index)
    {
        target = enemies[index];
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(true);
        spellTargetUI.SetActive(false);
        teamTargetUI.SetActive(false);
        DealAttack();
    }
    public void GetTargetSpellAttack(int index)
    {
        target = enemies[index];
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(false);
        spellTargetUI.SetActive(true);
        teamTargetUI.SetActive(false);
        DealSpellAttack();
    }

    //Basic Attacking Scripts to test damage
    public void DealAttack()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(false);
        target.gameObject.SetActive(true);
        isAttacking = true;
        playerTurn = true;
        randomrange = Random.Range(0, 100);
        tbbs.dpMove.DisplayMove(player.entityName, "Attacks", target.entityName);
        // Debug.Log("Enemy Weapon Damage Taken: " + (player.damage, player.weaponstrength));
    }

    public void DealSpellAttack()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(false);
        target.gameObject.SetActive(true);
        playerTurn = true;
        if (spelldecider == 0)
        {
            isSpellUsed = true;
            PlayFireSpellVFX();
            // Debug.Log("Enemy Weapon Damage Taken: " + (player.damage, player.weaponstrength));
        }

        if (spelldecider == 1)
        {
            isSpellUsed = true;
            PlayRockSpellVFX();
        }

    }

    public void DealHealSpell()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(false);
        target.gameObject.SetActive(true);
        PlayHealOnceSpellVFX();
    }

    //Block function (allows the player to take a blocking stance and ends their turn)
    public void Block()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        commandsUI.SetActive(false);
        targetUI.SetActive(false);
        spellTargetUI.SetActive(false);
        blocking = true;
        player.turnblock = true;
        player.isBlocking = true;
        Debug.Log(player.isBlocking);
        player.BlockSanity();
        //tbbs.p2_turn = true;
        tbbs.p2_isAttacking = true;
        tbbs.isTargeting = true;
        spellPlayer.anim.SetBool("isBlocking", true);
        StartCoroutine(PlayerBlockWaitTime());

    }

    public IEnumerator PlayerBlockWaitTime()
    {
        yield return new WaitForSeconds(2);
        if (player.turnblock == true)
        {
            player.turnblock = false;
            tbbs.EndPlayerTurn();
        }
    }

    //Allows the player to check their list of spell moves
    public void Spell()
    {
        playerUI.SetActive(false);
        spellsUI.SetActive(true);
        commandsUI.SetActive(false);
        spellUIMenu = true;
    }


    //Allows the player to look at the current commands of his team mates
    public void Command()
    {
        commandsUI.SetActive(true);
        playerUI.SetActive(false);
        spellsUI.SetActive(false);
        targetUI.SetActive(false);
    }

    //Allows the player to go back in a menu
    public void GoBack()
    {
        playerUI.SetActive(true);
        commandsUI.SetActive(false);
        spellsUI.SetActive(false);
        targetUI.SetActive(false);
        spellTargetUI.SetActive(false);
        spellUIMenu = false;
    }

    public void TakeRockFallDamage()
    {
        Debug.Log("Player MP Before Spell: " + player.MP);
        Debug.Log("Spell MP: " + player.spellmoves[4].mpUsed);
        target.TakeSpecialDamage(target.entityWeakness[0], (player.spellmoves[4].damage), (player.spellmoves[4].spTaken), player.manastrength);
        player.MP = player.MP - player.spellmoves[4].mpUsed;
        player.SP = player.SP + player.spellmoves[4].spUsed;
    }

    public void PlayFireSkillAnim()
    {
        Debug.Log("Player MP Before Spell: " + player.MP);
        Debug.Log("Spell MP: " + player.spellmoves[0].mpUsed);
        target.TakeSpecialDamage(target.entityWeakness[0], (player.spellmoves[0].damage), (player.spellmoves[0].spTaken),player.manastrength);
        player.MP = player.MP - player.spellmoves[0].mpUsed;
        Debug.Log("PlayerMP After Spell: " + player.MP);
        player.SP = player.SP + player.spellmoves[0].spUsed;
        Debug.Log("Damage Done: " + player.spellmoves[0].damage * player.manastrength / target.manadefence);
        Debug.Log("Enemy Has taken a " + player.spellmoves[0] + " It hurt!");
        GameObject cloneobject = Instantiate(fireVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 1.5f);
        spellPlayer.anim.SetBool("isSpelling", false);
    }

    public void SmallHeal()
    {
        player.MP = player.MP - player.spellmoves[1].mpUsed;
        player.SP = player.SP - player.spellmoves[1].spUsed;
        if (tbbs.player1.HP > 0 && teamdecider == 0)
        {
            tbbs.player1.TakeHeal(player.spellmoves[1].damage);
        }

        if (tbbs.player2.HP > 0 && teamdecider == 1)
        {
            tbbs.player2.TakeHeal(player.spellmoves[1].damage);
        }

        if (tbbs.player3.HP > 0 && teamdecider == 2)
        {
            tbbs.player3.TakeHeal(player.spellmoves[1].damage);
        }

        if (tbbs.player4.HP > 0 && teamdecider == 3)
        {
            tbbs.player4.TakeHeal(player.spellmoves[1].damage);
        }
    }

    public void BigHeal()
    {
        player.MP = player.MP - player.spellmoves[2].mpUsed;
        player.SP = player.SP - player.spellmoves[2].spUsed;
        if (tbbs.player1.HP > 0)
        {
            tbbs.player1.TakeHeal(player.spellmoves[2].damage);
        }

        if (tbbs.player2.HP > 0)
        {
            tbbs.player2.TakeHeal(player.spellmoves[2].damage);
        }

        if (tbbs.player3.HP > 0)
        {
            tbbs.player3.TakeHeal(player.spellmoves[2].damage);
        }

        if (tbbs.player4.HP > 0)
        {
            tbbs.player4.TakeHeal(player.spellmoves[2].damage);
        }
    }

    public void SanityRegen()
    {
        player.SanityRegen();
    }

    public void SmallHealAnim()
    {
        GameObject cloneobject = Instantiate(s_healVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void BigHealAnim()
    {
        GameObject cloneobject = Instantiate(b_healVFX, bigHealspot.transform.position, bigHealspot.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void SanityRegenAnim()
    {
        GameObject cloneobject = Instantiate(sanityVFX, player.transform.position, player.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }

    public void RockFallAnim()
    {
        GameObject cloneobject = Instantiate(rockVFX, target.transform.position, target.transform.rotation);
        Destroy(cloneobject, 2.5f);
    }


    public void AttackDamageDealt()
    {
        player.WeaponSanity();
        //isAttacking = false;
        tbbs.dpMove.DamageDisplay(target.entityName, "Took", "Damage!");
        target.TakeWeaponDamage(player.damage, player.weaponstrength);
        Debug.Log("Enemy Weapon Damage Taken: " + (player.damage, player.weaponstrength));
    }

    public void PlayFireSpellVFX()
    {

        if (player.MP >= player.spellmoves[0].mpUsed)
        {
            //playerUI.SetActive(false);
            //spellsUI.SetActive(false);
            //spellUIMenu = false;
            spellPlayer.anim.SetBool("isSpelling", true);
            spellsUI.SetActive(false);
            spellTargetUI.SetActive(false);
            display.OnClickVanish();

        } else
        {
            GoBack();
        }
        //spellLifetime = 3;
        //Destroy(fireVFX, 3);

    }

    public void PlayHealOnceSpellVFX()
    {

        if (player.MP >= player.spellmoves[1].mpUsed)
        {
            //playerUI.SetActive(false);
            //spellsUI.SetActive(false);
            //spellUIMenu = false;
            spellPlayer.anim.SetBool("isHealing", true);
            spellsUI.SetActive(false);
            spellTargetUI.SetActive(false);
            display.OnClickVanish();


        }
        else
        {
            GoBack();
        }
        //spellLifetime = 3;
        //Destroy(fireVFX, 3);

    }

    public void PlayHealAllSpellVFX()
    {

        if (player.MP >= player.spellmoves[2].mpUsed)
        {
            //playerUI.SetActive(false);
            //spellsUI.SetActive(false);
            //spellUIMenu = false;
            spellPlayer.anim.SetBool("isBigHealing", true);
            spellsUI.SetActive(false);
            spellTargetUI.SetActive(false);
            display.OnClickVanish();

        }
        else
        {
            GoBack();
        }
        //spellLifetime = 3;
        //Destroy(fireVFX, 3);

    }

    public void PlaySanityVFX()
    {

        if (player.MP >= player.spellmoves[0].mpUsed)
        {
            //playerUI.SetActive(false);
            //spellsUI.SetActive(false);
            //spellUIMenu = false;
            spellPlayer.anim.SetBool("isSanityRegen", true);
            spellsUI.SetActive(false);
            spellTargetUI.SetActive(false);
            display.OnClickVanish();

        }
        else
        {
            GoBack();
        }
        //spellLifetime = 3;
        //Destroy(fireVFX, 3);

    }

    public void PlayRockSpellVFX()
    {

        if (player.MP >= player.spellmoves[3].mpUsed)
        {
            //playerUI.SetActive(false);
            //spellsUI.SetActive(false);
            //spellUIMenu = false;
            spellPlayer.anim.SetBool("isRock", true);
            spellsUI.SetActive(false);
            spellTargetUI.SetActive(false);
            display.OnClickVanish();
        }
        else
        {
            GoBack();
        }
        //spellLifetime = 3;
        //Destroy(fireVFX, 3);

    }

    public void EndTurnAfterAnim()
    {
        spellPlayer.anim.SetBool("isSpelling", false);
        spellPlayer.anim.SetBool("isSanityRegen", false);
        spellPlayer.anim.SetBool("isHealing", false);
        spellPlayer.anim.SetBool("isBigHealing", false);
        spellPlayer.anim.SetBool("isRock", false);
        tbbs.EndPlayerTurn();
    }

    public void WalkBack()
    {
        Debug.Log("Walk To Original Position");
        isAttacking = false;
        spellPlayer.anim.Play("Walk");
    }

    public void Escape()
    {
        Debug.Log("You Tried To Escape");
        SceneManager.LoadScene(0);
        
    }

    void FootL()
    {

    }

    void FootR()
    {

    }
}
