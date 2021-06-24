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

    [SerializeField] private Transform _attackPoint;

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
        if (!_player.Mana.TryTake(toCast.ManaCost))
            return;
        
        
        //Calculate direction
        Vector3 direction = CalculateDirection();

        //get type of shooting
        toCast.Cast(_attackPoint.position,direction);
        //shoot projectile
        
    }

    private Vector3 CalculateDirection()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        Vector3 direction = (targetPoint - _attackPoint.position).normalized;
        return direction;
    }
    
    private void CheckInput()
    {
        if(Input.GetButtonDown("Fire1"))
            UseSpell();
        
    }
    
    
}
