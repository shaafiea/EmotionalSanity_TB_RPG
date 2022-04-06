using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayMoves : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText = null;

    public void DisplayMove(string description)
    {
        displayText.text = description;
    }

    public void DisplayMove(string player, string description, string target)
    {
        displayText.text = player + " " + description + " " + target;
    }

    public void DisplaySpellMove(string player, string description, string spell, string target)
    {
        displayText.text = player + " " + description + " " + spell + " On " + target;
    }

    public void DamageDisplay(string player, string description, string damage)
    {
        displayText.text = player + " " + description + " " + damage;
    }
    public void SanityRegenDisplay(string player)
    {
        displayText.text = player + " is regenerating their sanity!";
    }

    public void BlockDisplay(string player)
    {
        displayText.text = player + " is blocking!";
    }

    public void HealOneDisplay(string player, string target)
    {
        displayText.text = player + " is healing " + target; 
    }

    public void HeallAllDisplay(string player)
    {
        displayText.text = player + " is healing everyone on their side!";
    }

    public void MissDisplay(string player)
    {
        displayText.text = player + " missed their attack!";
    }

    public void p1Turn()
    {
        displayText.text =  "Its Player 1s Turn";
    }

    public void MenuDisplay(string description)
    {
        displayText.text = description;
    }
}
