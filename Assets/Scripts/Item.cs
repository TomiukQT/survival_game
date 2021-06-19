using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string _name;
    private bool _stackable = false;
    private int _count = 1;

    public bool IsStackable => _stackable;
    public int Count => _count;

    public string Name => _name;
    public string Description => "description";
    
    public Item(string n, bool stack, int count = 1)
    {
        _name = n;
        _stackable = stack;
        _count = count;
    }
}
