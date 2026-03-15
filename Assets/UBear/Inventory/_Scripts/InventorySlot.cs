using UnityEngine;

namespace UBear.Inventory {
public interface IUsable
{
    void Use()
    {
      #if UNITY_INCLUDE_TESTS
      #else
      Debug.Log("Default IUsable method called.");
      #endif
    }
}
public interface ISellable
{
    int GetSellPrice();
    bool IsSellable();
}
/// <summary>
/// Represents an item instance in an inventory slot.
/// Note: This is now a wrapper around ItemInstance for serialization compatibility.
/// Pure C# class, not a ScriptableObject or MonoBehaviour.
/// Primarily used by InventoryObject to store individual slots.
/// </summary>
[System.Serializable]
public class InventorySlot
{
  public Item Item;

  public InventorySlot(Item itemInstance)
  {
    if(itemInstance == null)
    {
      Item = null;
      return;
    }
    Item = new Item(itemInstance);
  }

  public InventorySlot(ItemDefinition blueprint, int stackCount = 1)
  {
    Item = blueprint.CreateInstance(stackCount);
  }

  /// <summary>
  /// Returns true if this slot is empty, Item null or stack count 0.
  /// </summary>
  public bool IsEmpty()
  {
    return Item == null || Item.StackCount <= 0;
  }

  /// <summary>
  /// Attempts to add the specified amount to this slot, up to the maximum stack size of the item. Safe to use on full stacks.
  /// </summary>
  /// <param name="value">Amount of items to add</param>
  /// <returns>Returns the amount of items that were actually added to the slot</returns>
  public int AddAmount(int value)
  {
    return Item.AddToStack(value);
  }

  public void RemoveItem()
  {
    Item = null;
  }

  public void TryMoveToInventory(InventoryData inventory)
  {
    if (Item == null)
    {
        Debug.LogWarning("Attempted to move null item to inventory.");
        return;
    }
    int amountMoved = inventory.AddItemInstance(Item);
    Item.RemoveFromStack(amountMoved);
    if (Item.StackCount <= 0)
    {
        Item = null;
    }
  }

  public void OverwriteItem(Item itemInstance)
  {
    Item = new Item(itemInstance);
  }

/// <summary>
/// Clears the slot and returns a new InventorySlot containing the item that was previously in this slot (or null if it was empty).
/// </summary>
/// <returns>The contents that were cleared from the inventory slot</returns>
  public InventorySlot ClearSlot()
  {
    InventorySlot newSlot = new InventorySlot(Item);
    Item = null;
    return newSlot;
  }

  public void SwapItem(InventorySlot otherSlot)
  {
    Item temp = Item;
    Item = otherSlot.Item;
    otherSlot.Item = temp;
  }
}}