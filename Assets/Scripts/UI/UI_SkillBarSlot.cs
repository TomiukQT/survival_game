using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SkillBarSlot : MonoBehaviour, IDropHandler
{
    private Image _image;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        //Debug.Log(eventData.pointerDrag);

        Spell spell = eventData.pointerDrag.GetComponent<UI_SkillButton>().Spell;
        //_image = spell.icon;
        Debug.Log($"Spell {spell.Name} was dropped to slot {gameObject}");
    }
}
