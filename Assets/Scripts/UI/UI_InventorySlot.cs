using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InventorySlot : MonoBehaviour, IPointerEnterHandler
{

    private Item _item;
    private int _count; 
    private Image _icon;
    private TextMeshProUGUI _countText;
    
    private TextMeshProUGUI _itemName;
    private TextMeshProUGUI _itemDesc;
    
    private void Awake()
    {
        _icon = transform.Find("icon").GetComponent<Image>();
        _countText = transform.Find("count_text").GetComponent<TextMeshProUGUI>();
        
        Transform parent = transform.parent.parent.Find("item_info");
        _itemName = parent.Find("item_name").GetComponent<TextMeshProUGUI>();
        _itemDesc = parent.Find("item_desc").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateGraphics();
    }

    public void SetItem(Item item, int count = 1)
    {
        _item = item;
        _count = count;
        UpdateGraphics();
    }

    public void RemoveItem()
    {
        _item = null;
        _count = 0;
        UpdateGraphics();
    }

    private void UpdateGraphics()
    {
        _icon.sprite = _item == null ? null :_item.Icon;
        if (_count > 0)
            _countText.text = $"{_count}";
        else
            _countText.text = "";
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item == null)
            return;
        _itemName.text = _item.Name;
        _itemDesc.text = _item.Description;
    }
}
