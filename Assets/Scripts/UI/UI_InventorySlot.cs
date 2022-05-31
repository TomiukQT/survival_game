using System;
using NSubstitute.Exceptions;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDragAndDropEventArgs : EventArgs
{
    public int From;
    public int To;
}

public class UI_InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    private Item _item;
    private int _count; 
    private Image _icon;
    private TextMeshProUGUI _countText;
    
    private TextMeshProUGUI _itemName;
    private TextMeshProUGUI _itemDesc;

    public event EventHandler<OnDragAndDropEventArgs> OnDragAndDrop;
    
    
    private Image _placeholder;
    private RectTransform _placeholderTransform;
    private Canvas _canvas;
    
    private void Awake()
    {
        _icon = transform.Find("icon").GetComponent<Image>();
        _countText = transform.Find("count_text").GetComponent<TextMeshProUGUI>();
        
        Transform parent = transform.parent.parent.Find("item_info");
        _itemName = parent.Find("item_name").GetComponent<TextMeshProUGUI>();
        _itemDesc = parent.Find("item_desc").GetComponent<TextMeshProUGUI>();
        _canvas = GameObject.Find("UI").GetComponent<Canvas>();
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
    
    public int SlotIndex { get;set;}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item == null)
            return;
        _itemName.text = _item.Name;
        _itemDesc.text = _item.Description;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_item == null)
            return;
    }

   
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_item == null)
            return;
        //Spawn Placeholder Image
        _placeholder = Instantiate(new GameObject("inventory_placeholder"), transform.position, Quaternion.identity,_canvas.transform).AddComponent<Image>();
        var placeholderCanvasGroup = _placeholder.gameObject.AddComponent<CanvasGroup>();
        placeholderCanvasGroup.blocksRaycasts = false;
        placeholderCanvasGroup.alpha = .7f;
        _placeholderTransform = _placeholder.GetComponent<RectTransform>();
        _placeholderTransform.localScale = Vector3.one * 0.7f;
        //Set Icon to placeholder
        _placeholder.sprite = _item.Icon;
        //Invoke event to inform Inventory UI
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_placeholder == null)
            return;
        _placeholderTransform.anchoredPosition += eventData.delta * _canvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_placeholder == null)
            return;
        Destroy(_placeholder);
        _placeholder = null;
    }


    public void OnDrop(PointerEventData eventData)
    {
        var draggedFrom = eventData.pointerDrag.GetComponent<UI_InventorySlot>();
        if (draggedFrom._item == null)
            return;
        OnDragAndDrop?.Invoke(this, new OnDragAndDropEventArgs()
        {
            From = draggedFrom.SlotIndex,
            To = SlotIndex
        });
    }
}
