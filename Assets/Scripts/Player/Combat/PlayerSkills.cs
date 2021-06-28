﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSkills : MonoBehaviour
{

    [SerializeField] private HashSet<Spell> _unlockedSkills;

    [SerializeField] private SkillsDependencies _skillsDependencies;
    
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
        if (!CheckDependencies(spell))
            return;
        _unlockedSkills.Add(spell);
        Debug.Log($"Spell {spell.Name} has beem unlocked");
    }

    private bool CheckDependencies(Spell spell)
    {
        var dependencies = _skillsDependencies.GetDependencies(spell);
        if (dependencies == null)
            return true;
        return dependencies.ToList().TrueForAll(x => IsSkillUnlocked(x));
    }

    public bool IsSkillUnlocked(Spell spell) => _unlockedSkills.Contains(spell);
    

}

[System.Serializable]
public class SkillsDependencies
{
    public List<Dependency> _dependencyList;

    public IEnumerable<Spell> GetDependencies(Spell spell)
    {
        foreach (var dep in _dependencyList)
        {
            if (dep.spell == spell)
                return dep.dependencies;
        }
        return null;
    }
    
    [System.Serializable]
    public struct Dependency
    {
        public Spell spell;
        public List<Spell> dependencies;

    }

}