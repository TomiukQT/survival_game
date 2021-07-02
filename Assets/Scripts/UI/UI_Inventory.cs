using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Inventory : MonoBehaviour,IShowable
{

    [SerializeField] private PlayerItems _playerItems;
    private Inventory _inventory;
    
    private List<UI_InventorySlot> _inventorySlots;

    private GameObject _panel;
    
    private void Awake()
    {
        _inventory = _playerItems.Inventory;
        _inventory.OnItemChanged += OnInventoryChange;

        InitInventorySlots();
        
            
        _panel = transform.GetChild(0).gameObject;
    }

    private void InitInventorySlots()
    {
        _inventorySlots = new List<UI_InventorySlot>();
        Transform inventorySlotsParent = _panel.transform.Find("inventory_slots");
        for (int i = 0; i < inventorySlotsParent.childCount; i++)
        {
            
        }
    }

    private void UpdateInvetory()
    {
        
    }
    
    public void OnInventoryChange(object sender, EventArgs eventArgs)
    {
        UpdateInventory();
    }
    
    
    

    public void SetActive(bool state) => _panel.SetActive(state);
    public void Toggle() => _panel.SetActive(!_panel.activeSelf);
}
