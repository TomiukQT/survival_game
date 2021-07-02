using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InventorySlot : MonoBehaviour, IPointerEnterHandler
{

    private Item _item;
    private Image _icon;

    private TextMeshProUGUI _itemName;
    private TextMeshProUGUI _itemDesc;
    
    private void Awake()
    {
        _icon = transform.Find("icon").GetComponent<Image>();
        
        Transform parent = transform.parent.parent.Find("item_info");
        _itemName = parent.Find("item_name").GetComponent<TextMeshProUGUI>();
        _itemDesc = parent.Find("item_desc").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateGraphics();
    }

    public void SetItem(Item item)
    {
        _item = item;
        UpdateGraphics();
    }

    public void RemoveItem()
    {
        _item = null;
        UpdateGraphics();
    }

    private void UpdateGraphics()
    {
        _icon.sprite = _item == null ? null :_item.Icon;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item == null)
            return;
        _itemName.text = _item.Name;
        _itemDesc.text = _item.Description;
    }
}
