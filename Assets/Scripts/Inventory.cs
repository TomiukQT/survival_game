using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;



public class Inventory
{
    private Item[] _items;
    private Dictionary<string, int> _itemsStackSize;

    public event EventHandler OnItemChanged;
    
    private int _capacity;
    private int _itemsCount = 0;

    private readonly int DEFAULT_STACK_SIZE;
    
    public Inventory(int capacity) : this(capacity, new Dictionary<string, int>()) { }
    public Inventory(int capacity, int defaultStackSize) : this(capacity, new Dictionary<string, int>(),defaultStackSize) { }
    public Inventory(int capacity, Dictionary<string, int> stackSizes, int defaultStackSize = 100)
    {


        _items = new Item[capacity];
        _itemsStackSize = stackSizes;
        DEFAULT_STACK_SIZE = defaultStackSize;
    }

    /// <summary>
    /// Add item to invenotory. 
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <returns>True if adding was succesful, false if not.</returns>
    public bool AddItem(Item item)
    {
        if (item == null)
            throw new ArgumentException();
        if (item.IsStackable)
            //find same item.
            foreach (var slot in _items)
                if (slot.Name == item.Name)
                {
                    int maxStack = DEFAULT_STACK_SIZE;
                    //check if special stack size exists
                    if (_itemsStackSize.ContainsKey(item.Name))
                        maxStack = _itemsStackSize[item.Name];
                    int canAdd = maxStack - slot.Count;
                    int toAdd = Mathf.Min(canAdd, item.Count);
                    
                    slot.Count += toAdd;
                    item.Count -= toAdd;
                    if(toAdd > 0)
                        OnItemChanged?.Invoke(this,new EventArgs());
                    if(item.Count <= 0)
                        return true;
                }

        for (int i = 0; i < _capacity; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                OnItemChanged?.Invoke(this,new EventArgs());
                return true;
            }
        }
        return false;
    }

    private int GetItemMaxStack(Item item)
    {
        return 1;
    }

    public bool RemoveItem(Item item)
    {
        return false;
    }

    public bool RemoveItem(Item item, int count)
    {
        return false;
    }

    public Item GetItem(int index)
    {
        if (index < 0 || index >= _capacity)
            throw new IndexOutOfRangeException();
        return _items[index];
    }

    public void ChangeCapacity(int delta)
    {
        
    }

    public int Capacity => _capacity;



}
