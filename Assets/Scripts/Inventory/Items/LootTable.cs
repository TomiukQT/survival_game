using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace SurvivalGame.Inventory.Items
{
	[Serializable]
	public class LootTable
	{
		[SerializeField] private List<LootTableEntry> _lootTable;

		public List<(Item, int)> GetDrop()
		{
			var itemsToDrop = new List<(Item, int)>();
			foreach (var lootTableEntry in _lootTable)
			{
				if (Random.Range(0, 1) > lootTableEntry.DropChance)
					continue;
				int count = Random.Range(lootTableEntry.DropCount.from, lootTableEntry.DropCount.to + 1);
				itemsToDrop.Add((lootTableEntry.Item,count));
			}
			
			return itemsToDrop;
		}

}

	[Serializable]
	public struct LootTableEntry
	{
		public Item Item;
		public SurvivalGame.Inventory.Items.Range DropCount;
		public float DropChance;
	}

	[Serializable]
	public struct Range
	{
		public int from;
		public int to;
	}
}
