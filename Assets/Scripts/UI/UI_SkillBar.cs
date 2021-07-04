using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_SkillBar : MonoBehaviour
{
    private PlayerSkills _playerSkills;
    private List<UI_SkillBarSlot> _skillBarSlots;
    
    private void Awake()
    {
        _playerSkills = GameObject.Find("player").GetComponent<PlayerSkills>();
        InitSkillBarSlots();
        
        
    }

    private void InitSkillBarSlots()
    {
        _skillBarSlots = new List<UI_SkillBarSlot>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _skillBarSlots.Add(transform.GetChild(i).GetComponent<UI_SkillBarSlot>());
            _skillBarSlots[i].OnSpellDrop += e_OnSpellDrop;
        }
    }

    private void e_OnSpellDrop(object sender, OnSpellDropEventArgs e)
    {
        _playerSkills.SetSpellToBar(e.DroppedSpell, e.Index);
    }
}
