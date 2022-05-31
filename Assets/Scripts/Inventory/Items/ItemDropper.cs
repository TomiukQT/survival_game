using System.Collections.Generic;
using UnityEngine;

namespace SurvivalGame.Inventory.Items
{
	public class ItemDropper : MonoBehaviour
	{
		public static void DropItemsOnPosition(List<(Item,int)> itemsToDrop,Vector3 position)
		{
			// TODO Change item dropping for multiple items ??? remove break
			Debug.Log($"Spawning {itemsToDrop.Count} items at {position}");
			foreach (var itemPair in itemsToDrop)
			{
				var item = itemPair.Item1;
				var spawnedItem = Instantiate(new GameObject($"Item{item.Name}"), position, Quaternion.identity);
				spawnedItem.AddComponent(typeof(ItemObject)).GetComponent<ItemObject>().SetItem(item);
				// spawnedItem.model = item.model;
				
				break;
			}
		}
	}
}
