using System.Collections;
using UnityEngine;

[System.Serializable]
public class Stat
{
    private string _name;
    private int _value;
    //private int _initialValue;
    
    public Stat(int value, string name = "defautlName")
    {
        _value = value;
        
        _name = name;
    }

    public void Increase(int amount = 1)
    {
        if (amount < 0)
            return;
        _value += amount;
    }

    public void Decrease(int amount = 1)
    {
        if (amount < 0 && _value - amount < 0)
            return;
        _value -= amount;
    }

    public void ChangeStatTemporary(int newValue, float time)
    {
        if(newValue < 0 || time < 0f)
            return;
    }

    private IEnumerator ChangeStatOnTime(int newValue, float time)
    {
        yield return new WaitForSeconds(time);
    }

    public int Value => _value;
    public string Name => _name;

}