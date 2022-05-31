using UnityEngine;

public class ItemObject : MonoBehaviour
{ 
    
    [SerializeField] private Item _itemData;
    [SerializeField] private int _itemCount;
    public void SetItem(Item item)
    {
        _itemData = item;
    }

    public Item Item => _itemData;
    public int ItemCount => _itemCount;

    public void OnPickUp()
    {
        Destroy(gameObject);
    }
    
}