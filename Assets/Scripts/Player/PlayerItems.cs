using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{

    [SerializeField] [ReadOnly] private Inventory _inventory;
    private const int INVENTORY_CAPACITY = 20;
    
    private void Awake()
    {
        _inventory = new Inventory(INVENTORY_CAPACITY);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out ItemObject item))
        {
            if (_inventory.AddItem(item.Item))
            {
                item.OnPickUp();
            }
        }
    }
}
