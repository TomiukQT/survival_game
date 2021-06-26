using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillTreeSlot
{
    public Spell Spell;
    public List<Spell> Dependencies;
    public int CostToUnlock = 0;
}

[CreateAssetMenu(fileName = "New Skill Tree", menuName = "Skill Tree")]
public class SkillTree : ScriptableObject
{

    public string name;
    public Image icon;
    public List<SkillTreeSlot> slots;
}
