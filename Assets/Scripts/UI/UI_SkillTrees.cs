using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class UI_SkillTrees : MonoBehaviour, IShowable
{
    [SerializeField] private GameObject _skillTreePanel;

    [SerializeField] private GameObject _skillButtonPrefab;

    private PlayerSkills _playerSkills;

    private void Awake()
    {
        _playerSkills = GameObject.Find("player").GetComponent<PlayerSkills>();
    }

    public void SetActive(bool state) => _skillTreePanel.SetActive(state);
    public void Toggle() => _skillTreePanel.SetActive(!_skillTreePanel.activeSelf);
    
    
    public void OnSpellButtonClick(Spell spell)
    {
        _playerSkills.UnlockSkill(spell);
    }
    

}
