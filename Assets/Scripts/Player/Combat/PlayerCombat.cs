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
    
    private Dictionary<Spell,float> _spellCooldowns;
    
    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _player = GetComponent<Player>();

        _playerSkills = GetComponent<PlayerSkills>();

        _camera = transform.Find("player_camera").GetComponent<Camera>();
        
        _spellCooldowns = new Dictionary<Spell, float>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void UseSpell(Spell toCast)
    {
        //Calculate direction
        if (IsOnCooldown(toCast))
            return;

        //get type of shooting
        if (toCast.CastingMode == CastingMode.Instant)
        {
            if (!_player.Mana.TryTake(toCast.ManaCost))
                return;
            Vector3 direction = CalculateDirection();
            toCast.Cast(_attackPoint.position,direction);
            if(toCast.Cooldown > 0f)
                StartCoroutine(StartCooldown(toCast));
        }
        else if (toCast.CastingMode == CastingMode.Chanelling)
        {
            StartCoroutine(SpellChannelling(toCast));
        }
        else if (toCast.CastingMode == CastingMode.Charging)
        {
            if (!_player.Mana.TryTake(toCast.ManaCost))
                return;
            StartCoroutine(SpellCharging(toCast));
        }
        //shoot projectile
    }

    
    
    private IEnumerator SpellChannelling(Spell spell)
    {
        float castingRate = spell.CastringModeParameter;
        while (_player.Mana.TryTake(spell.ManaCost/castingRate) && Input.GetButton("Fire1"))
        {
            Vector3 direction = CalculateDirection();
            spell.Cast(_attackPoint.position,direction);
            yield return new WaitForSeconds(1f/castingRate);
        }
        if(spell.Cooldown > 0f)
            StartCoroutine(StartCooldown(spell));
    }

    private IEnumerator SpellCharging(Spell spell)
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
        Vector3 direction = CalculateDirection();
        spell.Cast(_attackPoint.position,direction,Mathf.Clamp01(charge/secondsToCharge));
        if(spell.Cooldown > 0f)
            StartCoroutine(StartCooldown(spell));
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
        
        if (Input.GetKey(KeyCode.Q))
            UseSpell(_playerSkills.GetSpell(0));  
        if (Input.GetKey(KeyCode.R))
            UseSpell(_playerSkills.GetSpell(1));  
        if (Input.GetKey(KeyCode.X))
            UseSpell(_playerSkills.GetSpell(2));



    }

    private IEnumerator StartCooldown(Spell spell)
    {
        float currCooldown = spell.Cooldown;
        _spellCooldowns[spell] = currCooldown;
        Debug.Log($"Starting cooldown {currCooldown} of spell: {spell.Name}");
        while (currCooldown > 0f)
        {
            yield return new WaitForSeconds(0.1f);
            currCooldown -= 0.1f;
            _spellCooldowns[spell] = currCooldown;
        }
        Debug.Log($"Ending cooldown {currCooldown} of spell: {spell.Name}");
    }
    
    private bool IsOnCooldown(Spell spell)
    {
        return _spellCooldowns.ContainsKey(spell) && _spellCooldowns[spell] > 0f;
    }
    
    
}
 