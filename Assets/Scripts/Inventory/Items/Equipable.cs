using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipable Item", menuName = "Items/Equipable")]
public class Equipable : Item
{
    [Header("Equipable part")]
    [SerializeField] private EquipSlot _equipSlot;


    public EquipSlot EquipSlot => _equipSlot;
}