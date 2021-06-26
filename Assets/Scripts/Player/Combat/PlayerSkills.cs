using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSkills : MonoBehaviour
{

    [SerializeField] private HashSet<Spell> _unlockedSkills;

    [SerializeField] private List<Spell> _allSpells;

    //TEMP
    //[SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Spell _tempSpell;
    public Spell GetSpell() => _tempSpell;
    //TEMP
    
    
    private void Awake()
    {
        _unlockedSkills = new HashSet<Spell>();
    }
    
    public void UnlockSkill(Spell spell)
    {
        _unlockedSkills.Add(spell);
    }

    public void IsSkillUnlocked(Spell spell) => _unlockedSkills.Contains(spell);
    

}