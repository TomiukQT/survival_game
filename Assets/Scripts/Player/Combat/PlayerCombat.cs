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
    private Player _player;

    private PlayerSkills _playerSkills;

    private Transform _attackPoint;
    
    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _player = GetComponent<Player>();

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
        Vector3 direction = CalculateDirection();
        GameObject bullet = Instantiate(projectile, _attackPoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Setup(direction);
    }

    private Vector3 CalculateDirection()
    {
        return new Vector3(1, 0, 0);
    }
    
    private void CheckInput()
    {
        if(Input.GetKeyDown("Fire!"))
            Shoot();
        
    }
    
    
}
