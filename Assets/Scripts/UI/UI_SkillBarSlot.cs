using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SkillBarSlot : MonoBehaviour, IDropHandler
{
    private Image _image;

    private void Awake()
    {
        _image = transform.Find("icon").GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        //Debug.Log(eventData.pointerDrag);

        Spell spell = eventData.pointerDrag.GetComponent<UI_SkillButton>().Spell;
        _image.sprite = spell.Icon;
        Debug.Log($"Spell {spell.Name} was dropped to slot {gameObject}");
    }

    public void OnSpellCast(float cooldown)
    {
        
    }
    
}
