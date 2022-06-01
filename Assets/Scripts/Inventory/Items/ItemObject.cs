using System;
using System.Collections;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Item _itemData;
    [SerializeField] private int _itemCount;

    private float _itemTimeOut = 1f;
    private bool _isEnabled = true;

    private void Awake()
    {
        StartCoroutine(ItemTimeout());
    }

    public void SetItem(Item item, int count = 1)
    {
        _itemData = item;
    }

    private IEnumerator ItemTimeout()
    {
        _isEnabled = false;
        yield return new WaitForSeconds(_itemTimeOut);
        _isEnabled = true;
    }

    public bool IsEnabled => _isEnabled;

    public Item Item => _itemData;
    public int ItemCount => _itemCount;

    public void OnPickUp()
    {
        Destroy(gameObject);
    }
}