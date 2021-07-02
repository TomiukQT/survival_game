using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] private List<ItemSlot> _itemSlots;
    private Dictionary<string, int> _itemsStackSize;

    public event EventHandler OnItemChanged;
    
    private int _capacity;
    //private int _itemsCount = 0;

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
                if (slot.item != null && slot.item.Name == item.Name)
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
                    if (count <= 0)
                    {
                        OnItemChanged?.Invoke(this,new EventArgs());
                        return true;
                    }
                }

        for (int i = 0; i < _capacity; i++)
        {
            if (_itemSlots[i].item == null)
            {
                _itemSlots[i].item = item;
                _itemSlots[i].count = count;
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

    public bool RemoveItem(int index)
    {
        if (index < 0 || index >= _capacity)
            return false;
        if (_itemSlots[index].item == null)
            return false;
        _itemSlots[index].item = null;
        OnItemChanged?.Invoke(this,new EventArgs());
        return true;
    }
    
    

    public bool RemoveItem(Item item, int count)
    {
        int toRemove = count;
        foreach (var slot in _itemSlots)
        {
            if (slot.item == item)
            {
                int toBeRemoved = Mathf.Min(toRemove, slot.count);
                slot.count -= toBeRemoved;
                if (slot.count <= 0)
                    slot.Reset();
                toRemove -= toBeRemoved;
            }

            if (toRemove == 0)
                return true;
        }
        return false;
    }

    public Item GetItem(int index)
    {
        if (index < 0 || index >= _capacity)
            throw new IndexOutOfRangeException();
        return _itemSlots[index].item;
    }
    
    public Item GetItemWithCount(int index, out int count)
    {
        if (index < 0 || index >= _capacity)
            throw new IndexOutOfRangeException();
        count = _itemSlots[index].count;
        return _itemSlots[index].item;
    }

    public int Contains(Item item)
    {
        int count = 0;
        foreach (var slot in _itemSlots)
        {
            if (slot.item == item)
                count += slot.count;
        }

        return count;
    }

    public bool IsSpace(Item item)
    {
        if (item == null)
            return false;
        
        foreach (var slot in _itemSlots)
        {
            if (item.IsStackable)
            {
                if (slot.item == null || slot.item == item)
                    return true;
            }
            else
            {
                if (slot.item == null)
                    return true;
            }
        }

        return false;
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
