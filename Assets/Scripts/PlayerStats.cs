using System;
using UnityEngine;

public class OnLevelUpEventArgs : EventArgs
{
    public int newLvl;
}


public class PlayerStats : MonoBehaviour
{

    private Resource _health = new Resource(100f, "Health");
    private Resource _mana = new Resource(100f, "Mana");

    private Stat _stamina = new Stat(10, "Stamina");
    private Stat _intellect = new Stat(10, "Intellect");
    
    //XP AND LVL
    private readonly int XP_FACTOR = 100;
    private float _xp;
    private int _maxXp = 100;
    private int _level = 1;

    public event EventHandler<OnLevelUpEventArgs> OnLevelUp;

    public void AddXP(float amount)
    {
        _xp += amount;
        if (_xp >= _maxXp)
        {
            _xp -= _maxXp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _level++;
        _maxXp = Utils.Fib(_level) * XP_FACTOR;
        
        OnLevelUp?.Invoke(this, new OnLevelUpEventArgs(){newLvl = _level});
    }

}