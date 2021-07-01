using System;
using UnityEngine.UI;
using UnityEngine;

public class UI_SkillBar : MonoBehaviour
{
    private PlayerSkills _playerSkills;

    private void Awake()
    {
        _playerSkills = GameObject.Find("player").GetComponent<PlayerSkills>();
    }
}
