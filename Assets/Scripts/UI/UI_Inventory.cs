using System;
using System.Collections.Generic;
using SurvivalGame.UI;
using UnityEngine.UI;
using UnityEngine;

public class UI_Inventory : MonoBehaviour, IShowable
{

    [SerializeField] private PlayerItems _playerItems;
    private Inventory _inventory;
    
    private List<UI_InventorySlot> _inventorySlots;

    [SerializeField] private GameObject _panel;

    [SerializeField] private Vector3 _initialPosition;

    [SerializeField] private UI_EquipBar _equipBar;
    
    private void Awake()
    {
        InitInventorySlots();
        //_initialPosition = transform.position;

    }
    

    private void Start()
    {
        _inventory = _playerItems.Inventory;
        _inventory.OnItemChanged += OnInventoryChange;
        
        UpdateInventory();
    }

    private void InitInventorySlots()
    {
        _inventorySlots = new List<UI_InventorySlot>();
        Transform inventorySlotsParent = _panel.transform.Find("inventory_slots");
        for (int i = 0; i < inventorySlotsParent.childCount; i++)
        {
            var slot = inventorySlotsParent.GetChild(i).GetComponent<UI_InventorySlot>();
            _inventorySlots.Add(slot);
            slot.SlotIndex = i;
            slot.OnDragAndDrop += e_OnDragAndDrop;
        }
    }

    private void UpdateInventory()
    {
        //Debug.Log("Updating");
        for (int i = 0; i < _inventory.Capacity; i++)
            _inventorySlots[i].SetItem(_inventory.GetItemWithCount(i, out int count),count);
        for (int i = 0; i < Constants.EQUIP_BAR_CAPACITY; i++)
            _equipBar.UpdateEquipSlot(i,_inventory.GetItemWithCount(i, out int count),count);
    }
    
    private void OnInventoryChange(object sender, EventArgs eventArgs)
    {
        //Debug.Log("Invoking");
        UpdateInventory();
    }

    public void SetActive(bool state)
    {
        if (!state)
            transform.position = new Vector3(-10000, -10000, -10000);
        else
            transform.position = _initialPosition;
    }

    public void Toggle() => SetActive(!(transform.position == _initialPosition));

    private void e_OnDragAndDrop(object sender, OnDragAndDropEventArgs e)
    {
        _inventory.SwapItemsOnIndexes(e.From,e.To);
    }
    
}
