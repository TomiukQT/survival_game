using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class inventory_test
{

    private Item i1 = new Item("item1",false,1);
    private Item i2 = new Item("item2",false,1);
    private Item i3 = new Item("item3",false,1);
    private Item i4 = new Item("item4",false,1);
    
    
    //Assert.Throws(()=>MethodThatThrows());
    //customer.NameChanged += (sender, e) => { eventRaised = true; };
    [Test]
    public void inventory_add()
    {
        Inventory inventory = new Inventory(10);

        bool add = inventory.AddItem(i1);
        
        Assert.True(add);
    }

    [Test]
    public void inventory_add_over_capacity()
    {
        Inventory inventory = new Inventory(1);
        
        bool a1 = inventory.AddItem(i1);
        bool a2 = inventory.AddItem(i2);
        
        Assert.True(a1);
        Assert.False(a2);
    }

    [Test]
    public void inventory_item_change_event()
    {
        Inventory inventory = new Inventory(10);
        int eventsCallCount = 0;
        //customer.NameChanged += (sender, e) => { eventRaised = true; };
        inventory.OnItemChanged += (sender, e) => { eventsCallCount++; };
        
        Assert.Equals(0, eventsCallCount);
        inventory.AddItem(i1);
        Assert.Equals(1, eventsCallCount);
        inventory.RemoveItem(i1);
        Assert.Equals(2, eventsCallCount);
        inventory.AddItem(i1);
        Assert.Equals(3, eventsCallCount);
        inventory.RemoveItem(i2);
        Assert.Equals(3, eventsCallCount);
    }

    [Test]
    public void increase_capacity()
    {
        Inventory inventory = new Inventory(1);

        bool a1 = inventory.AddItem(i1);
        bool a2 = inventory.AddItem(i2);

        inventory.ChangeCapacity(1);

        bool a3 = inventory.AddItem(i2);
        
        Assert.True(a1);
        Assert.False(a2);
        Assert.True(a3);
    }

    [Test]
    public void decrease_capacity()
    {
        Inventory inventory = new Inventory(10);

        inventory.AddItem(i1);
        inventory.AddItem(i2);
        inventory.ChangeCapacity(-9);

        Assert.Equals(i1, inventory.GetItem(0));
        Assert.Throws<ArgumentOutOfRangeException>(()=>inventory.GetItem(1));
    }

    [Test]
    public void decrease_capacity_underflow()
    {
        Inventory inventory = new Inventory(5);
        
        Assert.Throws<ArgumentException>(()=>inventory.ChangeCapacity(-6));
    }

    [Test]
    public void inventory_remove()
    {
        Inventory inventory = new Inventory(10);

        inventory.AddItem(i1);

        bool r1 = inventory.RemoveItem(i1);
        bool r2 = inventory.RemoveItem(i2);
        
        Assert.True(r1);
        Assert.False(r2);
    }
    
    
    
    //STACKABLES
    private Item s1 = new Item("stackable1",true,1);
    private Item s2 = new Item("stackable2",true,5);
    private Item s3 = new Item("stackable3",true,10);
    private Item s4 = new Item("stackable4",true,10);
    private Item s5 = new Item("stackable5",true,20);
    [Test]
    public void add_stackable_item()
    {
        Inventory inventory = new Inventory(1, 999);

        bool a1 = inventory.AddItem(s1);
        bool a2 = inventory.AddItem(s1);
        bool a3 = inventory.AddItem(s2);
        bool a4 = inventory.AddItem(s3);
        
        Assert.True(a1);
        Assert.Equals(2, inventory.GetItem(0).Count);
        Assert.True(a2);
        Assert.True(a3);
        Assert.False(a4);

    }
    
}