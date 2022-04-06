using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class OverworldSystem : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject playermenuUI;
    public GameObject ItemsUI;
    public GameObject itemPickerUI;
    public GameObject teamtargetUI;
    public GameObject weaponChangeButtonUI;
    public GameObject retireUI;
    public int teamdecider;
    public DisplayMoves dpMove;


    //Items
    public GameObject smallHealItemUI;
    public GameObject smallManaItemUI;
    public GameObject smallSanityItemUI;
    public GameObject mediumHealItemUI;
    public GameObject mediumManaItemUI;
    public GameObject mediumSanityItemUI;
    public GameObject mediumHealAllUI;
    public GameObject mediumMPAllUI;
    public GameObject mediumSPAllUI;

    //ItemText
    public Text smallHealText;
    public Text smallManaText;
    public Text smallSanityText;
    public Text mediumHealText;
    public Text mediumManaText;
    public Text mediumSanityText;
    public Text mediumHealAllText;
    public Text mediumMPAllText;
    public Text mediumSPAllText;

    public Text p1_item_text;
    public Text p2_item_text;
    public Text p3_item_text;
    public Text p4_item_text;


    //Players
    public BaseEntities player1;
    public BaseEntities player2;
    public BaseEntities player3;
    public BaseEntities player4;



    public bool inMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playermenuUI.SetActive(false);
        ItemsUI.SetActive(false);
        itemPickerUI.SetActive(false);
        teamtargetUI.SetActive(false);
        weaponChangeButtonUI.SetActive(false);
        itemPickerUI.SetActive(false);
        p1_item_text.text = player1.entityName;
        p2_item_text.text = player2.entityName;
        p3_item_text.text = player3.entityName;
        p4_item_text.text = player4.entityName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inMenu == false)
        {
            playermenuUI.SetActive(true);
            ItemsUI.SetActive(true);
            weaponChangeButtonUI.SetActive(true);
            retireUI.SetActive(true);
            itemPickerUI.SetActive(false);
            dpMove.MenuDisplay("Welcome to the Menu");
            inMenu = true;
        }


        //ITEM BUTTONS MANAGEMENT
        if (gameManager.smallHealItem == 0)
        {
            smallHealItemUI.SetActive(false);
        } else
        {
            smallHealItemUI.SetActive(true);
        }

        if (gameManager.smallManaItem == 0)
        {
            smallManaItemUI.SetActive(false);
        }
        else
        {
            smallManaItemUI.SetActive(true);
        }

        if (gameManager.smallSanityItem == 0)
        {
            smallSanityItemUI.SetActive(false);
        }
        else
        {
            smallSanityItemUI.SetActive(true);
        }

        if (gameManager.mediumHealItem == 0)
        {
            mediumHealItemUI.SetActive(false);
        }
        else
        {
            mediumHealItemUI.SetActive(true);
        }

        if (gameManager.mediumManaItem == 0)
        {
            mediumManaItemUI.SetActive(false);
        }
        else
        {
            mediumManaItemUI.SetActive(true);
        }

        if (gameManager.mediumSanityItem == 0)
        {
            mediumSanityItemUI.SetActive(false);
        }
        else
        {
            mediumSanityItemUI.SetActive(true);
        }

        if (gameManager.mediumHealAll == 0)
        {
            mediumHealAllUI.SetActive(false);
        }
        else
        {
            mediumHealAllUI.SetActive(true);
        }

        if (gameManager.mediumManaAll == 0)
        {
            mediumMPAllUI.SetActive(false);
        }
        else
        {
            mediumMPAllUI.SetActive(true);
        }

        if (gameManager.mediumSanityAll == 0)
        {
            mediumSPAllUI.SetActive(false);
        }
        else
        {
            mediumSPAllUI.SetActive(true);
        }

        smallHealText.text = "Small Heal x" + gameManager.smallHealItem;
        smallManaText.text = "Small Mana x" + gameManager.smallManaItem;
        smallSanityText.text = "Small Sanity x" + gameManager.smallSanityItem;
        mediumHealText.text = "Medium Heal x" + gameManager.mediumHealItem;
        mediumManaText.text = "Medium Mana x" + gameManager.mediumManaItem;
        mediumSanityText.text = "Medium Sanity x" + gameManager.mediumSanityItem;
        mediumHealAllText.text = "Medium Heal All x" + gameManager.mediumHealAll;
        mediumMPAllText.text = "Medium Mana All x" + gameManager.mediumManaAll;
        mediumSPAllText.text = "Medium Sanity All x" + gameManager.mediumSanityAll;

    }

    public void ItemOpen()
    {
        playermenuUI.SetActive(false);
        ItemsUI.SetActive(false);
        teamtargetUI.SetActive(true);
        weaponChangeButtonUI.SetActive(false);
        dpMove.MenuDisplay("Select A Player To Heal");
    }

    public void CloseMenu()
    {
        playermenuUI.SetActive(false);
        ItemsUI.SetActive(false);
        teamtargetUI.SetActive(false);
        weaponChangeButtonUI.SetActive(false);
        itemPickerUI.SetActive(false);
        dpMove.MenuDisplay("");
        inMenu = false;
    }

    public void GetItemTarget(int index)
    {
        teamdecider = index;
        playermenuUI.SetActive(false);
        ItemsUI.SetActive(false);
        teamtargetUI.SetActive(false);
        weaponChangeButtonUI.SetActive(false);
        itemPickerUI.SetActive(true);
        dpMove.MenuDisplay("Choose your item");
    }

    public void GoBack()
    {
        playermenuUI.SetActive(true);
        ItemsUI.SetActive(true);
        teamtargetUI.SetActive(false);
        weaponChangeButtonUI.SetActive(true);
        retireUI.SetActive(true);
        itemPickerUI.SetActive(false);
        dpMove.MenuDisplay("Welcome to the Menu");
    }

    public void smallHealItem()
    {
        gameManager.smallHealItem -= 1;
        if (teamdecider == 0)
        {
            player1.HP += 15;
        } else if (teamdecider == 1)
        {
            player2.HP += 15;
        } else if (teamdecider == 2)
        {
            player3.HP += 15;
        } else if (teamdecider == 3)
        {
            player4.HP += 15;
        }
    }

    public void smallManaItem()
    {
        gameManager.smallManaItem -= 1;
        if (teamdecider == 0)
        {
            player1.MP += 15;
        }
        else if (teamdecider == 1)
        {
            player2.MP += 15;
        }
        else if (teamdecider == 2)
        {
            player3.MP += 15;
        }
        else if (teamdecider == 3)
        {
            player4.MP += 15;
        }
    }

    public void smallSanityItem()
    {
        gameManager.smallSanityItem -= 1;
        if (teamdecider == 0)
        {
            player1.SP += 15;
        }
        else if (teamdecider == 1)
        {
            player2.SP += 15;
        }
        else if (teamdecider == 2)
        {
            player3.SP += 15;
        }
        else if (teamdecider == 3)
        {
            player4.SP += 15;
        }
    }

    public void mediumHealItem()
    {
        gameManager.mediumHealItem -= 1;
        if (teamdecider == 0)
        {
            player1.HP += 30;
        }
        else if (teamdecider == 1)
        {
            player2.HP += 30;
        }
        else if (teamdecider == 2)
        {
            player3.HP += 30;
        }
        else if (teamdecider == 3)
        {
            player4.HP += 30;
        }
    }

    public void mediumManaItem()
    {
        gameManager.mediumManaItem -= 1;
        if (teamdecider == 0)
        {
            player1.MP += 30;
        }
        else if (teamdecider == 1)
        {
            player2.MP += 30;
        }
        else if (teamdecider == 2)
        {
            player3.MP += 30;
        }
        else if (teamdecider == 3)
        {
            player4.MP += 30;
        }
    }

    public void mediumSanityItem()
    {
        gameManager.mediumSanityItem -= 1;
        if (teamdecider == 0)
        {
            player1.SP += 30;
        }
        else if (teamdecider == 1)
        {
            player2.SP += 30;
        }
        else if (teamdecider == 2)
        {
            player3.SP += 30;
        }
        else if (teamdecider == 3)
        {
            player4.SP += 30;
        }
    }

    public void mediumHealAllItem()
    {
        gameManager.mediumHealAll -= 1;
        player1.HP += 30;
        player2.HP += 30;
        player3.HP += 30;
        player4.HP += 30;
    }

    public void mediumManaAllItem()
    {
        gameManager.mediumManaAll -= 1;
        player1.MP += 30;
        player2.MP += 30;
        player3.MP += 30;
        player4.MP += 30;
    }

    public void mediumSanityAllItem()
    {
        gameManager.mediumSanityAll -= 1;
        player1.SP += 30;
        player2.SP += 30;
        player3.SP += 30;
        player4.SP += 30;
    }
}
