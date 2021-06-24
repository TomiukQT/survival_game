using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    
}

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    
    [SerializeField] private string _name;
    [TextArea(15,20)]
    [SerializeField] private string _description;
    [SerializeField] private bool _stackable = false;

    ///TODO
    //Icon
    //Rarity?
    //GameModel
    
    public bool IsStackable => _stackable;
    public string Name => _name;
    public string Description => _description;
    
    //For testing only.
    public Item(string n, bool stack)
    {
        _name = n;
        _stackable = stack;
    }
}
