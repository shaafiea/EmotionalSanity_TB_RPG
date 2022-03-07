using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class 
    DisplayInfo : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public bool isSpell = true;
    public EntityMoves spell = null;
    [SerializeField] private DisplayMoves displayMoveScript = null;
    [SerializeField] private TextMeshProUGUI spellNameText = null;

    private void Start()
    {
        displayMoveScript = GameObject.Find("Info Text (TMP)").GetComponent<DisplayMoves>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSpell)
        {
            displayMoveScript.DisplayMove(spell.entityDescription);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        displayMoveScript.DisplayMove("");
    }
    
}
