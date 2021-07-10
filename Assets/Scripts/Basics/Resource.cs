﻿using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

//Maybe useful in UI
//public class OnResourceChangeEventArgs : EventArgs
//{
//    public bool IsRegenerating;
//}

[System.Serializable]
public class Resource
{

    private string _name; // ??
    private float _maxValue;
    private float _value;

    private float _regenPerSec;
    private bool _isRegenerating = false;
    
    public event EventHandler OnResourceChange;

    public Resource(float maxValue = 100f, string name = "defaultName", float regenPerSec = 0f)
    {
        _maxValue = maxValue;
        _value = maxValue;
        _name = name;
        _regenPerSec = regenPerSec;
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
        StartRegen();
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
        Take(amount);
        return true;
    }

    private void StartRegen()
    {
        if (!_isRegenerating && _value != _maxValue)
#pragma warning disable 4014
            Regen();
#pragma warning restore 4014
    }
    

    private readonly int REGEN_TIMES_PER_SEC = 2;
    private async Task Regen()
    {
        _isRegenerating = true;
        while (_value < _maxValue && _regenPerSec > 0f)
        {
            await Task.Delay((int)((1f/REGEN_TIMES_PER_SEC) * 1000f));
            float toAdd = _regenPerSec / REGEN_TIMES_PER_SEC;
            Add(toAdd);
        }

        _isRegenerating = false;
    }
    
    public float MaxValue => _maxValue;
    public float Value => _value;
    public float Percentage => (_value / _maxValue) * 100;
    public float Percentage01 => (_value / _maxValue);
    public string Name => _name;


}