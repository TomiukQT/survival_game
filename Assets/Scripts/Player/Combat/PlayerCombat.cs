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
    
    [SerializeField] private Spell toCast;
    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _player = GetComponent<Player>();

        _playerSkills = GetComponent<PlayerSkills>();

        _camera = transform.Find("player_camera").GetComponent<Camera>();
        
        toCast = _playerSkills.GetSpell();
    }

    private void Update()
    {
        CheckInput();
    }

    private void UseSpell(Spell toCast)
    {
        //Calculate direction
        Vector3 direction = CalculateDirection();

        //get type of shooting
        if (toCast.CastingMode == CastingMode.Instant)
        {
            if (!_player.Mana.TryTake(toCast.ManaCost))
                return;
            toCast.Cast(_attackPoint.position,direction);
        }
        else if (toCast.CastingMode == CastingMode.Chanelling)
        {
            StartCoroutine(SpellChannelling(toCast,direction));
        }
        else if (toCast.CastingMode == CastingMode.Charging)
        {
            if (!_player.Mana.TryTake(toCast.ManaCost))
                return;
            StartCoroutine(SpellCharging(toCast, direction));
        }
        //shoot projectile
        
    }

    
    
    private IEnumerator SpellChannelling(Spell spell, Vector3 direction)
    {
        float castingRate = spell.CastringModeParameter;
        while (_player.Mana.TryTake(spell.ManaCost/castingRate) && Input.GetButton("Fire1"))
        {
            spell.Cast(_attackPoint.position,direction);
            yield return new WaitForSeconds(1f/castingRate);
        }
    }

    private IEnumerator SpellCharging(Spell spell, Vector3 direction)
    {
        float secondsToCharge = spell.CastringModeParameter;
        float charge = 0f;
        while (Input.GetButton("Fire1"))
        {
            charge += .1f;
            if(charge >= secondsToCharge)
                break;
            yield return new WaitForSeconds(.1f);
        }
        spell.Cast(_attackPoint.position,direction,Mathf.Clamp01(charge/secondsToCharge));
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
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            toCast = _playerSkills.GetSpell(0);  
        if (Input.GetKeyDown(KeyCode.Alpha2))
            toCast = _playerSkills.GetSpell(1);  
        if (Input.GetKeyDown(KeyCode.Alpha3))
            toCast = _playerSkills.GetSpell(2);

        if (Input.GetButtonDown("Fire1") && toCast != null)
            UseSpell(toCast);

    }
    
    
}
