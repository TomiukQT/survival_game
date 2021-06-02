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
    }

    private test_item i1 = new test_item("item1",false,1);
    private test_item i2 = new test_item("item2",false,1);
    private test_item i3 = new test_item("item3",false,1);
    private test_item i4 = new test_item("item4",false,1);
    private test_item s1 = new test_item("stackable1",true,1);
    private test_item s2 = new test_item("stackable2",true,5);
    private test_item s3 = new test_item("stackable3",true,10);
    private test_item s4 = new test_item("stackable4",true,10);
    private test_item s5 = new test_item("stackable5",true,20);
    
    [Test]
    public void inventory_testSimplePasses()
    {
    }
}