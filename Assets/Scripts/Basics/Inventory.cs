using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory
{
    private List<ItemSlot> _itemSlots;
    private Dictionary<string, int> _itemsStackSize;

    public event EventHandler OnItemChanged;
    
    private int _capacity;
    private int _itemsCount = 0;

    private readonly int DEFAULT_STACK_SIZE;
    
    public Inventory(int capacity) : this(capacity, new Dictionary<string, int>()) { }
    public Inventory(int capacity, int defaultStackSize) : this(capacity, new Dictionary<string, int>(),defaultStackSize) { }
    public Inventory(int capacity, Dictionary<string, int> stackSizes, int defaultStackSize = 100)
    {


        _itemSlots = new List<ItemSlot>();
        _capacity = capacity;
        Init(capacity);
        _itemsStackSize = stackSizes;
        DEFAULT_STACK_SIZE = defaultStackSize;
    }

    private void Init(int capacity)
    {
        for (int i = 0; i < capacity; i++)
            _itemSlots.Add(new ItemSlot());
    }
    
    
    /// <summary>
    /// Add item to invenotory. 
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <returns>True if adding was succesful, false if not.</returns>
    public bool AddItem(Item item, int count = 1)
    {
        if (item == null)
            throw new ArgumentException();
        if (item.IsStackable)
            //find same item.
            foreach (var slot in _itemSlots)
                if (slot.item.Name == item.Name)
                {
                    int maxStack = DEFAULT_STACK_SIZE;
                    //check if special stack size exists
                    if (_itemsStackSize.ContainsKey(item.Name))
                        maxStack = _itemsStackSize[item.Name];
                    int canAdd = maxStack - slot.count;
                    int toAdd = Mathf.Min(canAdd, count);
                    
                    slot.count += toAdd;
                    count -= toAdd;
                    if(toAdd > 0)
                        OnItemChanged?.Invoke(this,new EventArgs());
                    if(count <= 0)
                        return true;
                }

        for (int i = 0; i < _capacity; i++)
        {
            if (_itemSlots[i].item == null)
            {
                _itemSlots[i].item = item;
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
        return _itemSlots[index].item;
    }

    public void ChangeCapacity(int delta)
    {
        if (_capacity + delta <= 0)
            throw new ArgumentException();
        _capacity += delta;
        if(delta > 0)
            for (int i = 0; i < delta; i++)
                _itemSlots.Add(new ItemSlot());
        else if (delta < 0)
        {
            //TODO : Items shrinkink.
            _itemSlots.RemoveRange(_itemSlots.Count + delta,-delta);
        }
    }

    public int Capacity => _capacity;



}
