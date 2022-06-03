using UnityEngine;

namespace SurvivalGame.Inventory.Items
{
	[System.Serializable]
	public abstract class UsableItem : Item
	{
		public abstract void Use();
	}
}
