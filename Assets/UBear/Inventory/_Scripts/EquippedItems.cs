using System;
using System.Collections.Generic;
using UnityEngine;

namespace UBear.Inventory {
public class EquippedItems : MonoBehaviour
{
  [SerializeField]
  EquippedItemsData _equippedItemsObject;

  List<EquipmentSlot> _equippedSlots => _equippedItemsObject.equipmentSlots;

  public void EquipItem(ref Item item)
  {
    if (item == null)
    {
      Debug.LogWarning("Cannot equip null item");
      return;
    }
    if(item.Blueprint is not EquipmentDefinition equipmentItem)
    {
      Debug.LogWarning($"Cannot equip item {item.Blueprint.ItemName} because it is not an equipment item");
      return;
    }
    //Find the first slot that matches the item's category and equip it there
    var equippedType = equipmentItem.EquipmentType;
    foreach (var slot in _equippedSlots)
    {
      if (slot.SlotType == equippedType)
      {
        slot.Equip(ref item);
        return;
      }
    }
    Debug.LogWarning($"No equipment slot found for item {item.Blueprint.ItemName} with type {equipmentItem.EquipmentType}");
  }
}}