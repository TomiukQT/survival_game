using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items")]
public class Item : ScriptableObject
{
    
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private bool _stackable = false;
    [SerializeField] private int _count = 1;

    public bool IsStackable => _stackable;
    public int Count
    {
        get => _count;
        set => value;
    }
    public string Name => _name;
    public string Description => _description;
    
    private Item(string n, bool stack, int count = 1)
    {
        _name = n;
        _stackable = stack;
        _count = count;
    }
}
