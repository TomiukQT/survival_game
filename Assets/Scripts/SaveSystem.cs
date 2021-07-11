using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static void SaveInventory(Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath + "/invetory.save");
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream,inventory.ItemSlots);
        stream.Close();
    }

    public static bool LoadInventory(out IEnumerable<ItemSlot> itemSlots)
    {
        string path = Path.Combine(Application.persistentDataPath + "/invetory.save");
        Debug.Log(path);
        itemSlots = null;
        if (!File.Exists(path))
            return false;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        itemSlots = formatter.Deserialize(stream) as IEnumerable<ItemSlot>;
        stream.Close();
        return true;
    }
}
