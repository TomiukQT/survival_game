using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{

    [SerializeField] [ReadOnly] private Inventory _inventory;
    private const int INVENTORY_CAPACITY = 21;

    private Dictionary<EquipSlot, Equipable> _equipment;
    
    private void Awake()
    {
        _inventory = new Inventory(INVENTORY_CAPACITY);
        if(SaveSystem.LoadInventory(out IEnumerable<ItemSlot> itemSlots))
            _inventory.LoadInventory(itemSlots);
        
        _equipment = new Dictionary<EquipSlot, Equipable>();
        InitEquipment();
    }

    private void Update()
    {
        //TEMP for testing
        if(Input.GetKeyDown(KeyCode.F9))
            SaveSystem.SaveInventory(_inventory);
        if(Input.GetKeyDown(KeyCode.F10) && SaveSystem.LoadInventory(out IEnumerable<ItemSlot> itemSlots))
            _inventory.LoadInventory(itemSlots);
            
    }

    private void InitEquipment()
    {
        _equipment.Add(EquipSlot.Head,null);
        _equipment.Add(EquipSlot.Body,null);
        _equipment.Add(EquipSlot.Amulet,null);
        _equipment.Add(EquipSlot.Ring,null);
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

    public void EquipItem(Equipable item, EquipSlot equipSlot)
    {
        if (!_equipment.ContainsKey(equipSlot))
            return;
        Equipable itemToChange = _equipment[equipSlot];
        _equipment[equipSlot] = item;
    }

    public Inventory Inventory => _inventory;
}
