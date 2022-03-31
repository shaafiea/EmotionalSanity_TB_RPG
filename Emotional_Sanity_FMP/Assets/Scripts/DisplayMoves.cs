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

    public void DamageDisplay(string player, string description, string damage)
    {
        displayText.text = player + " " + description + " " + damage;
    }

    public void p1Turn()
    {
        displayText.text =  "Its Player 1s Turn";
    }
}
