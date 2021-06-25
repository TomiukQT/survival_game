using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillTreeSlot
{
    public Spell Spell;
    public List<Spell> Dependencies;
    public bool IsUnlocked;
    public int CostToUnlock = 0;
}

public class SkillTree : MonoBehaviour
{
    [SerializeField] private string _name;
    
    [SerializeField] private List<SkillTreeSlot> _slots;

    //TEMP
    [SerializeField] private int _skillPoints;
    
    public bool TryUnlockSkill(Spell spellToUnlock)
    {
        SkillTreeSlot slot;
        if (!GetSlot(spellToUnlock, out slot))
            return false;
        if (slot.CostToUnlock > _skillPoints)
            return false;
        _skillPoints -= slot.CostToUnlock;
        slot.IsUnlocked = true;
        return true;
    }

    public bool TryGetSkill(out Spell spellToGet)
    {
        spellToGet = null;
        SkillTreeSlot slot;
        if (!GetSlot(spellToGet, out slot))
            return false;
        if (!slot.IsUnlocked)
            return false;
        spellToGet = slot.Spell;
        return true;
    }

    private bool GetSlot(Spell spell, out SkillTreeSlot skillTreeSlot)
    {
        skillTreeSlot = null;
        foreach (var slot in _slots)
        {
            if (slot.Spell == spell)
            {
                skillTreeSlot = slot;
                return true;
            }
        }

        return false;
    }

    private bool CheckSkillDependencies(Spell spellToCheck)
    {
        SkillTreeSlot slot;
        if (!GetSlot(spellToCheck, out slot))
            return false;
        foreach (var dependency in slot.Dependencies)
        {
            SkillTreeSlot s;
            if (!GetSlot(dependency,out s))
                return false;
            if (!s.IsUnlocked)
                return false;

        }
        return true;
    }
    
}
