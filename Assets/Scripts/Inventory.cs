using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;



public class Inventory
{
    private List<IStorable> _items;
    private Dictionary<string, int> _itemsStackSize;

    public event EventHandler OnItemChanged;
    
    private int _capacity;
    private int _itemsCount = 0;

    private readonly int DEFAULT_STACK_SIZE;
    
    public Inventory(int capacity) : this(capacity, new Dictionary<string, int>()) { }
    public Inventory(int capacity, int defaultStackSize) : this(capacity, new Dictionary<string, int>(),defaultStackSize) { }
    public Inventory(int capacity, Dictionary<string, int> stackSizes, int defaultStackSize = 100)
    {
        
        
        _items = new List<IStorable>();
        _itemsStackSize = stackSizes;
        DEFAULT_STACK_SIZE = defaultStackSize;
    }

    public bool AddItem(IStorable item)
    {
        return false;
    }

    private int GetItemMaxStack(IStorable item)
    {
        return 1;
    }

    public bool RemoveItem(IStorable item)
    {
        return false;
    }

    public bool RemoveItem(IStorable item, int count)
    {
        return false;
    }

    public IStorable GetItem(int index)
    {
        if (index < 0 || index >= _capacity)
            throw new IndexOutOfRangeException();
        return _items[index];
    }

    public void ChangeCapacity(int delta)
    {
        
    }
    
    

}
