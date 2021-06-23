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

    private Camera _camera;
    
    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _player = GetComponent<Player>();

        _playerSkills = GetComponent<PlayerSkills>();

        _camera = transform.Find("player_camera").GetComponent<Camera>();

    }

    private void Update()
    {
        CheckInput();
    }

    private void UseSpell()
    {
        Spell toCast = _playerSkills.GetSpell();
       
        //get cost and try to take resources
        
        
        //Calculate direction
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        Vector3 direction = (targetPoint - _attackPoint.position).normalized;

        //get type of shooting

        //shoot projectile

    }

    private Vector3 CalculateDirection()
    {
        return new Vector3(1, 0, 0);
    }
    
    private void CheckInput()
    {
        if(Input.GetKeyDown("Fire!"))
            UseSpell();
        
    }
    
    
}
