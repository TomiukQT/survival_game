using System.Collections.Generic;
using UnityEngine;

namespace SurvivalGame.Inventory.Items
{
	public class ItemDropper : MonoBehaviour
	{
		public static void DropItemsOnPosition(List<(Item,int)> itemsToDrop,Vector3 position)
		{
			// TODO Change item dropping for multiple items ??? remove break
			foreach (var itemPair in itemsToDrop)
			{
				var item = itemPair.Item1;
				var count = itemPair.Item2;
				var spawnedItem = Instantiate(item.WorldObject, position, Quaternion.identity);
				
				if (!spawnedItem.TryGetComponent<ItemObject>(out var itemObject))
					itemObject = spawnedItem.AddComponent<ItemObject>().GetComponent<ItemObject>();
				itemObject.SetItem(item,count);
				//spawnedItem.AddComponent(typeof(MeshCollider));
				break;
			}
		}
	}
}
