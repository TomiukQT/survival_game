using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalGame.UI
{
	public class UI_EquipSlot : MonoBehaviour
	{
		private readonly Color NORMAL_COLOR = Color.white;
		private readonly Color SELECTED_COLOR = Color.green;
		
		private Image _icon;
		
		[SerializeField] private List<Image> _spriteRenderers;

		private void Awake()
		{
			_icon = transform.Find("icon").GetComponent<Image>();
		}

		public void Select()
		{
			foreach (var spriteRenderer in _spriteRenderers)
				spriteRenderer.color = SELECTED_COLOR;
		}

		public void Deselect()
		{
			foreach (var spriteRenderer in _spriteRenderers)
				spriteRenderer.color = NORMAL_COLOR;
		}
		
		public void UpdateGraphics(Item item)
		{
			_icon.sprite = item == null ? null : item.Icon;
			//if (_count > 0)
				//_countText.text = $"{_count}";
			//else
				//_countText.text = "";
		}
	}
}
