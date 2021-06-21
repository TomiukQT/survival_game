using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum ShootingMode
{
    Manual,
    Automatic,
    Charging
}

public class PlayerCombat : MonoBehaviour
{
    private PlayerStats _playerStats;
    private PlayerResources _playerResources;

    private PlayerSkills _playerSkills;

    private Transform _attackPoint;
    
    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerResources = GetComponent<PlayerResources>();

        _playerSkills = GetComponent<PlayerSkills>();

    }

    private void Update()
    {
        CheckInput();
    }

    private void Shoot()
    {
        //get projectile
        GameObject projectile = _playerSkills.GetSkillProjectile();
        //get type of shooting
        //get cost and try to take resources
        //shoot projectile
    }
    
    private void CheckInput()
    {
        if(Input.GetKeyDown("Fire!"))
            Shoot();
        
    }
    
    
}