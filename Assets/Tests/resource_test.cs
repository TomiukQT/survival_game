using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class resource_test
{
    
    [Test]
    public void resource_testSimplePasses()
    {
        //obj.Event += (sender, e) => { eventsCallCount++; };
    }

    [Test]
    public void resource_parameterless_constructor_test()
    {
        Resource resource = new Resource();
        
        Assert.AreEqual(resource.MaxValue,resource.Value);
        Assert.AreEqual(1f,resource.Percentage01);
        Assert.AreEqual(100,resource.Percentage);
    }
    
    [Test]
    public void resource_constructor_test()
    {
        Resource resource = new Resource(1000f);
        
        Assert.AreEqual(resource.MaxValue,resource.Value);
        Assert.AreEqual(1000f,resource.Value);
        Assert.AreEqual(1f,resource.Percentage01);
        Assert.AreEqual(100,resource.Percentage);
    }

    [Test]
    public void add_over_maximum()
    {
        Resource resource = new Resource();

        float maximum = resource.MaxValue;
        resource.Add(10f);
        resource.Add(10f);
        resource.Add(10f);
        
        
        Assert.AreEqual(maximum,resource.Value);
    }

    [Test]
    public void take_50_percent()
    {
        Resource resource = new Resource(100f);

        float half = resource.Value / 2f;
        bool a = resource.Take(half);
        
        Assert.False(a);
        Assert.AreEqual(half,resource.Value);
        Assert.AreEqual(.5f,resource.Percentage01);
        Assert.AreEqual(50f,resource.Percentage);
    }
    
    [Test]
    public void trytake_50_percent()
    {
        Resource resource = new Resource(100f);

        float half = resource.Value / 2f;
        bool a = resource.TryTake(half);
        
        Assert.True(a);
        Assert.AreEqual(half,resource.Value);
        Assert.AreEqual(.5f,resource.Percentage01);
        Assert.AreEqual(50f,resource.Percentage);
    }
    
    [Test]
    public void take_200_percent()
    {
        Resource resource = new Resource(100f);

        float h = resource.Value * 2f;
        bool a = resource.Take(h);
        
        Assert.True(a);
        Assert.AreEqual(100f,resource.Value);
        Assert.AreEqual(0f,resource.Percentage01);
        Assert.AreEqual(0f,resource.Percentage);
    }
    
    [Test]
    public void trytake_200_percent()
    {
        Resource resource = new Resource(100f);

        float h = resource.Value * 2f;
        bool a = resource.TryTake(h);
        
        Assert.False(a);
        Assert.AreEqual(100f,resource.Value);
        Assert.AreEqual(1,resource.Percentage01);
        Assert.AreEqual(100f,resource.Percentage);
    }

    [Test]
    public void add_zero_or_negative()
    {
        Resource resource = new Resource(100f);

        
        resource.Take(50f);
        float value = resource.Value;
        resource.Add(0f);
        resource.Add(-10f);
        resource.Add(-100f);
        resource.Add(-1000f);


        Assert.AreEqual(value,resource.Value);
    }

}
