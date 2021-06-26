using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class UI_SkillTrees : MonoBehaviour, IShowable
{
    [SerializeField] private GameObject _skillTreePanel;

    [SerializeField] private GameObject _skillButtonPrefab;
    
    private void Awake()
    {
        
    }

    public void SetActive(bool state) => _skillTreePanel.SetActive(state);
    public void Toggle() => _skillTreePanel.SetActive(!_skillTreePanel.activeSelf);


}
