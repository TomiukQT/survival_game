using UnityEngine;

public class ItemObject : MonoBehaviour
{ 
    
    [SerializeField] private Item _itemData;

    public void SetItem(Item item)
    {
        _itemData = item;
    }

    public Item Item => _itemData;
    

    public void OnPickUp()
    {
        Destroy(gameObject);
    }
    
}