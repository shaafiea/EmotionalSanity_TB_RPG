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
}
