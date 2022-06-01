using System;
using System.Collections.Generic;
using SurvivalGame.Inventory.Items;
using SurvivalGame.Survival.Mining;
using Unity.Collections;
using UnityEngine;

public class OnItemEquipEventArgs : EventArgs
{
    public int Index;
}

public class PlayerItems : MonoBehaviour
{

    [SerializeField] [ReadOnly] private Inventory _inventory;
    

    private Dictionary<EquipSlot, Equipable> _equipment;
    private Item _equippedItem;
    public event EventHandler<OnItemEquipEventArgs> OnItemEquip;

    private GameObject _interactZone;
    private Transform _equipPoint;
    
    private void Awake()
    {
        _inventory = new Inventory(Constants.INVENTORY_CAPACITY);
        //TODO if(SaveSystem.LoadInventory(out IEnumerable<ItemSlot> itemSlots))
            //_inventory.LoadInventory(itemSlots);}}
        
        _equipment = new Dictionary<EquipSlot, Equipable>();
        //_equipBar = new Dictionary<int, Item>();
        InitEquipment();

        _interactZone = transform.Find("interact_zone").gameObject;
        _equipPoint = transform.Find("equip_point");
    }

    private void Update()
    {
        //TEMP for testing
        if(Input.GetKeyDown(KeyCode.F9))
            SaveSystem.SaveInventory(_inventory);
        if(Input.GetKeyDown(KeyCode.F10) && SaveSystem.LoadInventory(out IEnumerable<ItemSlot> itemSlots))
            _inventory.LoadInventory(itemSlots);
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectItemOnEquipBar(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectItemOnEquipBar(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectItemOnEquipBar(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectItemOnEquipBar(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectItemOnEquipBar(4);

        if (Input.GetMouseButtonDown(0))
            HandleItemsActions();
    }

    private void HandleItemsActions()
    {
        if (_equippedItem == null)
            return;
        if(_equippedItem is UsableItem usable)
            usable.Use();
    }
    
    private void InitEquipment()
    {
        _equipment.Add(EquipSlot.Head,null);
        _equipment.Add(EquipSlot.Body,null);
        _equipment.Add(EquipSlot.Amulet,null);
        _equipment.Add(EquipSlot.Ring,null);
       
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out ItemObject item))
        {
            if (item.IsEnabled && _inventory.AddItem(item.Item))
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

    

    private void SelectItemOnEquipBar(int index)
    {
        if (index < 0 || index >= Constants.EQUIP_BAR_CAPACITY)
            throw new IndexOutOfRangeException("Equip bar out of range");
        _equippedItem = _inventory.GetItem(index);
        OnSwitchItem();
        OnItemEquip?.Invoke(this, new OnItemEquipEventArgs(){Index = index});
    }

    private void OnSwitchItem()
    {
        if(_equipPoint.childCount > 0)
            Utils.RemoveAllChilds(_equipPoint);
        if (_equippedItem == null)
            return;
        if (_equippedItem is MiningTool miningTool)
        {
            //Spawn Item on equipPoint;
            miningTool.SetHitArea(_interactZone);
        }
    }
    
    

    public Inventory Inventory => _inventory;
}
