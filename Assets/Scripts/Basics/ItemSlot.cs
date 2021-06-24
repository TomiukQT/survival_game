public class ItemSlot
{
    public Item item;
    public int count;

    public ItemSlot()
    {
        item = null;
        count = 0;
    }

    public ItemSlot(Item _item, int _count)
    {
        item = _item;
        if (item.IsStackable)
            count = _count;
        else
            count = 1;
    }


}