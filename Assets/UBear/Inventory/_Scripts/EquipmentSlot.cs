using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace UBear.Inventory {
[System.Serializable]
public class EquipmentSlot
{
  public Item EquippedItem;
  [field: SerializeField]
  public EquipmentCategory SlotType { get; private set; }

  public string SlotName;
  /// <summary>
  /// Initializes an empty equipment slot with the specified type and optional name. The slot can hold one equipment item that matches the category.
  /// </summary>
  /// <param name="slotType">Currently only allows one category per slot</param>
  /// <param name="slotName">Optional parameter; equipment category used by default</param>
  public EquipmentSlot(EquipmentCategory slotType, string slotName = "")
  {
    SlotType = slotType;
    SlotName = slotName;
    if(string.IsNullOrEmpty(SlotName))
    {
      SlotName = slotType.ToString();
    }
    EquippedItem = null;
  }
  public void Equip(ref Item item)
  {
    //Assertions are for testing and debugging, not for production code. Todo: replace with proper error handling if needed.
    // Debug.Assert(item != null, "Cannot equip null item.");
    // Debug.Assert(item.Blueprint != null, "Cannot create equipment with item that has null blueprint.");
    // Debug.Assert(item.Blueprint is EquipmentDefinition, "Cannot create equipment with non-equipment item: " + item.Blueprint.ItemName);
    var swap = EquippedItem;
    EquippedItem = item;
    item = swap;
  }
  public void Unequip()
  {
    EquippedItem = null;
    //todo: do more than destroy the item reference, maybe return it to inventory or drop it in the world?
  }
  }
}
