using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour,ICharacter
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

    public void Stun(float duration)
    {
        _playerMovement.Stun(duration);
    }

    public void ApplyDOT(float amount,float duration)
    {
        StartCoroutine(DamageOverTime(amount, duration));
    }

    public IEnumerator DamageOverTime(float amount, float duration)
    {
        for (int i = 0; i < Mathf.FloorToInt(duration); i++)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(amount);
        }
    }
    
    //Getters
    public Resource Health => _health;
    public Resource Mana => _mana;

}