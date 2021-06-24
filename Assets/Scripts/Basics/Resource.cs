using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
public class Resource
{

    private string _name; // ??
    private float _maxValue;
    private float _value;

    private float _regenPerSec;
    
    public event EventHandler OnResourceChange;
    
    public Resource(float maxValue = 100f, string name = "defaultName")
    {
        _maxValue = maxValue;
        _value = maxValue;
        _name = name;
    }

    public void IncreaseMax(float newMax)
    {
        if (newMax <= 0f)
            throw new ArgumentException();
        _maxValue = newMax;
        OnResourceChange?.Invoke(this, new EventArgs());
    }

    public void Add(float amount)
    {
        if (amount < 0f)
            return;
        _value = Mathf.Min(_value + amount, _maxValue);
        OnResourceChange?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// Taking amount from resource.
    /// </summary>
    /// <param name="amount">Amount</param>
    /// <returns> False if value is above zero, true if value after sub is below zero.</returns>
    public bool Take(float amount)
    {
        if (amount <= 0f)
            amount = 0f;
        _value = Mathf.Max(_value - amount, 0f);
        OnResourceChange?.Invoke(this, new EventArgs());
        return _value <= 0f;
    }

    /// <summary>
    /// Try to take amount from resource and if there is enough of resource, take it.
    /// E.g. for mana usage. 
    /// </summary>
    /// <param name="amount">Amount</param>
    /// <returns>False if amount is negative or not enough resource. True if is enough of resource.</returns>
    public bool TryTake(float amount)
    {
        if (amount < 0f)
            return false;
        if (amount > _value)
            return false;
        _value = Mathf.Max(_value - amount, 0f);
        OnResourceChange?.Invoke(this, new EventArgs());
        return true;
    }

    private readonly int REGEN_TIMES_PER_SEC = 2;
    private IEnumerator Regen()
    {
        while (_value < _maxValue && _regenPerSec > 0f)
        {
            float toAdd = _regenPerSec / REGEN_TIMES_PER_SEC;
            Add(toAdd);
            yield return new WaitForSeconds(1f/REGEN_TIMES_PER_SEC);
        }
    }
    
    public float MaxValue => _maxValue;
    public float Value => _value;
    public float Percentage => (_value / _maxValue) * 100;
    public string Name => _name;


}