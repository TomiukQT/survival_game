using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    private Item _item;

    public void SetItem(Item item) => _item = item;
    public Item Item => _item;
}
