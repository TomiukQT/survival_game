using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
    
    private Resource _health = new Resource(100f, "Health");
    private Resource _mana = new Resource(100f, "Mana");

    private PlayerMovement _playerMovement;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Die()
    {
        Debug.Log("Player died!!!");
    }
    
    public void TakeDamage(float amount)
    {
        if (_health.Take(amount))
            Die();
    }

    public void ChangeSpeed(float percentage)
    {
        _playerMovement.ChangeSpeed(percentage);
    }

    public void Stun()
    {
        _playerMovement.Stun(1f);
    }

    public void ApplyDOT(float amount)
    {
        
    }

    public IEnumerable DOT(float amount)
    {
        TakeDamage(amount);
        yield return new WaitForSeconds(1f);
        TakeDamage(amount);
    }
}