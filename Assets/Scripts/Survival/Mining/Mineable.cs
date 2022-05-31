using System;
using SurvivalGame.Inventory.Items;
using SurvivalGame.Survival.Mining.Enums;
using UnityEngine;

namespace SurvivalGame.Survival.Mining
{
	public class Mineable : MonoBehaviour, IMineable
	{
		[SerializeField] private MiningToolType _neededToolType;
		[SerializeField] private float _neededEffectivity;

		[SerializeField] private float _maxHitpoints;
		
		
		private Resource _hitpoints; 
		[SerializeField] private LootTable _lootTable;

		
		
		private void Awake()
		{
			_hitpoints = new Resource(_maxHitpoints);
		}

		public void Mine(MiningTool miningTool)
		{
			if (miningTool.ToolType != _neededToolType || miningTool.Effectivity < _neededEffectivity)
				return;

			if(_hitpoints.Take(miningTool.Effectivity))
				this.Destroy();
		}

		private void Destroy()
		{
			var lootToDrop = _lootTable.GetDrop();
			ItemDropper.DropItemsOnPosition(lootToDrop, transform.position);
			Destroy(gameObject);
		}
	}
}
