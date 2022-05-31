using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalGame.UI
{
    public class UI_EquipBar : MonoBehaviour
    {
        private List<UI_EquipSlot> _equipBarSlots;
        private UI_EquipSlot _selectedSlot;

        private PlayerItems _playerItems;

        private int _selected = -1;
        
        private void Awake()
        {
            InitEquipBarSlots();
            
            _playerItems = GameObject.Find("player").GetComponent<PlayerItems>();
            _playerItems.OnItemEquip += e_OnItemEquip;
        }
        
        private void InitEquipBarSlots()
        {
            _equipBarSlots = new List<UI_EquipSlot>();
            for (int i = 0; i < transform.childCount; i++)
            {
                _equipBarSlots.Add(transform.GetChild(i).GetComponent<UI_EquipSlot>());
                //_equipBarSlots[i].OnSpellDrop += e_OnSpellDrop;
            }
        }

        private void e_OnItemEquip(object sender, OnItemEquipEventArgs e)
        {
            if(_selected >= 0)
                _equipBarSlots[_selected].Deselect();
            _selected = e.Index;
            _equipBarSlots[_selected].Select();
        }

        public void UpdateEquipSlot(int index, Item item, int count = 1) => _equipBarSlots[index].UpdateGraphics(item);
    }
}
