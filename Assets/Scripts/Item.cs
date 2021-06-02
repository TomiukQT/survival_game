using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IStorable
{
    private bool _stackable = false;
    private int _count = 1;

    public bool IsStackable => _stackable;
    public int Count => _count;
}
