using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class inventory_test
{
    public class test_item : IStorable
    {
        private bool _stackable;
        private int _count;
        public string name;
        public test_item(string n, bool stack, int count = 1)
        {
            _stackable = stack;
            _count = count;
        }
        
        public bool IsStackable => _stackable;
        public int Count => _count;
        public string Name => "test_item";
        public string Description => "test_desc";
    }

    private test_item i1 = new test_item("item1",false,1);
    private test_item i2 = new test_item("item2",false,1);
    private test_item i3 = new test_item("item3",false,1);
    private test_item i4 = new test_item("item4",false,1);
    
    
    
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
    private test_item s1 = new test_item("stackable1",true,1);
    private test_item s2 = new test_item("stackable2",true,5);
    private test_item s3 = new test_item("stackable3",true,10);
    private test_item s4 = new test_item("stackable4",true,10);
    private test_item s5 = new test_item("stackable5",true,20);
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